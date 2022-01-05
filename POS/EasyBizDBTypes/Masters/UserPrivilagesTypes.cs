using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [Serializable]
    [DataContract]
    public class UserPrivilagesTypes : BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public long RoleID { get; set; }
        [DataMember]
        public string ScreenName { get; set; }
        [DataMember]
        public bool IsBackOffice { get; set; }
        [DataMember]
        public bool IsStore { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public int CreateBy { get; set; }
    }
}
