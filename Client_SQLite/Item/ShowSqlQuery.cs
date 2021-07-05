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
    public partial class ShowSqlQuery : Form
    {
        public ShowSqlQuery(string t)
        {
            InitializeComponent();
            textBox1.Text = t;
        }
    }
}
