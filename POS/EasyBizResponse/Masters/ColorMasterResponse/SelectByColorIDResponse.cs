using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ColorMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectByColorIDResponse : BaseResponseType
    {
        [DataMember]
        public ColorMaster ColorRecord { get; set; }
    }
}
