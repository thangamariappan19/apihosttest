using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StyleMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectColorDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<ColorMaster> StyleWithColorDetailsRecord { get; set; }
    }
}
