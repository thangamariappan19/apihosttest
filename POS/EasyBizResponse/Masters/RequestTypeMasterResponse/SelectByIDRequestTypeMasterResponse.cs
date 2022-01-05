using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using EasyBizDBTypes.Masters;

namespace EasyBizResponse.Masters.RequestTypeMasterResponse
{
    [Serializable]
    [DataContract]
    public   class SelectByIDRequestTypeMasterResponse:BaseResponseType
    {
        [DataMember]
        public RequestTypeMaster RequestTypeMasterRecord { get; set; }
    }
}
