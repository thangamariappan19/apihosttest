using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Coupons
{

    [Serializable]
    [DataContract]
    public class SelectByIDsCouponMasterRequest:BaseRequestType
    {

        [DataMember]
        public int IDs { get; set; }
    }
}
