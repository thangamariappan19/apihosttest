using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ShiftMasterResponse
{
    [DataContract]
    [Serializable]
   public class SelectAllShiftResponse:BaseResponseType
    {
        [DataMember]

        public List<ShiftMaster> ShiftList { get; set; }
    }
}
