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
    public class FreightMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string FreightCode { get; set; }
        [DataMember]
        public string FreightName { get; set; }
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Remarks { get; set; }
    }
   
}
