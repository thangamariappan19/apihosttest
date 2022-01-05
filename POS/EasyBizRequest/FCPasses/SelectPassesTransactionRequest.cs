using EasyBizDBTypes.FCPasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.FCPasses
{
    [DataContract]
    [Serializable]
    public class SelectPassesTransactionRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
