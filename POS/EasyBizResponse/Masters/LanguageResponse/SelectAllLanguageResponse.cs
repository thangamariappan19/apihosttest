using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.LanguageResponse
{
    [DataContract]
    [Serializable]
   public class SelectAllLanguageResponse:BaseResponseType
    {
        [DataMember]
        public List<LanguageMaster> LanguageMasterList { get; set; }
    }
}
