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
    public class AFSegamationMasterTypes : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int AFSegamationCode { get; set; }
        [DataMember]
        public string AFSegamationName { get; set; }
        [DataMember]
        public string StyleName { get; set; }
        [DataMember]
        public string ColorName { get; set; }
        [DataMember]
        public string ScaleName { get; set; }
        [DataMember]
        public string Size { get; set; }
        [DataMember]
        public Boolean StyleUse { get; set; }
        [DataMember]
        public int StyleMaxLength { get; set; }
        [DataMember]
        public Boolean StyleDefDescription { get; set; }
        [DataMember]
        public Boolean ColorUse { get; set; }
        [DataMember]
        public int ColorMaxLength { get; set; }
        [DataMember]
        public Boolean ColorDefDescription { get; set; }
        [DataMember]
        public Boolean ScaleUse { get; set; }
        [DataMember]
        public int ScaleMaxLength { get; set; }
        [DataMember]
        public Boolean ScaleDefDescription { get; set; }
        [DataMember]
        public Boolean Use { get; set; }
        [DataMember]
        public string SegmentName { get; set; }
        [DataMember]
        public string MaxLength { get; set; }
        [DataMember]
        public Boolean DefaultDescription { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int CodeLength { get; set; }
        [DataMember]
        public string UseSeperator { get; set; }
        [DataMember]
        public List<SegmentMaster> SegmentList { get; set; }
    }
}
