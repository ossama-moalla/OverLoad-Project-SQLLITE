using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace OverLoad_Server_Kernal
{

    public class OverLoadServer_EndPoint
    {
        public static class MessageType
        {
            public const byte Error = 0xf0;
            public const byte ExecuteSQlCMD = 0xf1;
            public const byte GetData = 0xf2;
            public const byte ExecuteSQlCMD_INSERT_Serialize = 0xf3;
            public const byte GetData_ByteArray = 0xf4;
            public const byte GetData_ByteArray_NULL = 0xf5;
            public const byte Execute_Function = 0xf6;

        }
  


        #region  attributes 
        public const int BufferSize = 4096;
        private TcpClient  _WorkStation;
        public byte[] buffer;


        private List<MessageReceive> _MessagesReceivedList;
        private int LastSentMessageID;
        private DatabaseInterface __DatabaseInterface;
        #endregion
        #region OverLoadServer_EndPointProperties
        public DatabaseInterface _DatabaseInterface
        {
            get { return __DatabaseInterface; }
        }
        public TcpClient WorkStation
        {
            get { return _WorkStation; }
        }
        public List<MessageReceive> MessagesReceivedList
        {
            get { return _MessagesReceivedList; }
        }
        public bool Disposed { get; set; }
        bool Finish_CompileBlocks;
        int Offset;
        int Length;
        #endregion
        public OverLoadServer_EndPoint(TcpClient client, DatabaseInterface DatabaseInterface_)
        {
            Finish_CompileBlocks = true;
            Offset = 0;
            Length = BufferSize;

            LastSentMessageID = 0;
            __DatabaseInterface = DatabaseInterface_;
            _MessagesReceivedList = new List<MessageReceive>();
            _WorkStation = client;
            buffer = new byte[BufferSize];

            Disposed = false;
            List<byte[]> Bytes_Received = new List<byte[]>();
            NetworkStream NetworkStream_ = _WorkStation .GetStream();
            NetworkStream_.BeginRead(buffer, 0, BufferSize, ReceiveCallBack, Bytes_Received);
        }
        private async void CompileData(List<byte[]> bytes_received)
        {
            Finish_CompileBlocks = false;
            try
            {

                if (bytes_received.Count > 0)
                {
                    lock (_MessagesReceivedList )
                    {
                        for (int i = 0; i < bytes_received.Count; i++)
                        {
                            byte[] buf = bytes_received[i];
                            if (buf.Length != BufferSize && buf[0] != 0xff) throw new Exception("Block Data Error");
                            Block block = Block.GetBlock(buf);

                            bool success = BlockArrived(block);
                            while (!success) success = BlockArrived(block);
                        }


                    }
                }

            }
            catch (Exception ee)
            {
               
            }

            Finish_CompileBlocks = true;


        }
        public void ReceiveCallBack(IAsyncResult ar)
        {
            List<byte[]> Bytes_Received = (List<byte[]>)ar.AsyncState;

            try
            {

               
                //MessageBox.Show("Offset:" + Offset + ",Length:" + Length);
                NetworkStream ns = _WorkStation .GetStream();
                int bytesRead = ns.EndRead(ar);
                //MessageBox.Show("bytesRead:" + bytesRead);
                if (Offset == 0 && Length == BufferSize && bytesRead == BufferSize)
                {
                    Bytes_Received.Add(buffer);
                    buffer = new byte[BufferSize];

                }
                else if (Offset == 0 && Length == BufferSize && bytesRead < BufferSize )
                {
                    Offset = bytesRead; Length = BufferSize - bytesRead;

                }
                else if (Offset > 0 && Length < BufferSize && bytesRead < Length)
                {
                    Offset = Offset + bytesRead; Length = BufferSize - Length - bytesRead;

                }
                else
                {
                    Bytes_Received.Add(buffer);
                    Offset = 0; Length = BufferSize;
                    buffer = new byte[BufferSize];

                }

                if (Bytes_Received.Count > 0)
                {

                    if (Finish_CompileBlocks)
                    {

                        CompileData(Bytes_Received);
                        Bytes_Received = new List<byte[]>();
                    }


                }

                if (!Disposed)
                {
                    NetworkStream NetworkStream_ = _WorkStation.GetStream();
                    NetworkStream_.BeginRead(buffer, Offset, Length, ReceiveCallBack, Bytes_Received);
                }

            }
            catch (Exception ee)
            {
 
                if (_WorkStation .Connected)
                {
                    buffer = new byte[BufferSize];
                    NetworkStream NetworkStream_ = _WorkStation.GetStream();
                    NetworkStream_.BeginRead(buffer, 0, BufferSize, ReceiveCallBack, Bytes_Received);
                }

            }


        }
        internal void Dispose()
        {
            try
            {
                Disposed = true;
                WorkStation.Close ();   
            }catch
            {

            }
        }
        public MessageSend NewMessageSend(int DestinationMessageID_)
        {
            LastSentMessageID++;
            int SourceMessageID_ = LastSentMessageID;
            MessageSend MessageSend_ = new MessageSend(SourceMessageID_, DestinationMessageID_);
            return MessageSend_;
        }

        public int GetMessageReceivedIndexBySourceID(int MessageID_)
        {
            for (int i = 0; i < _MessagesReceivedList.Count; i++)
            {
                if (_MessagesReceivedList[i].SourceMessageID == MessageID_)
                {
                    return i;
                }
            }
            return -1;
        }
        public bool  BlockArrived(Block _Block)
        {
            try
            {
                #region MesageSentACK
                if (_Block.DestinationMessageID == -1 && _Block.SourceMessageID == -1)
                { return true; }
                #endregion
                else
                {
                    int MsgInd = GetMessageReceivedIndexBySourceID(_Block.SourceMessageID);

                    if (MsgInd == -1)
                    {
                        //.add(DateTime.Now, "new  block -new order");
                        MessageReceive message = new MessageReceive(_Block.SourceMessageID, _Block.DestinationMessageID, _Block.MessageSize);
                        _MessagesReceivedList.Add(message);
                        MsgInd = GetMessageReceivedIndexBySourceID(_Block.SourceMessageID);
                    }

                    _MessagesReceivedList[MsgInd].AddReceivedBlock(_Block);
                    if (_MessagesReceivedList[MsgInd].MessageReceivedCompletly)
                    {
                        MessageReceive MS = _MessagesReceivedList[MsgInd];
                        _MessagesReceivedList.RemoveAt(MsgInd);
                        //Task.Factory.StartNew(() => { AcknowledgeSender(_MessagesReceivedList[MsgInd].SourceMessageID); });
                        Task.Factory.StartNew(() => { CompileMessage(MS.SourceMessageID, MS.GetFullMessage()); });

                    }
                    else
                        _MessagesReceivedList[MsgInd].LastSeen = DateTime.Now;

                    return true;
                }
                   
            }
            catch (Exception ee)
            {
                //byte[] errormessagearray = System.Text.Encoding.UTF8.GetBytes("Repaly from Server:" + ee.Message);
                //byte[] errorstream = new byte[1 + errormessagearray.Length];
                //errorstream[0] = MessageType.Error;
                //Array.Copy(errormessagearray, 0, errorstream, 1, errormessagearray.Length);
                //MessageSend messagesend = NewMessageSend(_Block.SourceMessageID);
                //messagesend.SendMessage(this, errorstream);
                return false;
            }

    

        }

        public void CompileMessage(int SourceMessageID, byte[] MessageData)
        {

            try
            {
                byte Code = MessageData[0];

                byte[] NextOrderData = new byte[MessageData.Length - 1];
                Array.Copy(MessageData, 1, NextOrderData, 0, NextOrderData.Length);
                List<byte> replay = new List<byte>();
                switch (Code)
                {
                    //execute sql command
                    case MessageType.GetData:
                        string SQLCommand1 = System.Text.Encoding.UTF8.GetString(NextOrderData);
                       
                        System.Data.DataTable table = _DatabaseInterface.GetData(SQLCommand1);
                        BinaryFormatter bformatter = new BinaryFormatter();
                        MemoryStream stream = new MemoryStream();
                        bformatter.Serialize(stream, table);

                        replay.Add(MessageType.GetData);
                        replay.AddRange(stream.ToArray());
                        break;
                    case MessageType.GetData_ByteArray:
                        string SQLCommand2 = System.Text.Encoding.UTF8.GetString(NextOrderData);
                        System.Data.DataTable table2 = _DatabaseInterface.GetData(SQLCommand2);

                     
                        if (table2.Rows.Count > 0)
                        {
                            replay.Add(MessageType.GetData_ByteArray);
                            replay.AddRange((byte[])table2.Rows[0][0]);

                        }
                        else
                            replay.Add(MessageType.GetData_ByteArray_NULL);
                        break;


                    case MessageType.ExecuteSQlCMD:
                        string SQLCommand3 = System.Text.Encoding.UTF8.GetString(NextOrderData);

                        _DatabaseInterface.ExecuteSQLCommand(SQLCommand3);
                        replay.Add(MessageType.ExecuteSQlCMD);
                        break;
                    case MessageType.ExecuteSQlCMD_INSERT_Serialize:

                        System.Data.SQLite.SQLiteCommand SQLiteCommand_ = GetSQLite_Command(true, NextOrderData);
                        _DatabaseInterface.ExecuteSQLCommand_Serialize(SQLiteCommand_);
                        replay.Add(MessageType.ExecuteSQlCMD_INSERT_Serialize);
                        break;
                    case MessageType.Execute_Function:

                        System.Data.DataTable Execute_Function_Table = new OverLoad_SQL_Functions (_DatabaseInterface ).Execute_Function (NextOrderData );
                        BinaryFormatter bformatter1 = new BinaryFormatter();
                        MemoryStream stream1 = new MemoryStream();
                        bformatter1.Serialize(stream1, Execute_Function_Table);

                        replay.Add(MessageType.Execute_Function);
                        replay.AddRange(stream1.ToArray());
                        break;
                    default:
                        throw new Exception("ERROR :Message UnIndified");

                }
                byte[] messagereplay = replay.ToArray();
                MessageSend messagesend = NewMessageSend(SourceMessageID);
                messagesend.SendMessage(this, messagereplay);

            }
            catch (Exception ee)
            {
                byte[] errormessagearray = System.Text.Encoding.UTF8.GetBytes("Repaly from Server:" + ee.Message);
                byte[] errorstream = new byte[1 + errormessagearray.Length];
                errorstream[0] = MessageType.Error;
                Array.Copy(errormessagearray, 0, errorstream, 1, errormessagearray.Length);
                MessageSend messagesend = NewMessageSend(SourceMessageID);
                messagesend.SendMessage(this, errorstream);
            }
            GC.Collect();
        }
        private System.Data.SQLite.SQLiteCommand GetSQLite_Command(bool Insert, byte[] FullMessage)
        {
            try
            {
                System.Data.SQLite.SQLiteCommand DATABASE_SQL_COMMAND = _DatabaseInterface.Build_SQLiteCommand();

                if (FullMessage.Length == 0) throw new Exception("Packet Disasssemly Error");
                System.Data.DataTable table =
             new System.Data.DataTable();
                table.Columns.Add("IS_Blop", typeof(bool));
                table.Columns.Add("ParameterName", typeof(string));

                table.Columns.Add("ParameterValue", typeof(byte[]));

                int cruser = 0;

                int TableName_Field_Length = BitConverter.ToInt32(FullMessage, cruser);
                cruser += 4;
                byte[] TableName_Field_Array = new byte[TableName_Field_Length];
                Array.Copy(FullMessage, cruser, TableName_Field_Array, 0, TableName_Field_Length);
                string TableName = Encoding.UTF8.GetString(TableName_Field_Array);

                table.TableName = TableName;

                cruser += TableName_Field_Length;
                int RowsCount = BitConverter.ToInt32(FullMessage, cruser);
                cruser += 4;




                for (int i = 0; i < RowsCount; i++)
                {
                    System.Data.DataRow row = table.NewRow();
                    bool Is_Blop;
                    if (FullMessage[cruser] == 0x00)
                        Is_Blop = false;
                    else
                        Is_Blop = true;

                    cruser += 1;
                    int ParameterName_Field_Length = BitConverter.ToInt32(FullMessage, cruser);


                    cruser += 4;
                    byte[] ParameterName_Field_Array = new byte[ParameterName_Field_Length];
                    Array.Copy(FullMessage, cruser, ParameterName_Field_Array, 0, ParameterName_Field_Length);
                    string ParameterName = Encoding.UTF8.GetString(ParameterName_Field_Array);


                    cruser += ParameterName_Field_Length;

                    int ParameterValue_Length = BitConverter.ToInt32(FullMessage, cruser);


                    byte[] ParameterValue_Array = new byte[ParameterValue_Length];
                    cruser += 4;
                    Array.Copy(FullMessage, cruser, ParameterValue_Array, 0, ParameterValue_Length);
                    cruser += ParameterValue_Length;


                    row["IS_Blop"] = Is_Blop;
                    row["ParameterName"] = ParameterName;

                    row["ParameterValue"] = ParameterValue_Array;
                    table.Rows.Add(row);
                }



                string query = "";
                if (Insert == true)
                {
                    query += @"INSERT INTO " + TableName + "(";
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string parameter_name = Convert.ToString(table.Rows[i][1]);
                        query += parameter_name;
                        if (i != table.Rows.Count - 1) query += ",";
                    }
                    query += ") values (";
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        query += "@V" + i;
                        if (i != table.Rows.Count - 1) query += ",";
                    }
                    query += ")";
                    DATABASE_SQL_COMMAND.CommandText = query;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        bool isblop = Convert.ToBoolean(table.Rows[i][0].ToString());


                        if (isblop)
                        {

                            byte[] array = (byte[])table.Rows[i][2];
                            DATABASE_SQL_COMMAND.Parameters.AddWithValue("@V" + i, array);
                            DATABASE_SQL_COMMAND.Parameters[i].DbType = System.Data.DbType.Binary;

                        }
                        else
                        {
                            //    tw.WriteLine(DateTime.Now.ToShortTimeString() + "," + "Blop:false" +
                            //",value:" + table.Rows[i][3].ToString());
                            //    tw.Close();
                            byte[] array = (byte[])table.Rows[i][2];
                            string value = Encoding.UTF8.GetString(array);
                            DATABASE_SQL_COMMAND.Parameters.AddWithValue("@V" + i, value);
                            DATABASE_SQL_COMMAND.Parameters[i].DbType = System.Data.DbType.String;

                        }
                    }

                }

                return DATABASE_SQL_COMMAND;
            }
            catch (Exception ee)
            {
                throw new Exception("GetSQLite_Command" + ee.Message);
            }

        }

    }

    public class MessageSend
    {
 
        #region MessageAttributes
        private int _SourceMessageID;
        private int _DestinationMessageID;
        //byte[] MessageData;
        //private int _MessageSize;
        //List<Block> Blocks;
        private bool _SendComplete;
        private bool _AcknowLedgmented;
        public const int BufferSize = 4096;
        public bool Stop_Send { get; set; }
        public bool Compiled { get; set; }
        #endregion
        #region MessageProperties
        public int SourceMessageID
        {
            get
            {
                return _SourceMessageID;
            }
        }
        public int DestinationMessageID
        {
            get
            {
                return _DestinationMessageID;
            }
        }

        public bool SendComplete
        {
            get { return _SendComplete; }
        }
        public bool AcknowLedgmented
        {
            get { return _AcknowLedgmented; }
        }
        #endregion
        #region MessageMethods
        public MessageSend(int SourceMessageID_, int DestinationMessageID_)
        {

            _SourceMessageID = SourceMessageID_;
            _DestinationMessageID = DestinationMessageID_;
            _SendComplete = false;
            _AcknowLedgmented = false;
            Stop_Send = false;
            Compiled = false;

        }
        private byte[] Encrypt(byte[] clearData)
        {

            System.Security.Cryptography.PasswordDeriveBytes pdb = new System.Security.Cryptography.PasswordDeriveBytes("ali",
             new byte[] {0xa1, 0xb1, 0xc1, 0xd1, 0xa2, 0xb2,
            0xc2, 0xd2, 0xa3, 0xb3, 0xc3, 0xd3, 0x11});
            byte[] Key = pdb.GetBytes(32);
            byte[] IV = pdb.GetBytes(16);
            // Create a MemoryStream to accept the encrypted bytes 
            MemoryStream ms = new MemoryStream();

            // Create a symmetric algorithm. 
            // We are going to use Rijndael because it is strong and
            // available on all platforms. 
            // You can use other algorithms, to do so substitute the
            // next line with something like 
            //      TripleDES alg = TripleDES.Create(); 
            System.Security.Cryptography.Rijndael alg = System.Security.Cryptography.Rijndael.Create();

            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because
            // the algorithm is operating in its default 
            // mode called CBC (Cipher Block Chaining).
            // The IV is XORed with the first block (8 byte) 
            // of the data before it is encrypted, and then each
            // encrypted block is XORed with the 
            // following block of plaintext.
            // This is done to make encryption more secure. 

            // There is also a mode called ECB which does not need an IV,
            // but it is much less secure. 
            alg.Key = Key;
            alg.IV = IV;
            alg.Padding = System.Security.Cryptography.PaddingMode.Zeros;
            // Create a CryptoStream through which we are going to be
            // pumping our data. 
            // CryptoStreamMode.Write means that we are going to be
            // writing data to the stream and the output will be written
            // in the MemoryStream we have provided. 
            System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms,
               alg.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Write the data and make it do the encryption 
            cs.Write(clearData, 0, clearData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            // This will tell it that we have done our encryption and
            // there is no more data coming in, 
            // and it is now a good time to apply the padding and
            // finalize the encryption process. 
            cs.Close();

            // Now get the encrypted data from the MemoryStream.
            // Some people make a mistake of using GetBuffer() here,
            // which is not the right way. 
            byte[] encryptedData = ms.ToArray();

            return encryptedData;
        }
        public void SendMessage(OverLoadServer_EndPoint Target, byte[] _MessageData)
        {
            try
            {

                _MessageData = Encrypt(_MessageData);
                int _MessageSize = _MessageData.Length;
                //List<Block> Blocks = new List<Block>();
                int BlockIndex = 0;
                int Data_Cruser = 0;
                bool Finish = false;
                while (!Finish)
                {
                    byte[] BlockData_ = new byte[BufferSize];

                    BlockData_[0] = 0xff;

                    Array.Copy(BitConverter.GetBytes(_SourceMessageID), 0, BlockData_, 1, 4);
                    Array.Copy(BitConverter.GetBytes(_DestinationMessageID), 0, BlockData_, 5, 4);

                    Array.Copy(BitConverter.GetBytes(_MessageSize), 0, BlockData_, 9, 4);

                    Array.Copy(BitConverter.GetBytes(BlockIndex), 0, BlockData_, 13, 4);
                    int BlockDataSize;


                    if ((_MessageData.Length - Data_Cruser) > BufferSize-21)
                    {
                        BlockDataSize = BufferSize - 21;
                        Array.Copy(BitConverter.GetBytes(BlockDataSize), 0, BlockData_, 17, 4);
                        Array.Copy(_MessageData, Data_Cruser, BlockData_, 21, BlockDataSize);
                        Data_Cruser += BufferSize - 21;
                    }
                    else
                    {
                        BlockDataSize = _MessageData.Length - Data_Cruser;
                        Array.Copy(BitConverter.GetBytes(BlockDataSize), 0, BlockData_, 17, 4);
                        Array.Copy(_MessageData, Data_Cruser, BlockData_, 21, BlockDataSize);
                        //for (int j = BlockDataSize + 21; j < 4096; j++)
                        //    BlockData_[j] = 0x00;
                        Finish = true;
                        Data_Cruser += _MessageData.Length - Data_Cruser;

                    }
                    NetworkStream NetworkStream_ = Target.WorkStation.GetStream();
                    NetworkStream_.Write (BlockData_,0,BufferSize);

                    BlockIndex++;


                }

                _SendComplete = true;


            }
            catch (Exception ee)
            {

            }
        }
        #endregion
    }
    public class MessageReceive
    {
        #region MessageAttributes
        private int _SourceMessageID;
        private int _DestinationMessageID;
        private int _MessageSize;
        private List<int> Blocks_Index;
        private List<byte> MessageDate;
        private bool _ReceiveComplete;
        public DateTime LastSeen;
        bool IS_Blop;
        string File_Path;
        #endregion
        #region MessageProperties
        public int SourceMessageID
        {
            get
            {
                return _SourceMessageID;
            }
        }
        public int DestinationMessageID
        {
            get
            {
                return _DestinationMessageID;
            }
        }
        public int MessageSize
        {
            get
            {
                return _MessageSize;
            }
        }
        public bool MessageReceivedCompletly
        {
            get { return _ReceiveComplete; }
        }
        #endregion
        #region MessageMethods
        public MessageReceive(int SourceMessageID_, int DestinationMessageID_, int MessageDataSize_)
        {
            _SourceMessageID = SourceMessageID_;
            _DestinationMessageID = DestinationMessageID_;
            _MessageSize = MessageDataSize_;
            _ReceiveComplete = false;
            
            Blocks_Index = new List<int>();
            if (MessageDataSize_ > 33554432)
            {
                IS_Blop = true;
               
                string path = System.IO.Path.GetTempPath();
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                string f = path + "\\" + "OV_tmp" + 0 + ".tmp";
                int j = 0;
                while (File.Exists(f))
                {
                    j++;
                    f = path + "\\" + "OV_tmp." + j + ".tmp";


                }
                File_Path = f;
            }

            else
            {
                IS_Blop = false;
                MessageDate = new List<byte>();
            }

        }
        private int GetBlockIndex(int blockindex)
        {
            for (int i = 0; i < Blocks_Index.Count; i++)
            {
                if (Blocks_Index[i] == blockindex)
                    return i;
            }
            return -1;
        }
        public void AddReceivedBlock(Block Block_)
        {

            if (GetBlockIndex(Block_.BlockIndex) != -1)
            {
                return;
            }
            if (_ReceiveComplete)
            {
                return;
            }
            if (Block_.SourceMessageID != _SourceMessageID
                || Block_.DestinationMessageID != _DestinationMessageID
                || Block_.MessageSize != _MessageSize)
            {
                //send error :block data error
                return;
            }
            if(IS_Blop )
            {
                FileStream fs = new FileStream(File_Path, FileMode.Append, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(Block_.BlockData);
                bw.Close();
                fs.Close();
                if (this._MessageSize ==new  FileInfo(File_Path).Length )
                {
                    _ReceiveComplete = true;
                }
            }
            else
            {
                this.MessageDate.AddRange(Block_.BlockData);
                if (this._MessageSize == GetBlocks_DataSize())
                {
                    _ReceiveComplete = true;
                }
            }
     
        }
        public int GetBlocks_DataSize()
        {
            return MessageDate.Count;
        }

        public byte[] GetFullMessage()
        {
            if (!MessageReceivedCompletly) return null;
            //byte[] Message_ = new byte[_MessageSize];
            //Int64 index = 0;
            //for (int i = 0; i < Blocks.Count; i++)
            //{
            //    for (int j = 0; j < Blocks.Count; j++)
            //    {
            //        if (Blocks[j].BlockIndex == i)
            //        {

            //            Array.Copy(Blocks[i].BlockData, 0, Message_, index, Blocks[i].BlockDataSize);
            //            index += Blocks[i].BlockDataSize;
            //            break;
            //        }
            //    }
            //}
            if (IS_Blop)
                return Decrypt ( File.ReadAllBytes(File_Path));
            else 
            return Decrypt(MessageDate.ToArray());
        }
        private byte[] Decrypt(byte[] cipherData)
        {

            System.Security.Cryptography.PasswordDeriveBytes pdb = new System.Security.Cryptography.PasswordDeriveBytes("ali",
               new byte[] {0xa1, 0xb1, 0xc1, 0xd1, 0xa2, 0xb2,
            0xc2, 0xd2, 0xa3, 0xb3, 0xc3, 0xd3, 0x11});
            byte[] Key = pdb.GetBytes(32);
            byte[] IV = pdb.GetBytes(16);
            // Create a MemoryStream that is going to accept the
            // decrypted bytes 
            MemoryStream ms = new MemoryStream();

            // Create a symmetric algorithm. 
            // We are going to use Rijndael because it is strong and
            // available on all platforms. 
            // You can use other algorithms, to do so substitute the next
            // line with something like 
            //     TripleDES alg = TripleDES.Create(); 
            System.Security.Cryptography.Rijndael alg = System.Security.Cryptography.Rijndael.Create();

            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because the algorithm
            // is operating in its default 
            // mode called CBC (Cipher Block Chaining). The IV is XORed with
            // the first block (8 byte) 
            // of the data after it is decrypted, and then each decrypted
            // block is XORed with the previous 
            // cipher block. This is done to make encryption more secure. 
            // There is also a mode called ECB which does not need an IV,
            // but it is much less secure. 
            alg.Key = Key;
            alg.IV = IV;
            alg.Padding = System.Security.Cryptography.PaddingMode.Zeros;
            // Create a CryptoStream through which we are going to be
            // pumping our data. 
            // CryptoStreamMode.Write means that we are going to be
            // writing data to the stream 
            // and the output will be written in the MemoryStream
            // we have provided. 
            System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms,
                alg.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Write the data and make it do the decryption 
            cs.Write(cipherData, 0, cipherData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            // This will tell it that we have done our decryption
            // and there is no more data coming in, 
            // and it is now a good time to remove the padding
            // and finalize the decryption process. 
            cs.Close();

            // Now get the decrypted data from the MemoryStream. 
            // Some people make a mistake of using GetBuffer() here,
            // which is not the right way. 
            byte[] decryptedData = ms.ToArray();

            return decryptedData;
        }

        internal void Clear_Data()
        {
            if (IS_Blop)
            {
                try
                {
                    File.Delete(File_Path);
                }catch { }
            }else 
                MessageDate = new List<byte>();
            Blocks_Index = new List<int>();
            GC.Collect();
        }

        // Decrypt a string into a string using a password 
        //    Uses Decrypt(byte[], byte[], byte[]) 


        // Decrypt bytes into bytes using a password 
        //    Uses Decrypt(byte[], byte[], byte[]) 

        #endregion
    }

    public class Block
    {
        #region BlockAttributes
        private int _SourceMessageID;
        private int _DestinationMessageID;
        private int _MessageSize;
        private int _BlockIndex;
        private int _BlockDataSize;
        private byte[] _BlockData;
        #endregion
        #region BlockProperties
        public int SourceMessageID
        {
            get { return _SourceMessageID; }
        }
        public int DestinationMessageID
        {
            get { return _DestinationMessageID; }
        }
        public int MessageSize
        {
            get { return _MessageSize; }
        }
        public int BlockIndex
        {
            get { return _BlockIndex; }
        }
        public int BlockDataSize
        {
            get { return _BlockDataSize; }
        }
        public byte[] BlockData
        {
            get { return _BlockData; }
        }
        #endregion
        #region BlockMethods
        public Block(int SourceMessageID_, int DestinationMessageID_, int MessageSize_, int BlockIndex_, int BlockDataSize_, byte[] BlockData_)
        {
            _SourceMessageID = SourceMessageID_;
            _DestinationMessageID = DestinationMessageID_;
            _MessageSize = MessageSize_;
            _BlockIndex = BlockIndex_;
            _BlockDataSize = BlockDataSize_;
            _BlockData = BlockData_;
        }
        public static Block GetBlock(byte[] Data)
        {


            //if (Data.Length != 4096) return null;
            try
            {

                int __SourceMessageID;
                int __DestinationMessageID;
                int __MessageSize;
                int __BlockIndex;
                int __BlockDataSize;//4096 or less

                if (Data[0] != 0xff) throw new Exception("Client:Invalid Block Header");

                byte[] __SourceMessageIDCode = new byte[4];
                Array.Copy(Data, 1, __SourceMessageIDCode, 0, 4); ;
                __SourceMessageID = BitConverter.ToInt32(__SourceMessageIDCode, 0);

                byte[] __DestinationMessageIDCode = new byte[4];
                Array.Copy(Data, 5, __DestinationMessageIDCode, 0, 4); ;
                __DestinationMessageID = BitConverter.ToInt32(__DestinationMessageIDCode, 0);


                byte[] __MessageSizeCode = new byte[4];
                Array.Copy(Data, 9, __MessageSizeCode, 0, 4);
                __MessageSize = BitConverter.ToInt32(__MessageSizeCode, 0);

                byte[] __BlockIndexCode = new byte[4];
                Array.Copy(Data, 13, __BlockIndexCode, 0, 4); ;
                __BlockIndex = BitConverter.ToInt32(__BlockIndexCode, 0);

                byte[] BlockDataSizeCode = new byte[4];
                Array.Copy(Data, 17, BlockDataSizeCode, 0, 4); ;
                __BlockDataSize = BitConverter.ToInt32(BlockDataSizeCode, 0);

                byte[] __BlockData = new byte[__BlockDataSize];
                Array.Copy(Data, 21, __BlockData, 0, __BlockDataSize); ;

                return
                    new Block(__SourceMessageID, __DestinationMessageID, __MessageSize, __BlockIndex, __BlockDataSize, __BlockData);

            }
            catch (Exception ee)
            {
                //string path = @"C:\Client_BlockLog.txt";

                //System.IO.TextWriter tw = new System.IO.StreamWriter(path, true);
                //tw.WriteLine(DateTime.Now.ToShortTimeString() + "," + "GetBlock:" + ee.Message);
                //tw.Close();
                return null;

            }

        }


        #endregion
    }

    public class OverLoadServer
    {
        private string _ComputerName;
        private string _ServerName;
        IPEndPoint _Server;
        public string ComputerName
        {
            get { return _ComputerName; }
        }
        public string ServerNAme
        {
            get { return _ServerName; }
        }
        public IPEndPoint Server
        {
            get { return _Server; }
        }


        public OverLoadServer(string ComputerName_, string ServerName__, IPEndPoint Server_)
        {
            _ComputerName = ComputerName_;
            _ServerName = ServerName__;
            _Server = Server_;
        }
    }

}

