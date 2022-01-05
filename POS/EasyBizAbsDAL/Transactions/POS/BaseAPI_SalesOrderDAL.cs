using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.API_SalesOrderRequest;
using EasyBizResponse.Transactions.POS.API_SalesOrderResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.POS
{
    public abstract class BaseAPI_SalesOrderDAL : BaseDAL
    {
        public abstract API_SelectALLSalesOrderResponse SelectAll(API_SelectAllSalesOrderRequest objRequest);
        public abstract API_SelectBySalesOrderIDResponse SelectRecord(API_SelectBySalesOrderIDRequest objRequest);
    }
}
