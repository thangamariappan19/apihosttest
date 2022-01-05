using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.DesignMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectByIDsDesignMasterResponse : BaseResponseType
    {
        [DataMember]

        public List<DesignMasterTypes> DesignMasterTypesList = new List<DesignMasterTypes>();
    }
}
