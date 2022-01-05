using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.WarehouseTypeMasterRequest
{
    [Serializable]
    [DataContract]
    public class UpdateWarehouseTypeMasterRequest : BaseRequestType
    {
        [DataMember]
        public WarehouseTypeMaster WarehouseTypeMasterData { get; set; }
    }
}
