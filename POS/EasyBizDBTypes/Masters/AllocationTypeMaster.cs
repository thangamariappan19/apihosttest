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
    public class AllocationTypeMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string AllocationTypeCode { get; set; }
        [DataMember]
        public string AllocationTypeName { get; set; }
        [DataMember]
        public string Description { get; set; }
        public string Remarks { get; set; }
    }
}
