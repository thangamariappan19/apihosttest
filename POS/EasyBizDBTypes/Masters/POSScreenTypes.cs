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
    public class POSScreenTypes : BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string UId { get; set; }
        [DataMember]
        public bool IsBackOffice { get; set; }
        [DataMember]
        public bool IsStore { get; set; }
       
    }
}
