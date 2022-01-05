using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.BinSubLevelResponse
{
    [DataContract]
    [Serializable]
    public class SelectByIDBinLevelDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<BinLevelDetailsTypes> BinSubLevelList { get; set; }
    }
}
