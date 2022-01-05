using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EasyBizDBTypes.Transactions.POS
{
    [DataContract]
    [Serializable]
    public class DashboardTypes
    {
        [DataMember]
        public DateTime FromDate { get; set; }
        [DataMember]
        public DateTime ToDate { get; set; }
        [DataMember]
        public Decimal SalesAmount { get; set; }
        [DataMember]
        public Decimal ReturnAmount { get; set; }
        [DataMember]
        public int country_id { get; set; }

    }
}
