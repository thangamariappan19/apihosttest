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
    public class ProductGroupMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ProductGroupCode { get; set; }
        [DataMember]
        public string ProductGroupName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<ProductSubGroupMaster> ProductSubGroupList { get; set; }
        public int UpdateFlag { get; set; }
        [DataMember]
        public int CreateBy { get; set; }
        [DataMember]
        public int UpdateBy { get; set; }
    }
}
