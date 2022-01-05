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
     public class BrandDivisionTypes : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public long BrandID { get; set; }

        [DataMember]
        public string DivisionCode { get; set; }

        [DataMember]
        public string DivisionName { get; set; }

        [DataMember]

        public int DivisionID { get; set; }

        [DataMember]
        public List<BrandDivisionTypes> BrandDivisionList { get; set; }

        [DataMember]
        public string BrandCode { get; set; }

    }
}
