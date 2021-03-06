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
    public class SelectBinDetailsBySKUCodeRequest : BaseRequestType
    {
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string FromBin { get; set; }
    }
}
