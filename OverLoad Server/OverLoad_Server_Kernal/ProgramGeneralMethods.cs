
using Microsoft.Win32;
using OverLoad_Server_Kernal.AccountingSQL;
using OverLoad_Server_Kernal.Objects;
using System;
using System.Collections.Generic;


namespace OverLoad_Server_Kernal
{
    //public static class ProgramGeneralMethods
    //{
    //    public static Currency Registry_GetDefaultCurrency(DatabaseInterface DB)
    //    {
            
    //        try
    //        {

    //            RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

    //            var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);

    //            if (reg != null)
    //            {
    //                string currencyid_Str = (string)reg.GetValue("DefaultCurrency");
    //                uint currencyid = Convert.ToUInt32(currencyid_Str);
    //                Currency defaultCurrency = new CurrencySQL(DB).GetCurrencyINFO_ByID(currencyid);
    //                if (defaultCurrency == null) throw new Exception();
    //                return defaultCurrency;
    //            }
    //            else
    //            {
    //                return new CurrencySQL(DB).GetReferenceCurrency();
    //            }
    //        }
    //        catch
    //        {
    //            return  new CurrencySQL(DB).GetReferenceCurrency();
    //        }


    //   }   
    //    public static TradeState Registry_GetDefaultTradeState(DatabaseInterface DB)
    //    {

    //        try
    //        {

    //            RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

    //            var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);

    //            if (reg != null)
    //            {
    //                string tradestateid_Str = (string)reg.GetValue("DefaultTradeState");
    //                uint tradestateid = Convert.ToUInt32(tradestateid_Str);
    //                TradeState defaultTradeState = new TradeSQL.TradeStateSQL(DB).GetTradeStateBYID(tradestateid);
    //                return defaultTradeState;
    //            }
    //            else return null;
    //        }
    //        catch
    //        {
    //            return null;
    //        }


    //    }
    //    public static SellType Registry_GetDefaultSellType(DatabaseInterface DB)
    //    {

    //        try
    //        {

    //            RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

    //            var reg = localMachine.OpenSubKey("Software\\OverLoadClient.0.0", true);

    //            if (reg != null)
    //            {
    //                string SellTypeid_Str = (string)reg.GetValue("DefaultSellType");
    //                uint SellTypeid = Convert.ToUInt32(SellTypeid_Str);
    //                SellType defaultSellType = new TradeSQL.SellTypeSql(DB).GetSellTypeinfo(SellTypeid);
    //                return defaultSellType;
    //            }
    //            else return null;
    //        }
    //        catch
    //        {
    //            return null;
    //        }


    //    }

    //}
}
