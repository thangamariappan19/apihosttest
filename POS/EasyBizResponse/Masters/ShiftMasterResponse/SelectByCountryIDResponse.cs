using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ShiftResponse
{
    [DataContract]
    [Serializable]
    public class SelectByCountryIDResponse : BaseResponseType
    {
        [DataMember]
        public ShiftMaster ShiftRecord { get; set; }
        public List<ShiftMaster> ShiftMasterList { get; set; }
    }
}
