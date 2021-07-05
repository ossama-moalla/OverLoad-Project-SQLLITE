using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Trade.Objects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverLoad_Client
{
    public  class ComboboxItem
    {
        public string Text { get; set; }
        public uint Value { get; set; }
        public ComboboxItem(string Text_, uint Value_)
        {
            Text = Text_;
            Value = Value_;
        }
        public override string ToString()
        {
            return Text;
        }
    }
    public static class ProgramGeneralMethods
    {
        public static Currency Registry_GetDefaultCurrency(DatabaseInterface DB)
        {
            
            try
            {

                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);

                if (reg != null)
                {
                    string currencyid_Str = (string)reg.GetValue("DefaultCurrency");
                    uint currencyid = Convert.ToUInt32(currencyid_Str);
                    Currency defaultCurrency = new AccountingObj.AccountingSQL.CurrencySQL(DB).GetCurrencyINFO_ByID(currencyid);
                    if (defaultCurrency == null) throw new Exception();
                    return defaultCurrency;
                }
                else
                {
                    return new AccountingObj.AccountingSQL.CurrencySQL(DB).GetReferenceCurrency();
                }
            }
            catch
            {
                return  new AccountingObj.AccountingSQL.CurrencySQL(DB).GetReferenceCurrency();
            }


       }
        public static string Registry_GetDataBasePath()
        {

            try
            {

                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);

                if (reg != null)
                {
                     return (string)reg.GetValue("DataBase Location");
                }
                else return "";
            }
            catch
            {
                return "";
            }


        }
        public static async  void Registry_SetDataBasePath(string path)
        {

            try
            {

                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);
                if (reg == null)
                {
                    reg = localMachine.CreateSubKey("Software\\OverLoadClient.0.0");
                }
                reg.SetValue("DataBase Location", path );

            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("فشل تخزين مسار قاعدة البيانات" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }


        }
        public static bool Registry_SetDefaultCurrency(uint CurrencyID)
        {
           try
            {
                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);
                if (reg == null)
                {
                    reg = localMachine.CreateSubKey("Software\\OverLoadClient.0.0");
                }

                reg.SetValue("DefaultCurrency", CurrencyID.ToString ());
                return true;
            }
            catch(Exception ee)
            {
                System.Windows.Forms.MessageBox.Show(""+ee.Message ,"",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error );
                return false;
            }

        }
        public  static  void FillComboBoxCurrency(ref System.Windows.Forms.ComboBox comboBoxCurrency,DatabaseInterface DB, Currency currency)
        {
            if (currency == null) currency= Registry_GetDefaultCurrency(DB);
            comboBoxCurrency.Items.Clear();
            int selected_index = 0;
            try
            {
                List<Currency> CurrencyList = new AccountingObj.AccountingSQL.CurrencySQL(DB).GetCurrencyList();
                for (int i = 0; i < CurrencyList.Count; i++)
                {
                    ComboboxItem item = new ComboboxItem(CurrencyList[i].CurrencyName + "(" + CurrencyList[i].CurrencySymbol + ")", CurrencyList[i].CurrencyID);
                    comboBoxCurrency.Items.Add(item);
                    if (currency != null && currency.CurrencyID == CurrencyList[i].CurrencyID)
                        selected_index = i;
                }
                comboBoxCurrency.SelectedIndex = selected_index;

            }
            catch
            {

            }

        }
        public static void FillComboBoxTradeState(ref System.Windows.Forms.ComboBox comboBoxTradeState, DatabaseInterface DB, Trade.Objects.TradeState TradeState_)
        {
            if (TradeState_ == null) TradeState_ = Registry_GetDefaultTradeState(DB);
            comboBoxTradeState.Items.Clear();
            int selected_index = 0;
            try
            {
                List<TradeState> TradeStateList = new Trade.TradeSQL.TradeStateSQL(DB).GetTradeStateList();
                for (int i = 0; i < TradeStateList.Count; i++)
                {
                    ComboboxItem item = new ComboboxItem(TradeStateList[i].TradeStateName , TradeStateList[i].TradeStateID);
                    comboBoxTradeState .Items.Add(item);
                    if (TradeState_ != null && TradeState_ .TradeStateID == TradeStateList[i].TradeStateID)
                        selected_index = i;
                }
                comboBoxTradeState.SelectedIndex = selected_index;

            }
            catch
            {

            }

        }
        public static bool Registry_SetDefaultTradeState(uint TradeStateID)
        {
            try
            {
                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);
                if (reg == null)
                {
                    reg = localMachine.CreateSubKey("Software\\OverLoadClient.0.0");
                }

                reg.SetValue("DefaultTradeState", TradeStateID.ToString());
                return true;
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

        }
        public static Trade.Objects.TradeState Registry_GetDefaultTradeState(DatabaseInterface DB)
        {

            try
            {

                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);

                if (reg != null)
                {
                    string tradestateid_Str = (string)reg.GetValue("DefaultTradeState");
                    uint tradestateid = Convert.ToUInt32(tradestateid_Str);
                    Trade.Objects.TradeState defaultTradeState = new Trade.TradeSQL.TradeStateSQL(DB).GetTradeStateBYID(tradestateid);
                    return defaultTradeState;
                }
                else return null;
            }
            catch
            {
                return null;
            }


        }
        public static void FillComboBoxSellType(ref System.Windows.Forms.ComboBox comboBoxSellType, DatabaseInterface DB, Trade.Objects.SellType SellType_)
        {
            if (SellType_ == null) SellType_ = Registry_GetDefaultSellType(DB);
            comboBoxSellType.Items.Clear();
            int selected_index = 0;
            try
            {
                List<SellType> SellTypeList;
                if (DB.IS_Belong_To_Admin_Group(DB.__User.UserID))
                    SellTypeList = new Trade.TradeSQL.SellTypeSql(DB).GetSellTypeList();
                else
                    SellTypeList = DB.Get_AccessSellTypePremession_List(DB.__User.UserID).Where(x => x.Access).Select(x => x._SellType).ToList();
                if (SellTypeList.Count == 0) throw new Exception("ليس لديك اي صلاحية للوصول لانماط البيع");
                for (int i = 0; i < SellTypeList.Count; i++)
                {
                    ComboboxItem item = new ComboboxItem(SellTypeList[i].SellTypeName, SellTypeList[i].SellTypeID);
                    comboBoxSellType.Items.Add(item);
                    if (SellType_ != null && SellType_.SellTypeID == SellTypeList[i].SellTypeID)
                        selected_index = i;
                }
                comboBoxSellType.SelectedIndex = selected_index;

            }
            catch(Exception ee)
            {
                throw new Exception("FillComboBoxSellType:"+ee.Message );
            }

        }
        public static bool Registry_SetDefaultSellType(uint SellTypeID)
        {
            try
            {
                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);
                if (reg == null)
                {
                    reg = localMachine.CreateSubKey("Software\\OverLoadClient.0.0");
                }

                reg.SetValue("DefaultSellType", SellTypeID.ToString());
                return true;
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

        }
        public static Trade.Objects.SellType Registry_GetDefaultSellType(DatabaseInterface DB)
        {

            try
            {

                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);

                if (reg != null)
                {
                    string SellTypeid_Str = (string)reg.GetValue("DefaultSellType");
                    uint SellTypeid = Convert.ToUInt32(SellTypeid_Str);
                    Trade.Objects.SellType defaultSellType = new Trade.TradeSQL.SellTypeSql(DB).GetSellTypeinfo(SellTypeid);
                    return defaultSellType;
                }
                else return null;
            }
            catch
            {
                return null;
            }


        }
        public static    string Registry_GetUserName()
        {

            try
            {

                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);

                if (reg != null)
                {
                    return (string)reg.GetValue("UserName");
                }
                else return "";
            }
            catch
            {
                return "";
            }


        }
        public static async void Registry_SetUserName(string username)
        {

            try
            {

                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);
                if (reg == null)
                {
                    reg = localMachine.CreateSubKey("Software\\OverLoadClient.0.0");
                }
                reg.SetValue("UserName", username );

            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("فشل تسجيل اسم المستخدم" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }


        }
        public static int Registry_Get_UDP_Port_Listner()
        {

            try
            {

                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);

                if (reg != null)
                {
                    return (int )reg.GetValue("UDP_Port_Listner");
                }
                else return 11001;
            }
            catch
            {
                return 11001;
            }


        }
        public static async void Registry_Set_UDP_Port_Listner(int UDP_Port)
        {

            try
            {

                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);
                if (reg == null)
                {
                    reg = localMachine.CreateSubKey("Software\\OverLoadClient.0.0");
                }
                reg.SetValue("UDP_Port_Listner", UDP_Port);

            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("فشل تسجيل اسم المستخدم" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }


        }
    }
}
