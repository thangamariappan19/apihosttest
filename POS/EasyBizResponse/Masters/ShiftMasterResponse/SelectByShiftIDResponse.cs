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
   public class SelectByShiftIDResponse: BaseResponseType
    {
        public EasyBizDBTypes.Masters.ShiftMaster ShiftRecord { get; set; }

        public List<EasyBizDBTypes.Masters.ShiftMaster> ShiftList { get; set; }
    }
}
