using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.CardDetails
{
    [DataContract]
    [Serializable]
    public class SelectByIDCardDetailsRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
