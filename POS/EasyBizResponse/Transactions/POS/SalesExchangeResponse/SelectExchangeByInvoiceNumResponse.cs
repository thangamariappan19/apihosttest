using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.SalesExchangeResponse
{
    [DataContract]
    [Serializable]
    public class SelectExchangeByInvoiceNumResponse : BaseResponseType
    {
        [DataMember]
        public List<ExchangeReceipt> ExchangeReceiptList { get; set; }
    }
}
