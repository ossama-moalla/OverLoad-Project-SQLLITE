
using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.ItemObj.Objects;
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

namespace OverLoad_Client.ItemObj.Forms
{
    public partial class AddItemFile : Form
    {
        string FileExtention="";
        DatabaseInterface DB;
        ItemFile ItemFile_;
        Item Item_;
        OpenFileDialog OpenFileDialog_;
        public AddItemFile(DatabaseInterface db ,Item Item__,OpenFileDialog OpenFileDialog__)
        {
            InitializeComponent();
            DB = db;

            Item_ = Item__;
            OpenFileDialog_ = OpenFileDialog__;
            string [] s = OpenFileDialog_.SafeFileName.Split('.');
            if (s.Length > 1)
                FileExtention = "."+ s[s.Length - 1];
            else FileExtention = "";
            textBoxExtention.Text = FileExtention; 
            textBoxFileName.Text = s[0] ;


        

        }
        public AddItemFile(DatabaseInterface db,ItemFile ItemFile__)
        {
            InitializeComponent();
            DB = db;

            ItemFile_ = ItemFile__;
            string[] s = ItemFile_.FileName .Split('.');
            if (s.Length > 1)
                FileExtention = "." + s[s.Length - 1];
            else FileExtention = "";
            textBoxExtention.Text = FileExtention;
            textBoxFileName.Text = s[0];
            textBoxFileDescription.Text = ItemFile__.FileDescription ;
            buttonADD.Name = "buttonUpdate";
            buttonADD.Text = "تعديل";
        }

        private void Item_File_Method()
        { 
}
        private void ADD_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonADD .Name == "buttonADD")
            {

                    //byte [] FileData=FileToByteArray(OpenFileDialog_.FileName);
                    bool success = (new ItemFileSQL(DB)).AddItemFile(Item_.ItemID
                        ,textBoxFileName.Text +FileExtention
                        , textBoxFileDescription.Text
                        , OpenFileDialog_.FileName);
                    if (success)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }

               
            }
            else
            {
                bool success = (new ItemFileSQL(DB)).UpdateFileInfo(ItemFile_ .FileID
                                      , textBoxFileName.Text+FileExtention
                                      , textBoxFileDescription.Text
                                      );
                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            }
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddFile:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //try
            //{
            //    buttonADD.Enabled = false;
            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show(ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    buttonADD.Enabled = true;
            //}
        }
        //public byte[] FileToByteArray(string fileName)
        //{
        //    try
        //    {
        //        byte[] buff = null;
        //        FileStream fs = new FileStream(fileName,
        //                                       FileMode.Open,
        //                                       FileAccess.Read);
        //        BinaryReader br = new BinaryReader(fs);
        //        long numBytes = new FileInfo(fileName).Length;
        //        buff = br.ReadBytes((int)numBytes);
        //        return buff;
        //    }
        //    catch
        //    {
        //        MessageBox.Show("AddFile:" + "فشل اضافة الملف", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }
           
        //}

        private void bbuttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel ;
            this.Close();
        }
    }
}
