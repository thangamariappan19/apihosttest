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
    public class SelectAllUsersRequest :BaseRequestType
    {
        [DataMember]
        public int StoreID { get; set; }
        //[DataMember]
        //public List<Users> UsersList { get; set; }

    }
}
