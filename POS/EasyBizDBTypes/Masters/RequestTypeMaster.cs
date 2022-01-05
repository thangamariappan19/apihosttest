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
    public class RequestTypeMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string RequestTypeCode { get; set; }
        [DataMember]
        public string RequestTypeName { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
  
   
}
