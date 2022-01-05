using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StyleStatusMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectStyleStatusLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<StyleStatusMasterType> StyleStatusMasterTypeList { get; set; }
    }
}
