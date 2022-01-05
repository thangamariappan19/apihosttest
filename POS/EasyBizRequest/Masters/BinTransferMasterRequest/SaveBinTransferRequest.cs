using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.BinTransferMasterRequest
{
    [DataContract]
    [Serializable]
    public class SaveBinTransferRequest : BaseRequestType
    {
        public List<BinLogTypes> BinLogList { get; set; }
    }
}
