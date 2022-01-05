using EasyBizDBTypes.Transactions.POS.API_SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.API_SalesOrderResponse
{
    [DataContract]
    [Serializable]
    public class API_SelectALLSalesOrderResponse : BaseResponseType
    {
        [DataMember]
        public List<API_SalesOrderHeader> SalesOrderHeaderList { get; set; }
        [DataMember]
        public List<API_SalesOrderDetails> SalesOrderDetailList { get; set; }
    }
}
