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
    public class PassMasterRequest: BaseRequestType
    {
        [DataMember]
        public PassMaster PassMasterRequestData { get; set; }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string From { get; set; }
        [DataMember]
        public string To { get; set; }
        [DataMember]
        public string IsActive { get; set; }
        [DataMember]
        public string IsOTP { get; set; }
    }
}
