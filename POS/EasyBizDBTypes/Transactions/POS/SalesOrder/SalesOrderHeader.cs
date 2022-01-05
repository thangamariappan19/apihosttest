using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS.SalesOrder
{
    [DataContract]
    [Serializable]
   public class SalesOrderHeader: BaseType
    {
        [DataMember]
        public int ID { get;set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }       
        [DataMember]
        public DateTime DeliveryDate { get; set; }
        [DataMember]
        public int TotalQty { get; set; }
        [DataMember]
        public Decimal TotalAmount { get; set; }
        [DataMember]
        public string DiscountType { get; set; }
        [DataMember]
        public Decimal DiscountValue { get; set; }
        [DataMember]
        public Decimal NetAmount { get; set; }
        [DataMember]
        public string OrderStatus { get; set; }
        [DataMember]
        public string PaymentStatus { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public List<SalesOrderDetail> SalesOrderDetailsList { get; set; }
        [DataMember]
        public List<PaymentDetail> PaymentList { get; set; }
    }
}
