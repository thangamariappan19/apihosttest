using EasyBizDBTypes.Transactions.POS.SalesExchange;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.SalesExchangeResponse
{
    [DataContract]
    [Serializable]
    public class SelectSalesExchangeRecordResponse :BaseResponseType
    {
        [DataMember]
        public SalesExchangeHeader SalesExchangeHeaderRecord { get; set; }        
    }
}
