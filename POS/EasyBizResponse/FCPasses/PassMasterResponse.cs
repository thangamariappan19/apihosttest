using EasyBizDBTypes.FCPasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.FCPasses
{
    [DataContract]
    [Serializable]
    public class PassMasterResponse: BaseResponseType
    {
        [DataMember]
        public PassMaster PassMasterResponseData { get; set; }
        [DataMember]
        public List<PassMaster> PassMasterResponseList { get; set; }
    }
}
