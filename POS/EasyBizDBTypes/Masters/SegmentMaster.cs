using System;
using EasyBizDBTypes.Common;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class SegmentMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string SegmentName { get; set; }
        [DataMember]
        public int MaxLength { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Boolean IsUsed { get; set; }
        [DataMember]
        public Boolean DefaultDescription { get; set; }
        [DataMember]
        public int SegmentHeaderID { get; set; }
    }
}
