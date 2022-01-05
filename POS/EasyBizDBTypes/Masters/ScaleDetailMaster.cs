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
    public class ScaleDetailMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int SizeID { get; set; }
        [DataMember]
        public int ColorID { get; set; }
        [DataMember]
        public int ScaleHeaderID { get; set; }
        [DataMember]
        public string ScaleCode { get; set; }
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string SizeCode { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int VisualOrder { get; set; }

        [DataMember]
        public string SizeName { get; set; }

        [DataMember]
        public int SSID { get; set; } // For Grid Edit Puropse.Beacuse id will be conflict
    }
}
