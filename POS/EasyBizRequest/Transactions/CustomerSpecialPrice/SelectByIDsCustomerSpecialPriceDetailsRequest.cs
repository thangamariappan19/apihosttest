using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.CustomerSpecialPrice
{
    [Serializable]
    [DataContract]
    public class SelectByIDsCustomerSpecialPriceDetailsRequest : BaseRequestType
    {
        [DataMember]
        public string IDs { get; set; }
    }
}
