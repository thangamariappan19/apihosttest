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
    public class SelectAllPaymentModeMasterRequest:BaseRequestType
    {
        [DataMember]
        public string SearchString { get; set; }

        public string CustomerInfo { get; set; }

        public int ID { get; set; }

        public string Source { get; set; }
    }
}
