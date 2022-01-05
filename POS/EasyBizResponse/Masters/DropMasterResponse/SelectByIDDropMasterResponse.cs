using EasyBizDBTypes.Masters;
using EasyBizResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.DropMasterResponse
{

    [DataContract]
    [Serializable]
    public class SelectByIDDropMasterResponse : BaseResponseType
    {
        [DataMember]
        public DropMasterTypes DropMasterTypesData { get; set; }
    }
}
