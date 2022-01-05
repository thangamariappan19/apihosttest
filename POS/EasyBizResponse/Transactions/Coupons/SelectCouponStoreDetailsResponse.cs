using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CouponMasterResponse
{
    public class SelectCouponStoreDetailsResponse:BaseResponseType
    {
        [DataMember]
        public List<StoreMaster> StoreMasterData { get; set; }

        public List<StoreGroupMaster> StoreGroupMasterData { get; set; }


        [DataMember]
        public List<CommonUtil> StoreCommonUtil { get; set; }

        [DataMember]
        public List<CommonUtil> DetailsRecord { get; set; }

    }
}
