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
    public class SelectDesignDevelopmentOfficeLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<DesignDevelopmentOfficeTypes> DesignDevelopmentOfficeList { get; set; }
    }
}
