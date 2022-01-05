using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.WebOrderSalesRequest
{
    [DataContract]
    [Serializable]
    public class WebSalesOrderRequest : BaseRequestType
    {
 
        [DataMember]
        public WebSalesOrderHeader WebOrderHeaderList { get; set; }
        [DataMember]
        public WebSalesOrderDetails WebOrderDetailsList { get; set; }
        [DataMember]
        public List<WebSalesOrderHeader> WebSalesHeaderDetails { get; set; }
    }
}

