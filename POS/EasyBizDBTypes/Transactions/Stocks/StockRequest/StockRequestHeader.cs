using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.StockRequest
{
    [DataContract]
    [Serializable]
    public class StockRequestHeader : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public int TotalQuantity { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int FromStore { get; set; }
        [DataMember]
        public int ToStore { get; set; }
        [DataMember]
        public int WareHouseID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public List<StockRequestDetails> StockRequestDetailsList { get; set; }
        [DataMember]
        public string StoreCode { get; set; }

    }
}
