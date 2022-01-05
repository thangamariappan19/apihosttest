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
    public class SelectOnAccountPaymentResponse : BaseResponseType
    {
        [DataMember]
        public OnAccountPayment OnAccountPaymentRecord { get; set; }
    }
}
