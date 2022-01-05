using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.TailoringMasterRequest
{
    [DataContract]
    [Serializable]
    public class SelectTailoringLookUpRequest : BaseRequestType
    {
        [DataMember]
        public int TailoringID { get; set; }
        [DataMember]
        public string TailoringName { get; set; }
    }
}
