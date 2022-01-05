using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.SalesOrder
{
    [DataContract]
    [Serializable]
    public class SelectAllSalesOrderRequest : BaseRequestType
    {
        [DataMember]
        public string DataMode { get; set; }
    }

    [DataContract]
    [Serializable]
    public class SelectSalesOrderDetailRequest : BaseRequestType
    {
        [DataMember]
        public long SalesOrderID { get; set; }

        [DataMember]
        public string SalesOrderNo { get; set; }
    }
}
