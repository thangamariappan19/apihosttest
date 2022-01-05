using EasyBizDBTypes.Common;
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
   public class CashInCashOutMaster: BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public Decimal Total { get; set; }
        [DataMember]
        public List<CashInCashOutDetails> CashInCashOutDetailsList { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string POSCode { get; set; }
        [DataMember]
        public string ShiftCode { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int ShiftID { get; set; }
        [DataMember]
        public int POSID { get; set; }
       
    }
}
