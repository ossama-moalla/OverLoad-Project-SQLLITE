using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //MessageBox.Show(System.IO.Path.GetTempPath());
                try
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.StartupPath + "\\" + "OverLoadTemp");

                    foreach (System.IO.FileInfo file in di.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch { }

                    }
                    foreach (System.IO.DirectoryInfo dir in di.GetDirectories())
                    {
                        try
                        {
                            dir.Delete(true);

                        }
                        catch { }
                    }
                }
                catch
                {

                }

                ServerSelectForm ServerSelect_ = new ServerSelectForm();
                DialogResult d = ServerSelect_.ShowDialog();

                if (d != DialogResult.OK) return;
                TcpClient TcpClient_ = ServerSelect_.Local_;
                string ServerName = ServerSelect_.ServerName;
                ServerSelect_.Dispose();


                DatabaseInterface DB = new DatabaseInterface(new OverLoadClientNET.OverLoadEndPoint(TcpClient_), ServerName);
                //MessageBox.Show("ff");
                while (true)
                {

                    LogINForm LogINForm_ = new LogINForm(DB);

                    DialogResult dd = LogINForm_.ShowDialog();
                    if (dd == DialogResult.OK)
                    {
                        //Application.Run(new Trade.Forms.Container.User_ShowLocationsForm(DB, null,0));
                        if (DB.IS_Belong_To_Admin_Group(DB.__User.UserID))
                            Application.Run(new AccountingObj.Forms.MainWindowForm(DB));
                        else
                            Application.Run(new APP_Choice_Form(DB));

                    }
                    else break;

                }
            }
            catch (Exception ee)
            {
                MessageBox .Show ("OverLoad Start:" + ee.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
           



        }
        //public static bool ConnectDataBase(string path)
        //{
        //    DatabaseInterface DB = new DatabaseInterface(path);
        //    try
        //    {
        //        DB.DATABASE_CONNECTION.Open();
        //        if (DB.DATABASE_CONNECTION.State == System.Data.ConnectionState.Open) DB.DATABASE_CONNECTION.Close();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }


}
