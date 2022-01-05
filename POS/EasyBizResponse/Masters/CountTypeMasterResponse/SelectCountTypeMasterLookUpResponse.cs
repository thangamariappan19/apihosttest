using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CountTypeMasterResponse
{
    [Serializable]
    [DataContract]
   public  class SelectCountTypeMasterLookUpResponse: BaseResponseType
    {
        [DataMember]
        public List<CountTypeMaster> CountTypeMasterList { get; set; }
    }
}
