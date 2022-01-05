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
    public class BrandMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int BrandID { get; set; }
        [DataMember]
        public int ScaleHeaderID { get; set; }
        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public string BrandName { get; set; }
        [DataMember]
        public string BrandLogo { get; set; }
        [DataMember]
        public string ARBName { get; set; }
        [DataMember]
        public string BrandType { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string ShortDescriptionName { get; set; }
        [DataMember]
        public List<SubBrandMaster> SubBrandList { get; set; }
        [DataMember]
        public int ScaleWithBrandID { get; set; }
        [DataMember]
        public int CreateBy { get; set; }
        [DataMember]
        public int UpdateBy { get; set; }
    }
}
