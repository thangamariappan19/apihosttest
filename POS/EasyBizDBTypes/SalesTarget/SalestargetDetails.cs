using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.SalesTarget
{
    [DataContract]
    [Serializable]
    public class SalestargetDetails
    {      
         [DataMember]
        public Int64 SalesQty { get; set; }
         [DataMember]
         public Decimal salesamount { get; set; }
         [DataMember]
         public String Month { get; set; }
    }
}
