using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.Invoice
{
    [DataContract]
    [Serializable]
   public class SelectInvoiceLookupResponse:BaseResponseType
    {
        [DataMember]
        public List<InvoiceHeader> InvoiceHeaderLookupList { get; set; }
    }
}
