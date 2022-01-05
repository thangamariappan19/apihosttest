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
    public class ScaleMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int ScaleID { get; set; }
        [DataMember]
        public int SizeID { get; set; }
        [DataMember]
        public string ScaleCode { get; set; }
        [DataMember]
        public string ScaleName { get; set; }
         [DataMember]
        public string InternalCode { get; set; }
         [DataMember]
        public string VisualOrder { get; set; }
        [DataMember]
        public List<ScaleDetailMaster> ScaleDetailMasterList { get; set; }
         [DataMember]
         public bool ApplytoAll { get; set; }
        [DataMember]
        public List<BrandMaster> BrandMasterList { get; set; }
        [DataMember]
        public int UpdateFlag { get; set; }
    }
}
