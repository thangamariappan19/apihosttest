using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.Invoice
{
    [DataContract]
    [Serializable]
    public class SelectByIDSalesReturnRequest:BaseRequestType
    {
        [DataMember]
        public long ID { get; set; }
    }
}
