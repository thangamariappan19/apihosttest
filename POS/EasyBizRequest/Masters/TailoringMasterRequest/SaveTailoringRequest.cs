using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.TailoringMasterRequest
{
    [DataContract]
    [Serializable]

    public class SaveTailoringRequest : BaseRequestType
    {
        [DataMember]
        public TailoringMasterTypes TailoringMasterRecord { get; set; }
        [DataMember]
        public List<TailoringMasterTypes> TailoringMasterlist { get; set; }
    }
}
