using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Stocks.InventoryCounting
{
    [DataContract]
    [Serializable]
    public class InventoryCountingHeader : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocumentNumber { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public bool PostingDone { get; set; }
        [DataMember]
        public Nullable<DateTime> PostingDate { get; set; }
        [DataMember]
        public List<InventoryCountingDetails> InventoryCountingDetailList { get; set; }
        public string StoreCode { get; set; }
    }
}
