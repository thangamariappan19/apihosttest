using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesExchange;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.SalesExchangeRequest
{
    [DataContract]
    [Serializable]
   public class SaveSalesExchangeRequest:BaseRequestType
    {
        [DataMember]
        public SalesExchangeHeader SalesExchangeHeaderRecord { get; set; }
        [DataMember]
        public List<SalesExchangeDetail> SalesExchangeDetailList { get; set; }

        [DataMember]
        public List<SalesExchangeDetail> ReturnList { get; set; } //For Updatiing Invoice Details

        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }

        public string PosCode { get; set; }
        public string CountryCode { get; set; }
        public string StoreCode { get; set; }

        [DataMember]
        public long RunningNo { get; set; }

        [DataMember]
        public long DocumentNumberingID { get; set; }
    }
}
