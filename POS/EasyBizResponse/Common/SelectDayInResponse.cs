using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EasyBizDBTypes.Masters;

namespace EasyBizResponse.Common
{
    [DataContract]
    [Serializable]
    public class SelectDayInResponse : BaseResponseType
    {
        [DataMember]
        public List<ShiftMaster> ShiftList { get; set; }
        [DataMember]
        public List<PosMaster> POSList { get; set; }
        [DataMember]
        public Boolean DayIn { get; set; }
        [DataMember]
        public Boolean ShiftIn { get; set; }
        [DataMember]
        public List<ShiftMaster> ShiftMasterList { get; set; }
        [DataMember]
        public ShiftMaster LogShiftList { get; set; }
    }
}
