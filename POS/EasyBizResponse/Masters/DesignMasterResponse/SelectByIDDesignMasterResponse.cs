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
    public class SelectByIDDesignMasterResponse : BaseResponseType
    {
        [DataMember]
        public DesignMasterTypes DesignMasterTypesData { get; set; }
        public List<DesignMasterTypes> DesignMasterTypesList = new List<DesignMasterTypes>();
       
    }
}
