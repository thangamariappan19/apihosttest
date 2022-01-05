using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Reports.DayWiseTransaction
{
    [DataContract]
    [Serializable]
    public class SalesReturnHeaderTransaction : BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int PosID { get; set; }
        [DataMember]
        public String SalesReturnNo { get; set; }
        [DataMember]
        public String InvNo { get; set; }
        [DataMember]
        public int TotalQty { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public String BusinessDate { get; set; }
        [DataMember]
        public Decimal ReturnAmount { get; set; }
        [DataMember]    
        public string PosName { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public string CustomerName { get; set; } 
        [DataMember]
        public String EmployeeName { get; set; }    
    }
}
