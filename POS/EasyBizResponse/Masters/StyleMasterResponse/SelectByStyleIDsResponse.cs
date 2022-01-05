using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StyleMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectByStyleIDsResponse : BaseResponseType
    {
        [DataMember]

        public List<StyleMaster> StyleMasterList = new List<StyleMaster>();
    }
}
