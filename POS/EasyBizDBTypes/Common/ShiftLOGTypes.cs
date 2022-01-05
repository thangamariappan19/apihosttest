using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Common
{
    [DataContract]
    [Serializable]
    public class ShiftLOGTypes
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int POSID { get; set; }
        [DataMember]
        public long ShiftInUserID { get; set; }
        [DataMember]
        public long ShiftOutUserID { get; set; }
        [DataMember]
        public int ShiftID { get; set; }

        [DataMember]
        public DateTime BusinessDate { get; set; }       
        [DataMember]
        public DateTime ShiftInDateTime { get; set; }
        [DataMember]
        public DateTime ShiftOutDateTime { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public Decimal CardAmount { get; set; }
        [DataMember]
        public Decimal ShiftAmount { get; set; }
        [DataMember]
        public Decimal ReturnAmount { get; set; }
         [DataMember]
        public Decimal SalesRetunAmount { get; set; }

        
        [DataMember]
        public Decimal CashInAmount { get; set; }
        [DataMember]
        public Decimal CashOutAmount { get; set; }

        [DataMember]
        public Decimal GrandTotal { get; set; }
         [DataMember]
        public Decimal CashIn { get; set; }
         [DataMember]
         public Decimal CashOut { get; set; }
         [DataMember]
         public string CountryCode { get; set; }
         [DataMember]
         public string StoreCode { get; set; }
         [DataMember]
         public string ShiftCode { get; set; }
         [DataMember]
         public string ShiftInUserCode { get; set; }
         [DataMember]
         public string ShiftInOutUserCode { get; set; }
         [DataMember]
         public string PosCode { get; set; }
    }
}
