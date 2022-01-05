using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS.API_SalesOrder
{
    [DataContract]
    [Serializable]
    public class API_SalesOrderDetails : BaseType
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
        public int PickedQty { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
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
