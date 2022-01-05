using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.WarehouseTypeMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectAllWarehouseTypeMasterResponse : BaseResponseType
    {
        [DataMember]
        public List<WarehouseTypeMaster> WarehouseTypeMasterList { get; set; }
    }
}
