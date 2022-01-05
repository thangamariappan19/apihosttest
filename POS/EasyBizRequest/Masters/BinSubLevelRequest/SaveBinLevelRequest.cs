using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.BinSubLevelRequest
{
    [DataContract]
    [Serializable]
    public class SaveBinLevelRequest : BaseRequestType
    {
        [DataMember]
        public BinLevelDetailsTypes BinLevelDetailsRecord { get; set; }
        [DataMember]
        public List<BinLevelDetailsTypes> BinLevelDetailsList { get; set; }
    }
}
