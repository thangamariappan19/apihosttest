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
  public class DocumentTypes:BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string DocumentCode { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
