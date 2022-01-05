using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.WarehouseMasterRequest
{
    [DataContract]
    [Serializable]
    public class SaveWarehouseMasterRequest: BaseRequestType
    {
        [DataMember]
        public WarehouseMaster WarehouseMasterData { get; set; }
    }
}
