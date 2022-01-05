using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StyleMasterResponse
{
    [DataContract]
    [Serializable]
    public class SaveStyleResponse : BaseResponseType
    {
        [DataMember]
        public string ReturnIDs { get; set; }
    }
}
