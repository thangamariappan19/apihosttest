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
    public class StyleColorSizeType
    {
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string Type { get; set; }
    }
}
