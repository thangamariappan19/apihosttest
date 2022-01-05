using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Stocks.StockReturn
{
    [DataContract]
    [Serializable]
    public class int_stockreturn : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocNum { get; set; }
        [DataMember]
        public DateTime DocDate { get; set; }
        [DataMember]
        public int LineId { get; set; }
        [DataMember]
        public string FromLocation { get; set; }
        [DataMember]
        public string ToLocation { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public int SKUID { get; set; }
        [DataMember]
        public string SKUName { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public bool Flag { get; set; }
        [DataMember]
        public int OpenQty { get; set; }

        public List<int_stockreturn> int_stockreturnList { get; set; }
    }
}
