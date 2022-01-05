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
    public class SelectAllTailoringResponse : BaseResponseType
    {
        [DataMember]
        public List<TailoringMasterTypes> TailoringMasterList { get; set; }
    }
}
