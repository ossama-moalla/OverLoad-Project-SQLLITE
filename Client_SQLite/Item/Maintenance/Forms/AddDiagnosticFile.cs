
using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Maintenance.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.Maintenance.Forms
{
    public partial class AddDiagnosticFile : Form
    {
        string FileExtention="";
        DatabaseInterface DB;
        DiagnosticFile _DiagnosticFile;
        DiagnosticOPR _DiagnosticOPR;
        OpenFileDialog OpenFileDialog_;
        public AddDiagnosticFile(DatabaseInterface db , DiagnosticOPR DiagnosticOPR_, OpenFileDialog OpenFileDialog__)
        {
            InitializeComponent();
            DB = db;
            _DiagnosticOPR = DiagnosticOPR_;
            OpenFileDialog_ = OpenFileDialog__;
            string [] s = OpenFileDialog_.SafeFileName.Split('.');
            if (s.Length > 1)
                FileExtention = "."+ s[s.Length - 1];
            else FileExtention = "";
            textBoxExtention.Text = FileExtention; 
            textBoxFileName.Text = s[0] ;


        

        }
        public AddDiagnosticFile(DatabaseInterface db,DiagnosticFile DiagnosticFile_)
        {
            InitializeComponent();
            DB = db;
            _DiagnosticFile = DiagnosticFile_;
            string[] s = _DiagnosticFile.FileName .Split('.');
            if (s.Length > 1)
                FileExtention = "." + s[s.Length - 1];
            else FileExtention = "";
            textBoxExtention.Text = FileExtention;
            textBoxFileName.Text = s[0];
            textBoxFileDescription.Text = _DiagnosticFile.FileDescription ;
            ADD.Name = "Update";
            ADD.Text = "تعديل";
        }

        private void ADD_Click(object sender, EventArgs e)
        {
            try
            {

                if (ADD.Name == "ADD")
                {


                    bool success = (new MaintenanceSQL.DiagnosticOPRFileSQL(DB)).AddDiagnosticOPRFile(_DiagnosticOPR
                        , textBoxFileName.Text + FileExtention
                        , textBoxFileDescription.Text
                        , OpenFileDialog_.FileName);
                    if (success)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Sql Interface:" + "فشل اضافة الملف", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    bool success = (new MaintenanceSQL.DiagnosticOPRFileSQL(DB)).UpdateDiagnosticOPRFileInfo(_DiagnosticFile.FileID
                                          , textBoxFileName.Text + FileExtention
                                          , textBoxFileDescription.Text
                                          );
                    if (success)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Sql Interface:" + "فشل تعديل الملف", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Button_Click:" +ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public byte[] FileToByteArray(string fileName)
        {
            try
            {
                byte[] buff = null;
                FileStream fs = new FileStream(fileName,
                                               FileMode.Open,
                                               FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                long numBytes = new FileInfo(fileName).Length;
                buff = br.ReadBytes((int)numBytes);
                return buff;
            }
            catch(Exception ee)
            {
                MessageBox.Show("FileToByteArray:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
           
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
