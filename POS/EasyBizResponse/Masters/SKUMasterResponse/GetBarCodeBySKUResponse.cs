using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SKUMasterResponse
{
    [DataContract]
    [Serializable]
   public class GetBarCodeBySKUResponse : BaseResponseType
    {
        [DataMember]
        public SKUMasterTypes BarCodeData { get; set; }
    }
}
