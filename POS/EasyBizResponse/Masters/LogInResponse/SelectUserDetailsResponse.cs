using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.LogInResponse
{
    [DataContract]
    [Serializable]
    public class SelectUserDetailsResponse : BaseResponseType
    {
        [DataMember]
        //public UserDetailsInfo UsersInfoRecord { get; set; }
        public UserDetailsInfo UserInfoRecord { get; set; }
    }
}
