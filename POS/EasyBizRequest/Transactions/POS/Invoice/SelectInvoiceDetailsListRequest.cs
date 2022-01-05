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
   public class SelectInvoiceDetailsListRequest:BaseRequestType
    {
        [DataMember]
        public long InvoiceHeaderID { get; set; }        
        [DataMember]
        public string SearchString { get; set; }
        [DataMember]
        public string SalesStatus { get; set; }

        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public bool ForceSKUSearch { get; set; }
    }
}
