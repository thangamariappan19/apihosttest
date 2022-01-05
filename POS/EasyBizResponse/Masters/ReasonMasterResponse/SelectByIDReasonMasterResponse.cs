using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ReasonMasterResponse
{   
    [Serializable]
    [DataContract]
    public class SelectByIDReasonMasterResponse : BaseResponseType
    {
        [DataMember]
        public ReasonMaster ReasonMasterRecord { get; set; }
    }
}
