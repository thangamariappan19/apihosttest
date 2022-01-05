using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.API_SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.API_SalesOrderRequest
{
    [DataContract]
    [Serializable]
    public class API_SaveSalesOrderRequest : BaseRequestType
    {
        [DataMember]
        public API_SalesOrderHeader SalesOrderHeaderRecord { get; set; }
        [DataMember]
        public List<API_SalesOrderDetails> SalesOrderDetailsList { get; set; }
        [DataMember]
        public List<API_SalesOrderPayments> PaymentList { get; set; }
        [DataMember]
        public long RunningNo { get; set; }
        [DataMember]
        public long DocumentNumberingID { get; set; }
    }
}
