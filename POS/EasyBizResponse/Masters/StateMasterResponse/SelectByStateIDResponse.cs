using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StateMasterResponse
{
    [DataContract]
    [Serializable]
   public class SelectByStateIDResponse : BaseResponseType
    {
        [DataMember]
        public StateMaster StateMasterRecord { get; set; }
    }
}
