using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace EasyBizRequest.Transactions.POS.Invoice
{
    public class SelectNewXReportByDetailsRequest:BaseRequestType
    {
        [DataMember]
        public int CashierID { get; set; }

        [DataMember]
        public int ShiftID { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int POSID { get; set; }
    }
}
