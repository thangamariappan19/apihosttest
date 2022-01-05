using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StyleMasterResponse
{
    [DataContract]
    [Serializable]
   public class SelectAllStyleResponse:BaseResponseType
    {
        [DataMember]
         public List<StyleMaster> StyleList { get; set; }
        [DataMember]
        public List<StyleColorSizeType> styleColorSizeTypesList { get; set; }

    }
}
