using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POSOperations
{
    [DataContract]
    [Serializable]
    public class SalestargetDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public String StoreCode { get; set; }
        [DataMember]
        public int Qty { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public String Month { get; set; }       
    }
}
