using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.UsersResponse
{   
    [DataContract]
    [Serializable]
   public class UpdateUsersResponse : BaseResponseType
    {
        [DataMember]
        public UsersSettings UsersRecord { get; set; }
    }
}
