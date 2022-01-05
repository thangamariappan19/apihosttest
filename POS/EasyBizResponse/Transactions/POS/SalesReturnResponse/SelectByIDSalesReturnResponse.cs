using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesReturn;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.SalesReturnResponse
{
    [DataContract]
    [Serializable]
   public class SelectByIDSalesReturnResponse : BaseResponseType
    {
        [DataMember]
        public SalesReturnHeader SalesReturnHeaderData { get; set; }
    }
}
