using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.TransactionLogs;
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
    public class SelectByIDInvoiceResponse : BaseResponseType
    {
        [DataMember]
        public InvoiceHeader InvoiceHeaderData { get; set; }        
    }
}
