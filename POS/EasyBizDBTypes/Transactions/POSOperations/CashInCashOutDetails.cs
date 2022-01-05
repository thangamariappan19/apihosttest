using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.PaymentDetails
{
    [DataContract]
    [Serializable]
    public class CashInCashOutDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int HeaderID { get; set; }
        [DataMember]
        public DateTime ApplicationDate { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public int Reason { get; set; }
        [DataMember]
        public int ReasonID { get; set; }
        [DataMember]
        public Decimal ReceivedAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public List<ReasonMaster> ReasonMasterList { get; set; }
        [DataMember]
        public int POSID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int ShiftID { get; set; }
        [DataMember]
        public string ShiftCode { get; set; }
        [DataMember]
        public string POSCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
    }
}
