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
    public class SaveInvoiceResponse : BaseResponseType
    {
        [DataMember]
        public long InvoiceHeaderID { get; set; }
    }
}
