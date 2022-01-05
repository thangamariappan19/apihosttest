using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SKUMasterResponse
{
    [DataContract]
    [Serializable]
    public class SaveSKUMasterResponse:BaseResponseType
    {
        [DataMember]
        public string ReturnIDs { get; set; }
    }
}
