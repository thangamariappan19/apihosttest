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
   public class LanguageMaster:BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string LanguageCode { get; set; }
        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public string Remarks { get; set; }
        
    }
}
