using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.PrevilegesResponse
{
    [Serializable]
    [DataContract]
    public class SelectPrevilegesLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<UserPrivilagesTypes> UserPrivilagesTypesList { get; set; }
    }
}
