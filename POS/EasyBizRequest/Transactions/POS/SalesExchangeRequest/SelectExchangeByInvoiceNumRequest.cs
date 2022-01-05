using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.SalesExchangeRequest
{
    public class SelectExchangeByInvoiceNumRequest : BaseRequestType
    {
        [DataMember]
        public String InvoiceNum { get; set; }
    }
}
