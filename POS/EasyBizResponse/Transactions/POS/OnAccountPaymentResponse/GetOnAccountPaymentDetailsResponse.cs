using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.OnAccountPaymentResponse
{
    [DataContract]
    [Serializable]
    public class GetOnAccountPaymentDetailsResponse : BaseResponseType
    {
        [DataMember]
        public OnAcInvoiceWisePayment OnAccountPaymentDetails { get; set; }
 
    }
}
