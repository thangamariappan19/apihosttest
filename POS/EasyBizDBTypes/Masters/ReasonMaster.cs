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
    public class ReasonMaster : BaseType
    {
        [DataMember]
        public int ReasonID { get; set; }
        [DataMember]
        public string ReasonCode { get; set; }
        [DataMember]
        public string ReasonName { get; set; }
        [DataMember]
        public string Description { get; set; }
    } 
  
}
