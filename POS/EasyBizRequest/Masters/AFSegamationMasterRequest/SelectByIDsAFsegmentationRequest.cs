using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.AFSegamationMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectByIDsAFsegmentationRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
