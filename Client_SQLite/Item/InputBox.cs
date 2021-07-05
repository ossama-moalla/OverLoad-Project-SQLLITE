using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OverLoad_Client.Forms
{
    public partial class InputBox : Form
    {
        public InputBox(string title,string label)
        {
            InitializeComponent();
            this.Text = title;
            this.label .Text   = label;
            textBox1.Focus();
        }
        public InputBox(string title, string label,string inputtext)
        {
            InitializeComponent();
            this.Text = title;
            this.label.Text = label;
            this.textBox1.Text = inputtext;
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = inputtext.Length;
            textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("يرجى ادخال نص");
        }
    }
}
