using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ScaleMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectAllScaleDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<ScaleDetailMaster> ScaleDetailMasterList { get; set; }
    }
}
