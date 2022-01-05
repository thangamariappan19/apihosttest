using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.UsersRequest
{
    [DataContract]
    [Serializable]
    public class UpdateUsersRequest : BaseRequestType

    {
        [DataMember]
        public UsersSettings UsersRecord { get; set; }

        //[DataMember]
        //public string UserName { get; set; }

        //[DataMember]
        //public string CurrentPassword { get; set; }
        //[DataMember]
        //public string ConfirmPassword { get; set; }
    }
}
