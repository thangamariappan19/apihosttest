using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Common
{
    [DataContract]
    [Serializable]
   public class SelectShiftLogResponse : BaseResponseType
    {
        [DataMember]
        public ShiftLOGTypes ShiftTypesData { get; set; }
        [DataMember]
        public ShiftMaster MaxShiftTypesData { get; set; }
        [DataMember]
        public ShiftMaster MaxShiftTypesData1 { get; set; }

        [DataMember]
        public List<ShiftLOGTypes> ShiftLOGTypesList { get; set; }
        [DataMember]
        public List<ShiftLOGTypes> AllShiftLOGTypesList { get; set; }
        [DataMember]
        public List<ShiftMaster> AllShiftLOGandTypesList { get; set; }
        [DataMember]
        public List<ShiftMaster> DayInEnabledList { get; set; }
        [DataMember]
        public List<ShiftMaster> MaxShiftList { get; set; }
        [DataMember]
        public ShiftLOGTypes ShiftAmount { get; set; }
        [DataMember]
        public Boolean Dayout { get; set; }
    }
}
