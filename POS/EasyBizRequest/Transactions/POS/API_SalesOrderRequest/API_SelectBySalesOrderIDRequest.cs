using EasyBizDBTypes.Transactions.POS.API_SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.API_SalesOrderRequest
{
    [DataContract]
    [Serializable]
    public class API_SelectBySalesOrderIDRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public int StoreID { get; set; }
    }
}
