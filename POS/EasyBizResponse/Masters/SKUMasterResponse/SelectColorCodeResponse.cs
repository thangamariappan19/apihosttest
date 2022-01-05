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
    public class SelectColorCodeResponse : BaseResponseType
    {
        [DataMember]
        public List<SKUMasterTypes> SKUMasterTypesList { get; set; }
    }
}
