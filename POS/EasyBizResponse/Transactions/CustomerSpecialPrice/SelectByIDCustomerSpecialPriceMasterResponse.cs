using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CustomerSpecialPriceMasterResponse
{
    [DataContract]
    [Serializable]
  public  class SelectByIDCustomerSpecialPriceMasterResponse:BaseResponseType
    {
         [DataMember]
        public CustomerSpecialPriceMasterTypes CustomerSpecialPriceMasterRecord { get; set; }
         public List<CustomerSpecialPriceMasterTypes> CustomerSpecialPriceMasterdata { get; set; }
    }
}
