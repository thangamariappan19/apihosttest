using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.BinRequest
{
    [DataContract]
    [Serializable]
    public class SaveBinLevelMasterRequest : BaseRequestType
    {
        [DataMember]
        public BinLevelMasterTypes BinLevelMasterRecord { get; set; }
        [DataMember]
        public List<BinLevelMasterTypes> BinLevelMasterList { get; set; }
    }
}
