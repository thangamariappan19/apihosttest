using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.YearMasterResponse
{ 
    [DataContract]
    [Serializable]
    public class SelectByYearIDResponse : BaseResponseType
    {
        [DataMember]
        public YearMaster YearRecord { get; set; }
    }
}
