using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EasyBizDBTypes.Combo
{
    [DataContract]
    [Serializable]
    public class ComboHeader : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string ProductBarcode { get; set; }
        [DataMember]
        public string ProductSKU { get; set; }
        [DataMember]
        public string SKUBarcode { get; set; }

    }
}
