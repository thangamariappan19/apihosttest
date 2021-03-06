using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.PaymentModeMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllPaymentModeMasterResponse:BaseResponseType
    {
        [DataMember]
        public List<PaymentModeTypes> PaymentModeMasterDate { get; set; }
    }
}
