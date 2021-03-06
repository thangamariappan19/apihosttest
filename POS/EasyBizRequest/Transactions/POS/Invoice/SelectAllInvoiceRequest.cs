using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.Invoice
{
    [DataContract]
    [Serializable]
    public class SelectAllInvoiceRequest : BaseRequestType
    {
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }

        [DataMember]
        public string SalesStatus { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public string SearchString { get; set; }

        [DataMember]
        public string SearchCriteria { get; set; }

        [DataMember]
        public bool GetHoldRrecordsToo { get; set; }
    }
}
