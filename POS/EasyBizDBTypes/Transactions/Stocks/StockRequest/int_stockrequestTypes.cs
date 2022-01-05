using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Stocks.StockRequest
{
    [DataContract]
    [Serializable]
    public class int_stockrequestTypes : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocNum { get; set; }
        [DataMember]
        public DateTime DocDate { get; set; }
        [DataMember]
        public DateTime DelDate { get; set; }
        [DataMember]
        public int LineId { get; set; }
        [DataMember]
        public string FromLocation { get; set; }
        [DataMember]
        public string ToLocation { get; set; }
        [DataMember]
        public string Priority { get; set; }
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public bool Flag { get; set; }
        public List<int_stockrequestTypes> int_stockrequestTypesList { get; set; }
    }
}
