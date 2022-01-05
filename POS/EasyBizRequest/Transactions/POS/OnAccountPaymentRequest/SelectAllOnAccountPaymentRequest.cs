using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.OnAccountPaymentRequest
{
    [DataContract]
    public class SelectAllOnAccountPaymentRequest :BaseRequestType
    {                
        [DataMember]
        public string SearchString { get; set; }
    }
}
