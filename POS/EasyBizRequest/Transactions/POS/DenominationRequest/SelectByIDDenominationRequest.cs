using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.DenominationRequest
{
    [DataContract]
    [Serializable]
    public class SelectByIDDenominationRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
