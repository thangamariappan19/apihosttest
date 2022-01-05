using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.DiscountMasterRequest
{
    [Serializable]
    [DataContract]
   public class SelectAllDiscountMasterRequest : BaseRequestType
    {
        [DataMember]
        public int CustomerGroupID { get; set; }
        [DataMember]
        public string CustomerGroupCode { get; set; }       
    }
}
