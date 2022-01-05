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
    public class SalesOrderDetail : BaseType
    {
        [DataMember]
        public int SerialNo { get; set; }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int SalesOrderID { get; set; }
        [DataMember]
        public string SalesOrderDocumentNo { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public int Qty { get; set; }
        [DataMember]
        public Decimal Price { get; set; }
        [DataMember]
        public Decimal SellingLineTotal { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
