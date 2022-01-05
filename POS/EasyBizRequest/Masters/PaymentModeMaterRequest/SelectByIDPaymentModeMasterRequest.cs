using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PaymentModeMaterRequest
{
    [DataContract]
    [Serializable]
    public class SelectByIDPaymentModeMasterRequest:BaseRequestType
    {
        [DataMember]

        public int ID { get; set; }
    }
}
