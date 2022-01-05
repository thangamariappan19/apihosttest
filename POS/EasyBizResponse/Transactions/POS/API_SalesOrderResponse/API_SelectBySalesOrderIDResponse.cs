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
    public class API_SelectBySalesOrderIDResponse : BaseResponseType
    {
        public API_SalesOrderHeader SalesOrderMasterRecord { get; set; }
    }
}
