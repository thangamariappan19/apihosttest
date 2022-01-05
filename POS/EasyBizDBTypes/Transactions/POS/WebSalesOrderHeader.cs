using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EasyBizDBTypes.Transactions.POS
{
    [Serializable]
    [DataContract]
    public class WebSalesOrderHeader : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string PhoneNo { get; set; }
        [DataMember]
        public int TotalOrderQty { get; set; }
        [DataMember]
        public int TotalIssuedQty { get; set; }
        [DataMember]
        public decimal TotalPrice { get; set; }
        [DataMember]
        public decimal TaxAmount { get; set; }
        [DataMember]
        public decimal DiscountAmount { get; set; }
        [DataMember]
        public decimal TotalNetAmount { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public string StatusCode { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public List<WebSalesOrderDetails> StoreOrderDetails { get; set; }
        [DataMember]
        public List<WebSalesOrderHeader> DocumentNoLookup { get; set; }
    }
}
