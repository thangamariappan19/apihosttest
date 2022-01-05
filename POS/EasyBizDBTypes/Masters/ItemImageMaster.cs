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
    public class ItemImageMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int StyleID { get; set; }
        [DataMember]
        public int SKUID { get; set; }

        //[DataMember]
        //public string ItemImage { get; set; }
        [DataMember]
        public int DesignID { get; set; }

        [DataMember]
        public bool IsDefaultImage { get; set; }

        [DataMember]
        public byte[] SKUImage { get; set; }

        [DataMember]
        public string StyleCode { get; set; }
       
    }
}
