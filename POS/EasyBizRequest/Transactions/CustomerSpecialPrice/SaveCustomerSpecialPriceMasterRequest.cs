using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CustomerSpecialPriceMasterRequest
{
    [Serializable]
    [DataContract]
    public class SaveCustomerSpecialPriceMasterRequest : BaseRequestType
    {
        [DataMember]
        public CustomerSpecialPriceMasterTypes CustomerSpecialPriceMasterRecord { get; set; }

        public List<StoreGroupMaster> CustomerSpecialPriceMasterList { get; set; }

        public List<CustomerMaster> CustomerMasterSpecialPriceMasterList { get; set; }

        public List<CommonUtil> CategoryCommonUtil { get; set; }
        public List<CommonUtil> StoreCommonUtil { get; set; }

    }
}
