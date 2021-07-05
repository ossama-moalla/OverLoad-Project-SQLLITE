using OverLoad_Client.OverLoadClientNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client
{
    public partial class ExecuteSQLCommand_Insert_Serialiaze_Form : Form
    {
        OverLoadEndPoint _OverLoadEndPoint;
        byte[] orderarray;
        string File_Path;
        Thread Send_Thread;
        MessageSend messagesend;
        bool Abort;
        public ExecuteSQLCommand_Insert_Serialiaze_Form(OverLoadEndPoint OverLoadEndPoint_, string File_Path_)
        {
            InitializeComponent();
            Abort = false;
            int MessageSize = Convert.ToInt32(new System.IO.FileInfo(File_Path_).Length);
            string size = "";
            if (MessageSize < 1000)
            {
                size = MessageSize + " بايت ";
            }
            else if (MessageSize < 1000000)
            {
                size = MessageSize / 1000 + " كيلو بايت ";
            }
            else
            {
                size = MessageSize / 1000000 + " ميغا بايت ";
            }
            labelSize.Text = "حجم الرسالة:" + size;
            File_Path = File_Path_;
            progressBar3.Style = ProgressBarStyle.Marquee;
            progressBar3.MarqueeAnimationSpeed = 0;
            _OverLoadEndPoint = OverLoadEndPoint_;

            progressBar1.Maximum = MessageSend.Get_Block_Count(MessageSize);
            progressBar1.Step = 1;
            messagesend = _OverLoadEndPoint.NewMessageSend(-1);

            Send_Thread = new Thread(ExecuteSQLCommand_Insert_Serialiaze_BigFile);
   

        }
        //public ExecuteSQLCommand_Insert_Serialiaze_Form(OverLoadEndPoint OverLoadEndPoint_, byte[] MessageData)
        //{
        //    InitializeComponent();

        //    orderarray = MessageData;
        //    progressBar3.Style = ProgressBarStyle.Marquee;
        //    progressBar3.MarqueeAnimationSpeed = 0;
        //    _OverLoadEndPoint = OverLoadEndPoint_;
        //    //BinaryFormatter bformatter = new BinaryFormatter();
        //    //MemoryStream stream = new MemoryStream();
        //    //bformatter.Serialize(stream, table_);
        //    //List<byte> order = new List<byte>();
        //    //order.Add(DatabaseInterface.MessageType.ExecuteSQlCMD_INSERT_Serialize);
        //    //order.AddRange(stream.ToArray());
        //    //orderarray = order.ToArray();


        //    progressBar1.Maximum = MessageSend.Get_Block_Count(orderarray.Length);
        //    progressBar1.Step = 1;
        //    messagesend = _OverLoadEndPoint.NewMessageSend(-1);
        //    Send_Thread = new Thread(ExecuteSQLCommand_Insert_Serialiaze_MessageData);
        //}
        //public ExecuteSQLCommand_Insert_Serialiaze_Form(OverLoadEndPoint OverLoadEndPoint_, byte[] MessageData_)
        //{
        //    InitializeComponent();


        //    progressBar3.Style = ProgressBarStyle.Marquee;
        //    progressBar3.MarqueeAnimationSpeed = 0;
        //    _OverLoadEndPoint = OverLoadEndPoint_;

        //    progressBar1.Maximum = MessageSend.Get_Block_Count(MessageData_.Length);
        //    progressBar1.Step = 1;
        //    messagesend = _OverLoadEndPoint.NewMessageSend(-1);
        //    if (MessageData_.Length > 10000000)
        //    {
        //        string path = Application.StartupPath + "\\" + "OverLoadTemp";
        //        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        //        File_Path = path + "\\" + "tmp." + 0;
        //        int j = 1;
        //        while (File.Exists(File_Path))
        //        {

        //            File_Path = path + "\\" + "tmp." + j;


        //        }




        //        File.WriteAllBytes(File_Path, MessageData_);
        //        int File_numBytes = Convert.ToInt32(new System.IO.FileInfo(File_Path).Length);
        //        if (File_numBytes <= 0) throw new Exception("فشل انشاء حزمة البيانات");
        //        //bool success= DB.ExecuteSQLCommand_Insert_Serialiaze(table);
        //        Send_Thread = new Thread(ExecuteSQLCommand_Insert_Serialiaze_BigFile);

        //    }
        //    else
        //    {
        //        orderarray = MessageData_;
        //        Array.Clear(MessageData_, 0, MessageData_.Length);
        //        GC.Collect();
        //        Send_Thread = new Thread(ExecuteSQLCommand_Insert_Serialiaze_MessageData);

        //    }
        //}

        private void ExecuteSQLCommand_Insert_Serialiaze_BigFile()
        {
            try
            {



                //_OverLoadEndPoint. log.addlog(DateTime.Now, "error ms length" + orderarray.Length.ToString());
                bool sent = messagesend.SendMessage(ref progressBar1, _OverLoadEndPoint, File_Path);
                if (!sent) return;
                //Array.Clear ( orderarray ,0, orderarray.Length );

                Change_CheckBox_Status(checkBoxSend, true);
                Change_Label_Color(labelsend);
                Start_Waiting_Progressbar();
                MessageReceive MessageReceive_ = _OverLoadEndPoint.GetReplayMessage(messagesend);
                if (MessageReceive_ == null) throw new Exception("Empty MessageReplay");
                Change_CheckBox_Status(checkBoxReplayReceive, true);
                Change_Label_Color(labelreplay);
                Stop_Waiting_Progressbar();
                /*byte[] replay =*/ MessageReceive_.GetFullMessage();
                //if (replay[0] == DatabaseInterface.MessageType.Error)
                //{
                //    byte[] errorstream = new byte[replay.Length - 1];
                //    Array.Copy(replay, 1, errorstream, 0, errorstream.Length);
                //    throw new Exception(System.Text.Encoding.UTF8.GetString(errorstream));
                //}

                Set_Dialog_Result(DialogResult.OK);
                //CloseForm();
            }
            catch (ThreadAbortException ee)
            {
                //Set_Dialog_Result(DialogResult.Cancel);
                //CloseForm();
            }
            catch (Exception ee)
            {
                if (!Abort)
                {
                    MessageBox.Show("ExecuteSQLCommand_Form:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Set_Dialog_Result(DialogResult.Cancel);
                }

                //CloseForm();
            }


        }
        private void ExecuteSQLCommand_Insert_Serialiaze_MessageData()
        {
            try
            {



                //_OverLoadEndPoint. log.addlog(DateTime.Now, "error ms length" + orderarray.Length.ToString());
                bool sent = messagesend.SendMessage(ref progressBar1, _OverLoadEndPoint, orderarray);
                Array.Clear(orderarray, 0, orderarray.Length );
                if (!sent) return;
                //Array.Clear ( orderarray ,0, orderarray.Length );
                GC.Collect();
                Change_CheckBox_Status(checkBoxSend, true);
                Change_Label_Color(labelsend);
                Start_Waiting_Progressbar();
                MessageReceive MessageReceive_ = _OverLoadEndPoint.GetReplayMessage(messagesend);
                if (MessageReceive_ == null) throw new Exception("Empty MessageReplay");
                Change_CheckBox_Status(checkBoxReplayReceive, true);
                Change_Label_Color(labelreplay);
                Stop_Waiting_Progressbar();
                /*byte[] replay =*/ MessageReceive_.GetFullMessage ();
                //if (replay[0] == DatabaseInterface.MessageType.Error)
                //{
                //    byte[] errorstream = new byte[replay.Length - 1];
                //    Array.Copy(replay, 1, errorstream, 0, errorstream.Length);
                //    throw new Exception(System.Text.Encoding.UTF8.GetString(errorstream));
                //}

                Set_Dialog_Result(DialogResult.OK);
                //CloseForm();
            }
            catch (ThreadAbortException ee)
            {
                Set_Dialog_Result(DialogResult.Cancel);
                //CloseForm();
            }
            catch (Exception ee)
            {
                MessageBox.Show("ExecuteSQLCommand_Form:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Set_Dialog_Result(DialogResult.Cancel);
                //CloseForm();
            }


        }

        public void Start_Waiting_Progressbar()
        {
            if (progressBar3.InvokeRequired)
            {
                progressBar3.Invoke((Action)(() => Start_Waiting_Progressbar()));
                return;
            }
            progressBar3.MarqueeAnimationSpeed = 30;
        }
        public void Stop_Waiting_Progressbar()
        {
            if (progressBar3.InvokeRequired)
            {
                progressBar3.Invoke((Action)(() => Stop_Waiting_Progressbar()));
                return;
            }
            progressBar3.MarqueeAnimationSpeed = 0;
        }
        private void Set_Dialog_Result(DialogResult d)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => Set_Dialog_Result(d)));
                return;
            }


            this.DialogResult = d;
        }
        private void CloseForm()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => CloseForm()));
                return;
            }


            this.Close();
        }
        private void Change_CheckBox_Status(CheckBox CheckBox_, bool status)
        {
            try
            {
                // If the current thread is not the UI thread, InvokeRequired will be true
                if (CheckBox_.InvokeRequired)
                {
                    // If so, call Invoke, passing it a lambda expression which calls
                    // UpdateText with the same label and text, but on the UI thread instead.
                    CheckBox_.Invoke((Action)(() => Change_CheckBox_Status(CheckBox_, status)));
                    return;
                }
                // If we're running on the UI thread, we'll get here, and can safely update 
                // the label's text.
                if (status)
                    CheckBox_.Checked = true;
                else
                    CheckBox_.Checked = false;

            }
            catch (Exception ee)
            {
                MessageBox.Show("Change_CheckBox_Status:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Change_Label_Color(Label label)
        {
            try
            {
                // If the current thread is not the UI thread, InvokeRequired will be true
                if (label.InvokeRequired)
                {
                    // If so, call Invoke, passing it a lambda expression which calls
                    // UpdateText with the same label and text, but on the UI thread instead.
                    label.Invoke((Action)(() => Change_Label_Color(label)));
                    return;
                }
                // If we're running on the UI thread, we'll get here, and can safely update 
                // the label's text.
                label.BackColor = Color.LimeGreen;

            }
            catch (Exception ee)
            {
                MessageBox.Show("Change_CheckBox_Status:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ExecuteSQLCommand_Insert_Serialiaze_Form_Load(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(ExecuteSQLCommand_Insert_Serialiaze);
            Send_Thread.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!messagesend.SendComplete) messagesend.Stop_Send = true;
            Abort = true;
            Send_Thread.Abort();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
