using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SKUMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectByIDSKUMasterResponse:BaseResponseType
    {
        [DataMember]
        public SKUMasterTypes SKUMasterTypesData { get; set; }

        [DataMember]
        public List<SKUMasterTypes> SKUMasterTypesList { get; set; }
    }
}
