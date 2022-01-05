using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EasyBizDBTypes.Dashboard;
using EasyBizDBTypes.Transactions.POS;

namespace EasyBizResponse.Masters.DashboardReponse
{
    [DataContract]
    [Serializable]
    public class SelectDashboardResponse : BaseResponseType
    {


        [DataMember]
        public decimal SalesNetAmount { get; set; }
        [DataMember]
        public decimal ReturnAmount { get; set; }

        [DataMember]
        public List<Dashboard_AreaChart> AreaChart { get; set; }

        [DataMember]
        public List<Dashboard_Purchase> PurchaseChart { get; set; }

        [DataMember]
        public List<Dashboard_Product> PieChart { get; set; }
    }
}
