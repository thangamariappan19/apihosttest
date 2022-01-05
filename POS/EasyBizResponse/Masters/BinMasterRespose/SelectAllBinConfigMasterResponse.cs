using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.BinMasterRespose
{
    [DataContract]
    [Serializable]
    public class SelectAllBinConfigMasterResponse : BaseResponseType
    {
        [DataMember]
        public List<BinLevelMasterTypes> BinConfigMasterList { get; set; }
    }
}
