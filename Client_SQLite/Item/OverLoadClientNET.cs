

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client
{

 
    namespace OverLoadClientNET
    {
 
        public class OverLoadEndPoint
        {
            //private class OverLoad_Order
            //{
            //    public const byte Block_Received=0x
            //}


            #region Overloadinternal
            #region  attributes 
            public const int BufferSize = 4096;
            private TcpClient __TcpClient;
            public TcpClient _TcpClient
            {
                get { return __TcpClient; }
            }
            public byte[] buffer;
            private bool _Disposed;

            private List<MessageSend> _MessageSend_List;
            private const byte _Reset = 0x11;
            private const byte _MessageReplay = 0x22;
            private int LastSentMessageID;
            List<int> MessageSend_Replay_StartReceive;
            bool Finish_CompileBlocks;
          
            int Offset;
            int Length;
            #endregion
            #region OverLoadEndPointProperties

            public bool Disposed
            {
                get
                {
                    return _Disposed;
                }
                set
                {
                    _Disposed = value;
                }
            }
            
            #endregion
            public OverLoadEndPoint(TcpClient TcpClient_)
            {
                Finish_CompileBlocks = true;
                Offset = 0;
                Length = BufferSize;  
                LastSentMessageID = 0;
                _MessageSend_List = new List<MessageSend>();
                __TcpClient = TcpClient_;
                buffer = new byte[BufferSize];
                _Disposed = false;

                MessageSend_Replay_StartReceive = new List<int>();
                List<byte[]> Bytes_Received= new List<byte[]>();

                NetworkStream NetworkStream_ = __TcpClient.GetStream();
                NetworkStream_.BeginRead(buffer, 0, BufferSize , ReceiveCallBack, Bytes_Received);
                
            }

            private async void CompileData(List <byte[]> bytes_received)
            {
                Finish_CompileBlocks = false;
                try
                {


                    if (bytes_received.Count > 0)
                    {
                        lock (_MessageSend_List)
                        {
                            
                          
                            for (int i = 0; i < bytes_received.Count; i++)
                            {
                                byte[] buf = bytes_received[i];
                                if (buf.Length != BufferSize && buf[0] != 0xff) throw new Exception("Block Data Error");
                                Block block = Block.GetBlock(buf);

                                bool success = BlockArrived(block);
                                while (!success) success = BlockArrived(block);
                            }
                            try
                            {
                                for (int i = 0; i < _MessageSend_List.Count; i++)
                                {
                                    if(_MessageSend_List[i].Replay !=null )
                                    {
                                        if (_MessageSend_List[i].Stop_Send && _MessageSend_List[i].Compiled)
                                            _MessageSend_List[i].Replay.Clear_Data();
                                        if (_MessageSend_List[i].Replay.MessageSize > 1048576 && !_MessageSend_List[i].Replay.MessageReceivedCompletly && !_MessageSend_List[i].Compiled)
                                            if (!_MessageSend_List[i].Replay.ReceiveMessage_Form_.MessageReceived_Canceled) _MessageSend_List[i].Replay.UpdateReceiveMessage_Form_INFO();
                                    }

                                }
                                _MessageSend_List.RemoveAll(x => x.Compiled || x.Stop_Send);
                            }
                            catch (Exception ee)
                            {
                                throw new Exception("Update_Message_Status:" + ee.Message);
                            }
                            
                        }
                        
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("CompileData:"+ee.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
                }

                Finish_CompileBlocks = true;


            }

            public void ReceiveCallBack(IAsyncResult ar)
            {
                List<byte[]> Bytes_Received = (List<byte[]>)ar.AsyncState;

                try
                {


                    //MessageBox.Show("Offset:" + Offset + ",Length:" + Length);
                    NetworkStream ns = _TcpClient .GetStream();
                    int bytesRead = ns.EndRead(ar);
                    //MessageBox.Show("bytesRead:" + bytesRead);
                    if (Offset == 0 && Length == BufferSize && bytesRead == BufferSize)
                    {
                        Bytes_Received.Add(buffer);
                        buffer = new byte[BufferSize];

                    }
                    else if (Offset == 0 && Length == BufferSize && bytesRead < BufferSize)
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
                        NetworkStream NetworkStream_ = _TcpClient .GetStream();
                        NetworkStream_.BeginRead(buffer, Offset, Length, ReceiveCallBack, Bytes_Received);
                    }



                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("ReceiveCallBack:" + ee.Message);
                    if (__TcpClient.Connected)
                    {
                        buffer = new byte[BufferSize];
                        NetworkStream NetworkStream_ = __TcpClient.GetStream();
                        NetworkStream_.BeginRead(buffer, 0, BufferSize, ReceiveCallBack, Bytes_Received);
                    }
                    else Disposed = true;

                }


            }

            public MessageSend NewMessageSend(int DestinationMessageID_)
            {
                //log.addlog(DateTime .Now ,"send message:"+LastSentMessageID .ToString ());
                //if (MessageData_.Length == 0) return null;
                LastSentMessageID++;
                int SourceMessageID_ = LastSentMessageID;
                MessageSend MessageSend_ = new MessageSend(SourceMessageID_, DestinationMessageID_);
                _MessageSend_List.Add(  MessageSend_);
                return MessageSend_;
            }
            public   MessageReceive GetReplayMessage( MessageSend MessageSend_)
            {
                bool MessageReceive_Cancel=false ;
                if (!MessageSend_.SendComplete) throw new Exception("Message Send Not Complete");
                DateTime tt = DateTime.Now;
                try
                {

                    while (!MessageSend_.Stop_Send &&!Disposed )
                    {
                        try
                        {
                            if (MessageSend_Replay_StartReceive.Where(x => x == MessageSend_.SourceMessageID).ToList().Count > 0) break;
                        }catch { }
                        if ((DateTime.Now - tt).TotalSeconds > 60) throw new Exception("GetReplayMessage-Requested Time OUT");
                        Thread.Sleep(1);
                    }
                    tt = DateTime.Now;
                    while (!MessageSend_.Stop_Send && !Disposed)
                    {
                        MessageReceive MessageReceive_ = Read_MessageSend_Replay(MessageSend_.SourceMessageID);
                        MessageReceive_Cancel = MessageReceive_.ReceiveMessage_Form_.MessageReceived_Canceled;
                        while (!MessageReceive_Cancel && !MessageReceive_.MessageReceivedCompletly &&  !Disposed)
                        {
                            
                            MessageReceive_ = Read_MessageSend_Replay(MessageSend_.SourceMessageID);
                            MessageReceive_Cancel = MessageReceive_.ReceiveMessage_Form_.MessageReceived_Canceled;
                            if (MessageReceive_Cancel) throw new Exception("تم الإلغاء");
                            //if ((DateTime.Now - tt).TotalSeconds > 60)
                            //{
                            //    if (MessageReceive_.MessageSize <= 1048576)
                            //    {
                            //        Task.Factory.StartNew(() => { MessageReceive_. ReceiveMessage_Form_.ShowDialog(); });

                            //    }
                            //}
                            Thread.Sleep(1);
                        }

                        if (!MessageSend_.Stop_Send && !MessageReceive_Cancel && !Disposed)
                        {
                            //List<MessageReceive> list
                            //     = _MessageSend_List.Where(x => x.SourceMessageID == MessageSend_.SourceMessageID && x.Replay != null && x.Replay.MessageReceivedCompletly).Select(x => x.Replay).ToList();
                            while (true)
                            {
                                try
                                {
                                    _MessageSend_List.Where(x => x.SourceMessageID == MessageSend_.SourceMessageID).ToList()[0].Compiled = true;
                                    break;
                                }
                                catch
                                {

                                }
                            }

                            return MessageReceive_;

                        }
                        else throw new Exception("تم الإلغاء");


                    }
                     throw new Exception("تم الإلغاء");

                }
                catch(Exception ee)
                {
                    while (true )
                    {
                        try
                        {
                            _MessageSend_List.Where(x => x.SourceMessageID == MessageSend_.SourceMessageID).ToList()[0].Compiled = true; ;
                            break;
                        }
                        catch
                        {

                        }
                    }
                     throw new Exception("GetReplayMessage"+ee.Message );

                }
   


            }
            private MessageReceive Read_MessageSend_Replay(int MessageSendID)
            {
                while (true)
                {
                    try
                    {
                       List <  MessageSend > MessageSend_List = _MessageSend_List.Where(x => x.SourceMessageID == MessageSendID && x.Replay != null).ToList();
                        if (MessageSend_List.Count > 0) return MessageSend_List[0].Replay ;
                    }
                    catch
                    {

                    }
                    Thread.Sleep(1);
                }
             
            }
            public int GetMessageSendIndex_InMessageSendList(int sourceMesageID)
            {
                for (int i = 0; i < _MessageSend_List.Count; i++)
                {
                    if (_MessageSend_List[i].SourceMessageID  == sourceMesageID)
                    {
                        return i;
                    }
                }
                return -1;
            }
            private bool  BlockArrived(Block _Block)
            {
                try
                {


                    #region MesageSentACK
                    if (_Block.DestinationMessageID == -1 && _Block.SourceMessageID == -1)
                    {
                        //byte[] MessageSend_Order = Decrypt(_Block.BlockData);
                        //int MessageID = BitConverter.ToInt32(MessageSend_Order, 1);
                        //try
                        //{
                           
                        //    switch (MessageSend_Order[0])
                        //    {
                        //        case MessageSend.MessageSend_Order.SEND_NEXT_BLOCK:
                                   
                        //            _MessageSend_List.Where(x => x.SourceMessageID == MessageID).ToList()[0].Send_Next_Block = true;
                        //            break;
                        //        case MessageSend.MessageSend_Order.STOP_SEND:
                        //            _MessageSend_List.Where(x => x.SourceMessageID == MessageID).ToList()[0].Stop_Send  = true;

                        //            break;
                        //    }


                        //    return true;
                        //}
                        //catch(Exception ee)
                        //{
                        //    MessageBox.Show("BlockAravied_dd:" + ee.Message+Environment.NewLine 
                        //        +"id:"+MessageID );
                        //    return false;
                        //}


                    }
                    #endregion
                    else
                    {

                        #region MessageSentReplay
                        if (_Block.DestinationMessageID != -1)
                        {
                            int MsgInd = GetMessageSendIndex_InMessageSendList(_Block.DestinationMessageID);

                            if (MsgInd != -1)
                            {
                                if (_MessageSend_List[MsgInd].Stop_Send) return true;
                                if (_MessageSend_List[MsgInd].Replay == null)
                                {
                                    MessageSend_Replay_StartReceive.Add(_MessageSend_List[MsgInd].SourceMessageID);
                                    _MessageSend_List[MsgInd].Replay = new MessageReceive(_Block.SourceMessageID
                                        , _Block.DestinationMessageID, _Block.MessageSize);

                                }


                                _MessageSend_List[MsgInd].Replay.AddReceivedBlock(_Block);
                            }
                            else return true;

                        }
                        #endregion
                        #region NewOrder
                        else
                        {

                            //int MsgInd = GetMessageReceiveIndex_InTempMessageReceiveList (_Block.SourceMessageID);

                            //if (MsgInd == -1)
                            //{
                            //    MessageReceive message = new MessageReceive(_Block.SourceMessageID, _Block.DestinationMessageID, _Block.MessageSize, this.log);
                            //    MessageReceive_List .Add(message);
                            //    MsgInd = GetMessageReceiveIndex_InTempMessageReceiveList (_Block.SourceMessageID);
                            //}
                            //else
                            //    log.addlog(DateTime.Now, "new  block -exit order");
                            //MessageReceive_List [MsgInd].AddReceivedBlock(_Block);
                            //if (MessageReceive_List [MsgInd].MessageReceivedCompletly)
                            //{
                            //    MessageReceive __MessageReceive = MessageReceive_List[MsgInd];
                            //    MessageReceive_List.RemoveAt(MsgInd);
                            //    //Task.Factory.StartNew(() => { AcknowledgeSender(__MessageReceive.SourceMessageID); });
                            //    try
                            //    {
                            //        Task.Factory.StartNew(() => { CompileMessage(__MessageReceive.SourceMessageID, __MessageReceive.GetFullMessagePacket()); });

                            //    }
                            //    catch
                            //    {

                            //    }
                            //}
                        }
                        #endregion
                    }

                    return true;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("BlockArrived:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }

            }
            internal byte[] Encrypt(byte[] clearData)
            {

                System.Security.Cryptography.PasswordDeriveBytes pdb = new System.Security.Cryptography.PasswordDeriveBytes("ali",
                 new byte[] {0xa1, 0xb1, 0xc1, 0xd1, 0xa2, 0xb2,
            0xc2, 0xd2, 0xa3, 0xb3, 0xc3, 0xd3, 0x11});
                byte[] Key = pdb.GetBytes(32);
                byte[] IV = pdb.GetBytes(16);
                // Create a MemoryStream to accept the encrypted bytes 
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

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
            #endregion
        }
        public class MessageSend
        {
            //public static class MessageSend_Order
            //{
            //    public const byte SEND_NEXT_BLOCK = 0X01;
            //    public const byte STOP_SEND = 0X02;
            //}
            #region MessageAttributes
            private int _SourceMessageID;
            private int _DestinationMessageID;
            //byte[] MessageData;
            //private int _MessageSize;
            //List<Block> Blocks;
            private bool _SendComplete;
            private bool _AcknowLedgmented;
            public const int BufferSize = 4096;
            public MessageReceive Replay { get; set; }
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
                set
                {
                    _AcknowLedgmented = value;
                }
            }
            #endregion

            
            #region MessageMethods
            public MessageSend(int SourceMessageID_, int DestinationMessageID_)
            {
                //MessageData = Encrypt( MessageData_);
                _SourceMessageID = SourceMessageID_;
                _DestinationMessageID = DestinationMessageID_;
                //Blocks = MessageDataToBlocks(MessageData);
                //_MessageSize = MessageData.Length;
                _SendComplete = false;
                _AcknowLedgmented = false;
                Stop_Send = false;
                Compiled = false;
            }
            public static  int Get_Block_Count(int MessageSize)
            {
                return Convert .ToInt32 ( MessageSize/(BufferSize - 21));
            }
          
            private List<Block> MessageDataToBlocks(byte[] _MessageData)
            {
                int _MessageSize = _MessageData.Length;
                List<Block> Blocks = new List<Block>();
                int BlockIndex = 0;
                int Cruser = 0;
                bool Finish = false;
                while (!Finish)
                {
                    byte[] BlockData_ = new byte[BufferSize];

                    BlockData_[0] = 0xff;

                    Array.Copy(BitConverter.GetBytes(_SourceMessageID), 0, BlockData_, 1, 4);
                    Array.Copy(BitConverter.GetBytes(_DestinationMessageID), 0, BlockData_, 5, 4);

                    Array.Copy(BitConverter.GetBytes(_MessageData.Length), 0, BlockData_, 9, 4);

                    Array.Copy(BitConverter.GetBytes(BlockIndex), 0, BlockData_, 13, 4);
                    int BlockDataSize;


                    if ((_MessageData.Length - Cruser) > (BufferSize-21))
                    {
                        BlockDataSize = (BufferSize-21);
                        Array.Copy(BitConverter.GetBytes(BlockDataSize), 0, BlockData_, 17, 4);
                        Array.Copy(_MessageData, Cruser, BlockData_, 21, BlockDataSize);
                        Cruser += (BufferSize-21);
                    }
                    else
                    {
                        BlockDataSize = _MessageData.Length - Cruser;
                        Array.Copy(BitConverter.GetBytes(BlockDataSize), 0, BlockData_, 17, 4);
                        Array.Copy(_MessageData, Cruser, BlockData_, 21, BlockDataSize);
                        for (int j = BlockDataSize + 21; j < 4096; j++)
                            BlockData_[j] = 0x00;
                        Finish = true;
                    }


                    Block block_ = new Block(this.SourceMessageID, this._DestinationMessageID, _MessageSize, BlockIndex, BlockDataSize, BlockData_);
                    Blocks.Add(block_);
                    BlockIndex++;

                }



                return Blocks;
            }

            public bool SendMessage(OverLoadEndPoint Target, byte[] _MessageData)
            {

                while (true)
                {

                    try
                    {
                        _MessageData = Target. Encrypt(_MessageData);
                        int _MessageSize = _MessageData.Length;
                        int BlockIndex = 0;
                        int Cruser = 0;
                        bool Finish = false;
                        while (!Finish &&!this .Stop_Send)
                        {
                            byte[] BlockData_ = new byte[BufferSize];

                            BlockData_[0] = 0xff;

                            Array.Copy(BitConverter.GetBytes(_SourceMessageID), 0, BlockData_, 1, 4);
                            Array.Copy(BitConverter.GetBytes(_DestinationMessageID), 0, BlockData_, 5, 4);

                            Array.Copy(BitConverter.GetBytes(_MessageData.Length), 0, BlockData_, 9, 4);

                            Array.Copy(BitConverter.GetBytes(BlockIndex), 0, BlockData_, 13, 4);
                            int BlockDataSize;


                            if ((_MessageData.Length - Cruser) > (BufferSize-21))
                            {
                                BlockDataSize = (BufferSize-21);
                                Array.Copy(BitConverter.GetBytes(BlockDataSize), 0, BlockData_, 17, 4);
                                Array.Copy(_MessageData, Cruser, BlockData_, 21, BlockDataSize);
                                Cruser += (BufferSize-21);
                            }
                            else
                            {
                                BlockDataSize = _MessageData.Length - Cruser;
                                Array.Copy(BitConverter.GetBytes(BlockDataSize), 0, BlockData_, 17, 4);
                                Array.Copy(_MessageData, Cruser, BlockData_, 21, BlockDataSize);
                                //for (int j = BlockDataSize + 21; j < 4096; j++)
                                //    BlockData_[j] = 0x00;
                                Finish = true;
                            }
                            //if (BlockData_.Length != 4096)
                            //    System.Windows.Forms.MessageBox.Show("BlockSizeError:"+ BlockData_.Length
                            //        +",BlockIndex:"+BlockIndex);
                            //Target.RemoteWorkStation.Send(BlockData_);
                            NetworkStream NetworkStream_ = Target._TcpClient .GetStream();
                            NetworkStream_.Write(BlockData_,0,BufferSize);
                            BlockIndex++;
                            

                        }

                        _SendComplete = true;

                        return true;

                    }
                    catch (Exception ee)
                    {
                        if (!Target._TcpClient .Connected)
                        {
                            while (!Target._TcpClient .Connected)
                            {
                                System.Windows.Forms.DialogResult dd = System.Windows.Forms.MessageBox.Show("لا يوجد اتصال , اعادة الاتصال؟", "", System.Windows.Forms.MessageBoxButtons.RetryCancel
                           , System.Windows.Forms.MessageBoxIcon.Error);
                                Application.Exit();
                                if (dd == System.Windows.Forms.DialogResult.OK)
                                {
                                    //Target._TcpClient .Connect(Target.RemoteWorkStation.LocalEndPoint);
                                }
                                else return false;
                            }


                        }
                        else
                        {
                        //    System.Windows.Forms.DialogResult dd = System.Windows.Forms.MessageBox.Show("SendMessage:" + ee.Message + "," + "اعادة المحاولة؟", "", System.Windows.Forms.MessageBoxButtons.RetryCancel
                        //, System.Windows.Forms.MessageBoxIcon.Error);
                        //    if (dd == System.Windows.Forms.DialogResult.Cancel)
                                return false;


                        }

                    }

                }



            }
            public bool SendMessage(ref System.Windows.Forms.ProgressBar progressBar, OverLoadEndPoint Target, byte[] _MessageData)
            {

                while (true)
                {

                    try
                    {
                        _MessageData = Target. Encrypt(_MessageData);
                        int _MessageSize = _MessageData.Length;
                        //List<Block> Blocks = new List<Block>();
                        int BlockIndex = 0;
                        int Cruser = 0;
                        bool Finish = false;
                        while (!Finish && !this.Stop_Send)
                        {
                            byte[] BlockData_ = new byte[BufferSize];

                            BlockData_[0] = 0xff;

                            Array.Copy(BitConverter.GetBytes(_SourceMessageID), 0, BlockData_, 1, 4);
                            Array.Copy(BitConverter.GetBytes(_DestinationMessageID), 0, BlockData_, 5, 4);

                            Array.Copy(BitConverter.GetBytes(_MessageData.Length), 0, BlockData_, 9, 4);

                            Array.Copy(BitConverter.GetBytes(BlockIndex), 0, BlockData_, 13, 4);
                            int BlockDataSize;


                            if ((_MessageData.Length - Cruser) > (BufferSize-21))
                            {
                                BlockDataSize = (BufferSize-21);
                                Array.Copy(BitConverter.GetBytes(BlockDataSize), 0, BlockData_, 17, 4);
                                Array.Copy(_MessageData, Cruser, BlockData_, 21, BlockDataSize);
                                Cruser += (BufferSize-21);
                            }
                            else
                            {
                                BlockDataSize = _MessageData.Length - Cruser;
                                Array.Copy(BitConverter.GetBytes(BlockDataSize), 0, BlockData_, 17, 4);
                                Array.Copy(_MessageData, Cruser, BlockData_, 21, BlockDataSize);
                                //for (int j = BlockDataSize + 21; j < 4096; j++)
                                //    BlockData_[j] = 0x00;
                                Finish = true;
                            }
                            //if (BlockData_.Length != 4096)
                            //    System.Windows.Forms.MessageBox.Show("BlockSizeError:" + BlockData_.Length
                            //        + ",BlockIndex:" + BlockIndex);
                            //Target.RemoteWorkStation.Send(BlockData_);
                            NetworkStream NetworkStream_ = Target._TcpClient.GetStream();
                            NetworkStream_.Write(BlockData_, 0, BufferSize);
                            Change_ProgressBar_status(progressBar);

                            BlockIndex++;
                          
                        }

                        _SendComplete = true;

                        return true;

                    }
                    catch (Exception ee)
                    {
                        if (!Target._TcpClient .Connected)
                        {
                            while (!Target._TcpClient .Connected)
                            {
                                System.Windows.Forms.DialogResult dd = System.Windows.Forms.MessageBox.Show("لا يوجد اتصال , اعادة الاتصال؟", "", System.Windows.Forms.MessageBoxButtons.RetryCancel
                           , System.Windows.Forms.MessageBoxIcon.Error);
                                if (dd == System.Windows.Forms.DialogResult.OK)
                                {
                                    //Target.WorkStation.Connect(Target.RemoteWorkStation.LocalEndPoint);
                                }
                                else return false;
                            }


                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("SendMessage:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                            return false;


                        }

                    }

                }



            }
            public bool SendMessage(ref System.Windows.Forms.ProgressBar progressBar, OverLoadEndPoint Target, string File_Path)
            {

                while (true)
                {

                    try
                    {
                        //byte[] FileData = null;
                        System.IO.FileStream fs = new System.IO.FileStream(File_Path,
                                                       System.IO.FileMode.Open,
                                                       System.IO.FileAccess.Read);
                        //System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                        int File_numBytes =Convert .ToInt32 ( new System.IO.FileInfo(File_Path).Length);
                        //System.Windows.Forms.MessageBox.Show("File_numBytes:" + File_numBytes);

                        long BlockIndex = 0;
                        int Cruser = 0;
                        bool Finish = false;
                        while (!Finish && !this.Stop_Send)
                        {
                            byte[] BlockData_ = new byte[BufferSize];

                            BlockData_[0] = 0xff;

                            Array.Copy(BitConverter.GetBytes(_SourceMessageID), 0, BlockData_, 1, 4);
                            Array.Copy(BitConverter.GetBytes(_DestinationMessageID), 0, BlockData_, 5, 4);

                            Array.Copy(BitConverter.GetBytes(File_numBytes), 0, BlockData_, 9, 4);

                            Array.Copy(BitConverter.GetBytes(BlockIndex), 0, BlockData_, 13, 4);
                            int BlockDataSize;


                            if ((File_numBytes - Cruser) > BufferSize - 21)
                            {
                                BlockDataSize = BufferSize - 21;
                                Array.Copy(BitConverter.GetBytes(BlockDataSize), 0, BlockData_, 17, 4);
                                byte[] stream_data = new byte[BlockDataSize];
                                //System.Windows.Forms.MessageBox.Show("Cru:"+ Cruser+ "BlockDataSize:"+ BlockDataSize);
                                fs.Read(stream_data, 0, stream_data.Length  );
                                //System.Windows.Forms.MessageBox.Show("1");
                                Array.Copy(stream_data, 0, BlockData_, 21, BlockDataSize);
                                //System.Windows.Forms.MessageBox.Show("2");
                                Cruser += BufferSize - 21;
                            }
                            else
                            {
                                BlockDataSize = File_numBytes - Cruser;
                                Array.Copy(BitConverter.GetBytes(BlockDataSize), 0, BlockData_, 17, 4);
                                byte[] stream_data = new byte[BlockDataSize];
                                //System.Windows.Forms.MessageBox.Show("g");
                                fs.Read(stream_data, 0, BlockDataSize);
                                Array.Copy(stream_data, 0, BlockData_, 21, BlockDataSize);

                                Finish = true;
                                fs.Close();
                            }

                            //if (BlockData_.Length != 4096)
                            //    System.Windows.Forms.MessageBox.Show("BlockSizeError:" + BlockData_.Length
                            //        + ",BlockIndex:" + BlockIndex);
                            //System.Windows.Forms.MessageBox.Show("S:"+ BlockData_.Length);
                            //Target.RemoteWorkStation.Send(BlockData_);
                            NetworkStream NetworkStream_ = Target._TcpClient.GetStream();
                            NetworkStream_.Write(BlockData_, 0, BufferSize);
                            Change_ProgressBar_status(progressBar);

                            BlockIndex++;
                            
                        }
                        _SendComplete = true;
                        try
                        {
                            System.IO.File.Delete(File_Path);
                        }
                        catch
                        {

                        }
                        return true;

                    }
                    catch (Exception ee)
                    {
                        if (!Target._TcpClient .Connected)
                        {
                            while (!Target._TcpClient .Connected)
                            {
                                System.Windows.Forms.DialogResult dd = System.Windows.Forms.MessageBox.Show("لا يوجد اتصال , اعادة الاتصال؟", "", System.Windows.Forms.MessageBoxButtons.RetryCancel
                           , System.Windows.Forms.MessageBoxIcon.Error);
                                if (dd == System.Windows.Forms.DialogResult.OK)
                                {
                                    //Target.WorkStation.Connect(Target.RemoteWorkStation.LocalEndPoint);
                                }
                                else
                                {
                                    try
                                    {
                                        System.IO.File.Delete(File_Path);
                                    }
                                    catch
                                    {

                                    }
                                    return false;
                                }
                            }


                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("SendMessage:"+ee.Message ,"",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error );
                            try
                            {
                                System.IO.File.Delete(File_Path);
                            }
                            catch
                            {

                            }
                            return false;


                        }

                    }

                }



            }

            private void Change_ProgressBar_status(System.Windows.Forms.ProgressBar progressBar)
            {
                try
                {
                    // If the current thread is not the UI thread, InvokeRequired will be true
                    if (progressBar.InvokeRequired)
                    {
                        // If so, call Invoke, passing it a lambda expression which calls
                        // UpdateText with the same label and text, but on the UI thread instead.
                        progressBar.Invoke((Action)(() => Change_ProgressBar_status(progressBar)));
                        return;
                    }
                    // If we're running on the UI thread, we'll get here, and can safely update 
                    // the label's text.
                    progressBar.PerformStep();

                }
                catch 
                {
                    //MessageBox.Show("Change_CheckBox_Status:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            //public bool SendMessage(OverLoadEndPoint Target, byte[] _MessageData)
            //{

            //    while (true)
            //    {

            //        try
            //        {
            //            List<Block> Blocks = MessageDataToBlocks(_MessageData);
            //            for (int i = 0; i < Blocks.Count; i++)
            //            {
            //                Target.RemoteWorkStation.Send(Blocks[i].BlockData);
            //                // log.addlog(DateTime.Now, "send message : source :[" + this.SourceMessageID.ToString() + "]"
            //                //+ ",dest:[" + this.DestinationMessageID.ToString() + "]"
            //                //+ ",Block Index:[" + this.Blocks[i].BlockIndex.ToString() + "]");

            //            }

            //            _SendComplete = true;

            //            return true;

            //        }
            //        catch (Exception ee)
            //        {
            //            if (!Target.WorkStation.Connected)
            //            {
            //                while (!Target.WorkStation.Connected)
            //                {
            //                    System.Windows.Forms.DialogResult dd = System.Windows.Forms.MessageBox.Show("لا يوجد اتصال , اعادة الاتصال؟", "", System.Windows.Forms.MessageBoxButtons.RetryCancel
            //               , System.Windows.Forms.MessageBoxIcon.Error);
            //                    if (dd == System.Windows.Forms.DialogResult.OK)
            //                    {
            //                        Target.WorkStation.Connect(Target.RemoteWorkStation.LocalEndPoint);
            //                    }
            //                    else return false;
            //                }


            //            }
            //            else
            //            {
            //                System.Windows.Forms.DialogResult dd = System.Windows.Forms.MessageBox.Show("SendMessage:" + ee.Message + "," + "اعادة المحاولة؟", "", System.Windows.Forms.MessageBoxButtons.RetryCancel
            //            , System.Windows.Forms.MessageBoxIcon.Error);
            //                if (dd == System.Windows.Forms.DialogResult.Cancel)
            //                    return false;


            //            }

            //        }

            //    }



            //}
            //public bool SendMessage(ref System.Windows.Forms.ProgressBar progressBar, OverLoadEndPoint Target, byte[] _MessageData)
            //{

            //    while (true)
            //    {

            //        try
            //        {
            //            List<Block> Blocks = MessageDataToBlocks(_MessageData);

            //            for (int i = 0; i < Blocks.Count; i++)
            //            {
            //                Target.RemoteWorkStation.Send(Blocks[i].BlockData);
            //                Change_ProgressBar_status(progressBar);
            //                // log.addlog(DateTime.Now, "send message : source :[" + this.SourceMessageID.ToString() + "]"
            //                //+ ",dest:[" + this.DestinationMessageID.ToString() + "]"
            //                //+ ",Block Index:[" + this.Blocks[i].BlockIndex.ToString() + "]");

            //            }

            //            _SendComplete = true;

            //            return true;

            //        }
            //        catch (Exception ee)
            //        {
            //            if (!Target.WorkStation.Connected)
            //            {
            //                while (!Target.WorkStation.Connected)
            //                {
            //                    System.Windows.Forms.DialogResult dd = System.Windows.Forms.MessageBox.Show("لا يوجد اتصال , اعادة الاتصال؟", "", System.Windows.Forms.MessageBoxButtons.RetryCancel
            //               , System.Windows.Forms.MessageBoxIcon.Error);
            //                    if (dd == System.Windows.Forms.DialogResult.OK)
            //                    {
            //                        Target.WorkStation.Connect(Target.RemoteWorkStation.LocalEndPoint);
            //                    }
            //                    else return false;
            //                }


            //            }
            //            else
            //            {
            //                System.Windows.Forms.DialogResult dd = System.Windows.Forms.MessageBox.Show("SendMessage:" + ee.Message + "," + "اعادة المحاولة؟", "", System.Windows.Forms.MessageBoxButtons.RetryCancel
            //            , System.Windows.Forms.MessageBoxIcon.Error);
            //                if (dd == System.Windows.Forms.DialogResult.Cancel)
            //                    return false;


            //            }

            //        }

            //    }



            //}

            #endregion
        }
        public class MessageReceive
        {
            internal ReceiveMessage_Form ReceiveMessage_Form_;
            #region MessageAttributes
            private int _SourceMessageID;
            private int _DestinationMessageID;
            private int _MessageSize;
            private List<int> Blocks_Index;
            private List<byte> MessageData;
            private bool _ReceiveComplete;
            public DateTime LastSeen;
            bool IS_Blop;
            string File_Path;
            public  bool Compiled { get; set; }
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
                Compiled = false;
                Blocks_Index = new List<int>();
                if (MessageDataSize_ > 33554432)
                {
                    IS_Blop = true;

                    //string path = System.IO.Path.GetTempPath();
                    string path = Application.StartupPath + "\\" + "OverLoadTemp";
                    if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);

                    string f = path + "\\" + "OV_C_tmp" + 0 + ".tmp";
                    int j = 0;
                    while (System.IO.File.Exists(f))
                    {
                        j++;
                        f = path + "\\" + "OV_C_tmp." + j + ".tmp";


                    }
                    File_Path = f;
                }

                else
                {
                    IS_Blop = false;
                    MessageData = new List<byte>();
                }
                ReceiveMessage_Form_ = new ReceiveMessage_Form(_MessageSize);
                if (_MessageSize > 1048576)
                {
                    Task.Factory.StartNew(()=> { ReceiveMessage_Form_.ShowDialog(); });
                  
                }
            }
         
            public void UpdateReceiveMessage_Form_INFO()
            {
                if (_MessageSize > 1048576 &&!ReceiveMessage_Form_.MessageReceived_Canceled)
                    ReceiveMessage_Form_.Set_Progress_Value(GetBlocks_DataSize()) ;
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
                if (ReceiveMessage_Form_.MessageReceived_Canceled)
                {
                    try
                    {
                        if (!ReceiveMessage_Form_.IsDisposed)
                            ReceiveMessage_Form_.Close();

                    }
                    catch { }
                    return;
                }
     
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
                
                if (IS_Blop)
                {
                    System.IO.FileStream fs = new System.IO.FileStream(File_Path, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                    System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                    bw.Write(Block_.BlockData);
                    bw.Close();
                    fs.Close();

                }
                else
                {
                    this.MessageData.AddRange(Block_.BlockData);
                    //MessageBox.Show("AddReceivedBlock:this._MessageSize:"
                    //    + this._MessageSize
                    //    + ",Block_.BlockDataSize :" + Block_.BlockDataSize 
                    //     + ",Block_.BlockIndex:" + Block_.BlockIndex);
                    
                }
                int BlocksSize_Recieved=GetBlocks_DataSize ();
                if (this._MessageSize == BlocksSize_Recieved)
                {
                    _ReceiveComplete = true;
                    try
                    {
                        ReceiveMessage_Form_.CloseForm();
                    }catch { }
                }

            }
            public   int GetBlocks_DataSize()
            {
                if (IS_Blop)
                    return Convert .ToInt32 ( new System.IO.FileInfo(File_Path).Length);
                else
                    return MessageData.Count;
            }

            public byte[] GetFullMessage()
            {
                try
                {

                    if (!MessageReceivedCompletly) throw new Exception("Message Receive Not Complete");

                    byte[] fullMessage;
                    if (IS_Blop)

                        fullMessage = Decrypt(System.IO.File.ReadAllBytes(File_Path));

                    else
                        fullMessage = Decrypt(MessageData.ToArray());

                    byte[] data = new byte[fullMessage.Length - 1];
                    Array.Copy(fullMessage, 1, data, 0, data.Length);
                   
                    Clear_Data();
                    if (fullMessage [0] == DatabaseInterface.MessageType.Error)
                        throw new Exception(System.Text.Encoding.UTF8.GetString(data));
                    fullMessage = new byte[0];
                    GC.Collect();
                    return data;
                }
                catch (Exception ee)
                {
                    throw new Exception( ee.Message);
                }
            }
            public byte []  GetFullMessage_ByteArray()
            {
                try
                {
                    if (!MessageReceivedCompletly) throw new Exception("Message Receive Not Complete");
                   
                    byte[] fullMessage;
                    if (IS_Blop)

                        fullMessage = Decrypt(System.IO.File.ReadAllBytes(File_Path));
                    else
                        fullMessage = Decrypt(MessageData.ToArray());
                    if (fullMessage[0] == DatabaseInterface.MessageType.GetData_ByteArray_NULL ) return null;
                    byte[] data = new byte[fullMessage.Length - 1];
                    Array.Copy(fullMessage, 1, data, 0, data.Length);

                    if (fullMessage[0]== DatabaseInterface.MessageType.Error)
                        throw new Exception(System.Text.Encoding.UTF8.GetString(data));
                    fullMessage = new byte[0];
                    Clear_Data();
                    GC.Collect();
                    return data ;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetFullMessage_ByteArray:" + ee.Message );
                }
                
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
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

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
                try
                {
                    try
                    {
                        ReceiveMessage_Form_.Dispose();
                    }
                    catch { }
                    if (IS_Blop)
                    {
                        try
                        {
                            System.IO.File.Delete(File_Path);
                        }
                        catch { }
                    }
                    else
                        MessageData = new List<byte>();
                    Blocks_Index = new List<int>();
                    GC.Collect();
                }
                catch(Exception ee)
                {
                    throw new Exception("Clear_Data:"+ee.Message );
                }
               
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

                        if (Data[0 ] != 0xff) throw new Exception("Client:Invalid Block Header");

                        byte[] __SourceMessageIDCode = new byte[4];
                        Array.Copy(Data, 1 , __SourceMessageIDCode, 0, 4); ;
                        __SourceMessageID = BitConverter.ToInt32(__SourceMessageIDCode, 0);

                        byte[] __DestinationMessageIDCode = new byte[4];
                        Array.Copy(Data, 5 , __DestinationMessageIDCode, 0, 4); ;
                        __DestinationMessageID = BitConverter.ToInt32(__DestinationMessageIDCode, 0);


                        byte[] __MessageSizeCode = new byte[4];
                        Array.Copy(Data, 9 , __MessageSizeCode, 0, 4);
                        __MessageSize = BitConverter.ToInt32(__MessageSizeCode, 0);

                        byte[] __BlockIndexCode = new byte[4];
                        Array.Copy(Data, 13 , __BlockIndexCode, 0, 4); ;
                        __BlockIndex = BitConverter.ToInt32(__BlockIndexCode, 0);

                        byte[] BlockDataSizeCode = new byte[4];
                        Array.Copy(Data, 17 , BlockDataSizeCode, 0, 4); ;
                        __BlockDataSize = BitConverter.ToInt32(BlockDataSizeCode, 0);

                        byte[] __BlockData = new byte[__BlockDataSize];
                        Array.Copy(Data, 21 , __BlockData, 0, __BlockDataSize); ;

                        return 
                            new Block(__SourceMessageID, __DestinationMessageID, __MessageSize, __BlockIndex, __BlockDataSize, __BlockData);
                       
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBlockList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    //string path = @"C:\Client_BlockLog.txt";

                    //System.IO.TextWriter tw = new System.IO.StreamWriter(path, true);
                    //tw.WriteLine(DateTime.Now.ToShortTimeString() + "," + "GetBlock:" + ee.Message);
                    //tw.Close();
                    return  null ;

                }
               
            }


            #endregion
        }
        public class OverLoadServer
        {
            private string _ComputerName;
            private string _ServerName;
            System.Net.IPEndPoint _Server;
            public string ComputerName
            {
                get { return _ComputerName; }
            }
            public string ServerNAme
            {
                get { return _ServerName; }
            }
            public System.Net.IPEndPoint Server
            {
                get { return _Server; }
            }


            public OverLoadServer(string ComputerName_, string ServerName__, System.Net.IPEndPoint Server_)
            {
                _ComputerName = ComputerName_;
                _ServerName = ServerName__;
                _Server = Server_;
            }
        }
        public static class ServerMethods
        {
            public static List<System.Net.IPAddress> GetLocalIPv4()
            {
                List<System.Net.IPAddress> IpAddressList = new List<System.Net.IPAddress>();
                foreach (System.Net.NetworkInformation.NetworkInterface item in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (item.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                    {
                        foreach (System.Net.NetworkInformation.UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                IpAddressList.Add(ip.Address);
                            }
                        }
                    }
                }
                return IpAddressList;
            }
            public static byte[] BuildUdpPacket(string servername, System.Net.IPEndPoint server)
            {
                if (servername.Length > 254) return null;
                List<byte> packet = new List<byte>();
                packet.Add(0xff);
                byte[] servername_bytes = System.Text.Encoding.ASCII.GetBytes(servername);
                packet.Add(Convert.ToByte(servername_bytes.Length));
                packet.AddRange(servername_bytes);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                bf.Serialize(ms, server);
                ms.Close();

                byte[] server_Bytes = ms.ToArray();
                byte[] server_Bytes_Length = BitConverter.GetBytes(server_Bytes.Length);

                packet.Add(Convert.ToByte(server_Bytes_Length.Length));
                packet.AddRange(server_Bytes_Length);
                packet.AddRange(server_Bytes);

                return (packet.ToArray());

            }


        }
        public class ServersMonitor
        {
            public OverLoadServer OVServer { get; }
            public System.Net.IPAddress ServerIP
            {
                get;
            }
            public DateTime LastSeen { get; set; }
            public bool  Available { get; set; }

            public ServersMonitor(OverLoadServer OVServer_, System.Net.IPAddress ServerIP_, DateTime LastSeen_,bool Available_)
            {
                OVServer = OVServer_;
                ServerIP = ServerIP_;
                LastSeen = LastSeen_;
                Available = Available_;

            }
        }

   
    }
}
