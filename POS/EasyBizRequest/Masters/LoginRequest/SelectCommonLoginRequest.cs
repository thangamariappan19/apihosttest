using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.LoginRequest
{
    [DataContract]
    [Serializable]
    public class SelectCommonLoginRequest : BaseRequestType
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int UserID { get; set; }

    }
}
