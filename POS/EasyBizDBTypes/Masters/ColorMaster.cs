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
   public class ColorMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int ColorID { get; set; }
        [DataMember]
        public string InternalCode { get; set; }
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Shade { get; set; }
        [DataMember]
        public string NRFCode { get; set; }
        [DataMember]
        public int Colors { get; set; }
        [DataMember]
        public string Attribute1 { get; set; }
        [DataMember]
        public string Attribute2 { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int SCID { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string MultiColorImage { get; set; }

        public int UpdateFlag { get; set; }
        [DataMember]
        public int CreateBy { get; set; }
        [DataMember]
        public int UpdateBy { get; set; }
    }
}
