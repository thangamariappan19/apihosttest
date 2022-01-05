using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.WarehouseMasterRequest
{
    [Serializable]
    [DataContract]  
    public class UpdateWarehouseMasterRequest: BaseRequestType
    {
        [DataMember]
        public WarehouseMaster WarehouseMasterData { get; set; }
    }
}
