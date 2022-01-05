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
    public class Int_ConfirmTransfer : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocNum { get; set; }
        [DataMember]
        public DateTime DocDate { get; set; }
        [DataMember]
        public DateTime applicationDate { get; set; }
        [DataMember]
        public DateTime DelDate { get; set; }
        [DataMember]
        public int LineId { get; set; }
        [DataMember]
        public string FromLocation { get; set; }
        [DataMember]
        public string ToLocation { get; set; }
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        public List<Int_ConfirmTransfer> Int_ConfirmTransferList { get; set; }
        public string StoreCode { get; set; }
        [DataMember]
        public int Qunatity { get; set; }
        [DataMember]
        public int ConfirmedQty { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
