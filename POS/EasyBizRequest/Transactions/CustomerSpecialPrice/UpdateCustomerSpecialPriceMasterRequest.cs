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
    public class UpdateCustomerSpecialPriceMasterRequest : BaseRequestType
    {
         [DataMember]
        public CustomerSpecialPriceMasterTypes CustomerSpecialPriceMasterRecord { get; set; }
         [DataMember]
         public List<CommonUtil> CategoryCommonUtil { get; set; }
         
    }
}
