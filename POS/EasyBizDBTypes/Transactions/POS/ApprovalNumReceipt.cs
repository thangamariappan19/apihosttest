using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS
{
    [DataContract]
    [Serializable]
    public class ApprovalNumReceipt
    {
        [DataMember]
        public String CardType { get; set; }
        [DataMember]
        public String ApprovalNumber { get; set; }
    }
}
