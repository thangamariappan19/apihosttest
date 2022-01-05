using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.OnAccountPaymentRequest
{
    [DataContract]
    public class SelectOnAccountPaymentRequest :BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }       
    }
}
