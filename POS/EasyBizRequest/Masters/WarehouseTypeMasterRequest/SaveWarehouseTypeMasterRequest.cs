using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.WarehouseTypeMasterRequest
{
    [DataContract]
    [Serializable]
    public class SaveWarehouseTypeMasterRequest: BaseRequestType
    {
        [DataMember]
        public WarehouseTypeMaster WarehouseTypMasterData { get; set; }
    }
}
