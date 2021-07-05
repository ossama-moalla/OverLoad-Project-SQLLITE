using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client
{
    public partial class ReceiveMessage_Form : Form
    {

        public bool Done { get; set; }
        private int MessageSize;
       public bool MessageReceived_Canceled { get; set; }
        public ReceiveMessage_Form(int MessageSize_)
        {
            InitializeComponent();
            //Form_Shown = false;
             MessageSize = MessageSize_;
            progressBar1.Maximum = MessageSize_;
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
            labelSize.Text = "حجم الرسالة:"+size ;

        }

        public void Set_Progress_Value(int value)
        {
            try
            {

                if (value == MessageSize) return;
                else
                {
                    // If the current thread is not the UI thread, InvokeRequired will be true
                    if (progressBar1.InvokeRequired)
                    {
                        // If so, call Invoke, passing it a lambda expression which calls
                        // UpdateText with the same label and text, but on the UI thread instead.
                        progressBar1.Invoke((Action)(() => Set_Progress_Value(value)));
                        return;
                    }
                    // If we're running on the UI thread, we'll get here, and can safely update 
                    // the label's text.
                    progressBar1.Value = value;

                }

            }
            catch
            {
                //MessageBox.Show("Change_CheckBox_Status:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            MessageReceived_Canceled = true;
            this.Close();
        }

        internal void CloseForm()
        {
            if (this .InvokeRequired)
            {
                // If so, call Invoke, passing it a lambda expression which calls
                // UpdateText with the same label and text, but on the UI thread instead.
                this .Invoke((Action)(() => CloseForm()));
                return;
            }
            // If we're running on the UI thread, we'll get here, and can safely update 
            // the label's text.
            this.Close();
        }

  
    }
}
