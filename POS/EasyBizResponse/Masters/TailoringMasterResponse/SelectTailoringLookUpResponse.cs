using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.TailoringMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectTailoringLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<TailoringMasterTypes> TailoringMasterList { get; set; }
    }
}
