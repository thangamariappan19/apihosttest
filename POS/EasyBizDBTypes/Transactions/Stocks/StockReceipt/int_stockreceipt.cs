using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Stocks.StockReceipt
{

    [DataContract]
    [Serializable]
    public class int_stockreceipt : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocNum { get; set; }
        [DataMember]
        public DateTime DocDate { get; set; }
        [DataMember]
        public DateTime ApplicationDate { get; set; }
        [DataMember]
        public DateTime DelDate { get; set; }
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
        public string Brand { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]
        public string Size { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public int FromStoreID { get; set; }
        public List<int_stockreceipt> int_stockreceiptList { get; set; }
        public List<int_stockreceipt> int_stockreceiptConfirmTransfer { get; set; }
        public string StoreCode { get; set; }
        [DataMember]
        public int RequestQuantity { get; set; }
        [DataMember]
        public int ReceivedQuantity { get; set; }
        [DataMember]
        public int TransferQuantity { get; set; }
        [DataMember]
        public int DifferenceQuantity { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Basedocument { get; set; }
        [DataMember]
        public string BaseDocKey { get; set; }
        [DataMember]
        public string BaseDocNum { get; set; }
        [DataMember]
        public bool Flag { get; set; }
        [DataMember]
        public bool TransferToPos { get; set; }
        [DataMember]
        public string BasDocNum { get; set; }
        [DataMember]
        public int WMSReqKey { get; set; }
        [DataMember]
        public string StkRecDocNum { get; set; }
        [DataMember]
        public string DocType { get; set; }
    }
}
