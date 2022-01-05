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
    public class API_SelectAllSalesOrderRequest : BaseRequestType
    {
        /*[DataMember]
        public string DataMode { get; set; }
        [DataMember]
        public long SalesOrderID { get; set; }
        [DataMember]
        public string SalesOrderNo { get; set; }*/
        [DataMember]
        public int StoreID { get; set; }
    }
}
