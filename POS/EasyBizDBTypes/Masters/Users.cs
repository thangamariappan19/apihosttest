using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class Users : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        
    }
}
