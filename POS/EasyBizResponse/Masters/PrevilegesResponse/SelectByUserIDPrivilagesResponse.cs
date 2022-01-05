using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.PrevilegesResponse
{
    [DataContract]
    [Serializable]
    public class SelectByUserIDPrivilagesResponse : BaseResponseType
    {
        [DataMember]
        public UserPrivilagesTypes MASUserPrivilagesRecord { get; set; }
    }
}
