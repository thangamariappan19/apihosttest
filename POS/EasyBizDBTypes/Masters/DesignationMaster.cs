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
    public class DesignationMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DesignationCode { get; set; }
        [DataMember]
        public string DesignationName { get; set; }
        [DataMember]
        public int RoleId { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string RoleCode { get; set; }


    }
}
