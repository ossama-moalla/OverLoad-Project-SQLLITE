using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Maintenance.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverLoad_Client.Trade
{
    namespace  Objects
    {
        public class Industrial_OPR
        {
            /// <summary>
            /// ///
            /// </summary>
            public Operation _Operation;
            public DateTime OPR_Date;
            public string OprDesc;
            public double? Amount;
            public ConsumeUnit _ConsumeUnit;
            public Item _Item;
            public string TradeStateName;
            public Money_Currency _Money_Currency;

            public string OPRStatus;
            public Industrial_OPR(Operation Operation_
                , DateTime OPR_Date_, string OprDesc_, double? Amount_, ConsumeUnit ConsumeUnit_,
                    Item Item_, string TradeStateName_, Money_Currency Money_Currency_, string OPRStatus_)
            {
                _Operation = Operation_;
                OPR_Date = OPR_Date_;
                OprDesc = OprDesc_;
                Amount = Amount_;
                _ConsumeUnit = ConsumeUnit_;
                _Item = Item_;
                TradeStateName = TradeStateName_;
                _Money_Currency = Money_Currency_;

                OPRStatus = OPRStatus_;
            }
        }

        public class DisAssemblabgeOPR
        {
            public Operation _Operation;
            public DateTime OprDate;
            public string OprDesc;
            public string Notes;
            public ItemOUT _ItemOUT;

            //public Currency _Currency;
            //public double ExchangeRate;
            public DisAssemblabgeOPR(uint OperationID, DateTime OprDate_, string OprDesc_,
                string Notes_, ItemOUT ItemOUT_         )
            {
                _Operation = new Operation(Operation.DISASSEMBLAGE, OperationID);
                OprDate = OprDate_;
                Notes = Notes_;
                OprDesc = OprDesc_;
                _ItemOUT = ItemOUT_;

                //_Currency = Currency_;
                //ExchangeRate = ExchangeRate_;

            }
        }
        public class AssemblabgeOPR
        {
            public Operation _Operation;
            public DateTime OprDate;
            public string OprDesc;
            public string Notes;
            public ItemIN _ItemIN;

            public AssemblabgeOPR(uint OperationID, DateTime OprDate_,string OprDesc_,
            string Notes_,  ItemIN ItemIN_
                )
            {
                _Operation = new Operation(Operation.ASSEMBLAGE , OperationID);
                OprDate = OprDate_;
                Notes = Notes_;
                OprDesc = OprDesc_;
                _ItemIN = ItemIN_;
 
                //_Currency = Currency_;
                //ExchangeRate = ExchangeRate_;


            }
        }

    }
}
