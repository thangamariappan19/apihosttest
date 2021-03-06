using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.DivisionMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllDivisionResponse : BaseResponseType
    {
        [DataMember]
        public List<DivisionMaster> DivisionList { get; set; }
    }
}
