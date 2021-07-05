using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace OverLoadServer_Interface
{
    public class DatabaseInterface
    {
        private  SQLiteConnection DATABASE_CONNECTION;
        private  SQLiteCommand DATABASE_SQL_COMMAND;



        public DatabaseInterface(string db)
        {
            string s = "Data Source=" + db + ";Password=os##588412;";
            //string s = "Data Source=" + db + ";";

            DATABASE_CONNECTION = new SQLiteConnection(s);
            DATABASE_SQL_COMMAND = new SQLiteCommand("", DATABASE_CONNECTION);
        }
        public void Open_Connection()
        {
            this.DATABASE_CONNECTION.Open();
        }
        public void Close_Connection()
        {
            this.DATABASE_CONNECTION.Close  ();
        }
        public System .Data .ConnectionState Get_Connection_State()
        {
            return  this.DATABASE_CONNECTION.State ;
        }
        //public void ExecuteSQLCommand(string SQLCommand)
        //{
        //    try
        //    {
        //        DATABASE_SQL_COMMAND.CommandText = SQLCommand;
        //        if (DATABASE_CONNECTION.State == ConnectionState.Open) DATABASE_CONNECTION.Close();
        //        DATABASE_CONNECTION.Open();
        //        DATABASE_SQL_COMMAND.ExecuteNonQuery();
        //        DATABASE_CONNECTION.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }


        //}

        public DataTable GetData(string SQLCommand)
        {
            try
            {

                if (DATABASE_CONNECTION.State != ConnectionState.Open)
                    DATABASE_CONNECTION.Open();
                DATABASE_SQL_COMMAND.CommandText = SQLCommand;
                SQLiteDataAdapter DATABASE_ADAPTER = new SQLiteDataAdapter();
                DATABASE_ADAPTER.SelectCommand = DATABASE_SQL_COMMAND;
                DataTable table = new DataTable();
                DATABASE_ADAPTER.Fill(table);
                //DATABASE_CONNECTION.Close();
                return table;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }

}

