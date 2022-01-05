using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POSOperations
{
    [DataContract]
    [Serializable]
    public class CashInCashOutReportDetails: BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int SerialNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public Decimal Total { get; set; }
        [DataMember]
        public Decimal ReceivedAmount { get; set; }
        [DataMember]
        public Decimal PaidAmount { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string CategoryType { get; set; }
        [DataMember]
        public string ReasonName { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public int ReasonID { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public DateTime ApplicationDate { get; set; }
        [DataMember]
        public int POSID { get; set; }
        [DataMember]
        public int ShiftID { get; set; }
        [DataMember]
        public string ShiftCode { get; set; }
        [DataMember]
        public string POSCode { get; set; }
        [DataMember]
        public string Type { get; set; }




    }
}
