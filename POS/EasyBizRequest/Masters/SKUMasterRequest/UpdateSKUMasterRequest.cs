using EasyBizDBTypes.Masters;
using EasyBizRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SKUMasterRequest
{
    [Serializable]
    [DataContract]
    public class UpdateSKUMasterRequest : BaseRequestType
    {
        public SKUMasterTypes SKUMasterTypesRecord { get; set; }
    }
}
