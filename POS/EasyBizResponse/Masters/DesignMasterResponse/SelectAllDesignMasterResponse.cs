using EasyBizDBTypes.Masters;
using EasyBizResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.DesignMasterResponse
{

    [DataContract]
    [Serializable]
    public class SelectAllDesignMasterResponse : BaseResponseType
    {
        [DataMember]
        public List<DesignMasterTypes> DesignMasterTypesList { get; set; }
    }
}
