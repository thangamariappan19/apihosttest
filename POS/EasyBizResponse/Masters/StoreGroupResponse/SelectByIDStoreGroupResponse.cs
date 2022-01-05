using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StoreGroupResponse
{
    [Serializable]
    [DataContract]
    public class SelectByIDStoreGroupResponse:BaseResponseType
    {
        [DataMember]
        public StoreGroupMaster StoreGroupMasterRecord { get; set; }


    }
}
