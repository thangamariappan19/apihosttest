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
    public class ProductSubGroupMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string ProductSubGroupCode { get; set; }

        [DataMember]
        public string ProductSubGroupName { get; set; }

        [DataMember]
        public long ProductGroupID { get; set; }
        [DataMember]
        public string ProductGroupName { get; set; }        
        [DataMember]
        public List<ProductSubGroupMaster> ProductSubGrouplist { get; set; }
    }
}
