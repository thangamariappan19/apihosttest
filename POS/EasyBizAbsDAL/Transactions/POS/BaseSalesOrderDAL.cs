using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.SalesOrder;
using EasyBizResponse.Transactions.POS.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizRequest.Transactions.POS.API_SalesOrderRequest;
using EasyBizResponse.Transactions.POS.API_SalesOrderResponse;

namespace EasyBizAbsDAL.Transactions.POS
{
    public abstract class BaseSalesOrderDAL : BaseDAL
    {
        public abstract SelectSalesOrderDetailResponse SelectSalesOrderDetails(SelectSalesOrderDetailRequest RequestObj);
    }
}
