using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.LanguageRequest
{
    [DataContract]
    [Serializable]
   public class UpdateLanguageRequest:BaseRequestType
    {
        [DataMember]
        public LanguageMaster LanguageMasterRecord { get; set; }
    }
}
