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
    public class WebSalesOrderDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int HeaderID { get; set; }
        [DataMember]
        public int SerialNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public int OrderQty { get; set; }
        [DataMember]
        public int IssuedQty { get; set; }
        [DataMember]
        public int AvailableQty { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public decimal TaxAmount { get; set; }
        [DataMember]
        public decimal DiscountAmount { get; set; }
        [DataMember]
        public decimal NetAmount { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public string StatusCode { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        /*[DataMember]
        public bool IsReturned { get; set; }
        [DataMember]
        public string ReturnDocumentNo { get; set; }
        [DataMember]
        public bool IsExchanged { get; set; }
        [DataMember]
        public string ExchangedDocumentNo { get; set; }*/

    }
}
