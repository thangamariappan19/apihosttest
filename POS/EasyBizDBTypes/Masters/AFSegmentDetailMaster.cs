using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    public class AFSegmentDetailMaster : BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public int SegmentHeaderID { get; set; }
        [DataMember]
        public bool IsUsed { get; set; }
        [DataMember]
        public string SegmentName { get; set; }
        [DataMember]
        public int MaxLength { get; set; }
        [DataMember]
        public bool DefaultDescription { get; set; }

           }
}
