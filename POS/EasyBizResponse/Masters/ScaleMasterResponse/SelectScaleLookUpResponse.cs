using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ScaleMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectScaleLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<ScaleMaster> ScaleList { get; set; }
    }
}
