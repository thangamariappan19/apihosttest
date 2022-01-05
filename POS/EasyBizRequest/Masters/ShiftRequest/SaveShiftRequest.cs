using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ShiftRequest
{
    [DataContract]
    [Serializable]
    public class SaveShiftRequest : BaseRequestType
    {
        [DataMember]
        public List<ShiftMaster> Shiftlist { get; set; }
        [DataMember]
        public ShiftMaster ShiftMasterRecord { get; set; }
    }
}
