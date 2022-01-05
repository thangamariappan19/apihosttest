using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.DesignationMasterResponse
{
    
    
    [Serializable]
    [DataContract]
    public class SelectAllDesignationMasterResponse:BaseResponseType
    {
        [DataMember]
        public List<DesignationMaster> DesignationMasterList { get; set; }
    }
}
