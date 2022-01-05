using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.Invoice
{
    public class SelectInvoiceReceiptByInvoiceNumRequest : BaseRequestType
    {
        [DataMember]
        public String InvoiceNum { get; set; }

        [DataMember]
        public string ReturnInvoiceNo { get; set; }
    }
}
