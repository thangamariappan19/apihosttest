using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.SalesOrder
{
    [DataContract]
    [Serializable]
   public class SaveSalesOrderRequest : BaseRequestType
    {
        [DataMember]       
        public SalesOrderHeader SalesOrderHeaderRecord { get; set; }
        [DataMember]
        public List<SalesOrderDetail> SalesOrderDetailsList { get; set; }
        [DataMember]
        public List<PaymentDetail> PaymentList { get; set; }
        public long RunningNo { get; set; }
        public long DocumentNumberingID { get; set; }
    }
}
