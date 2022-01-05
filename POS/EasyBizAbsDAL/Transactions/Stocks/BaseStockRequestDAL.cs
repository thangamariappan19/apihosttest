using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.Stocks
{
    public abstract class BaseStockRequestDAL : BaseDAL
    {
        public abstract SelectByStockRequestDetailsResponse SelectByStockRequestDetails(SelectByStockRequestDetailsRequest ObjRequest);
        public abstract SelectByStockRequestDetailsResponse SelectByStockRequestHeaderID(SelectByStockRequestDetailsRequest ObjRequest);
        public abstract SaveStockRequestResponse Saveint_stock(SaveStockRequestRequest ObjRequest);
        public abstract SelectAllStockRequestResponse SelectAllInt_ConfirmTransfer(SelectAllStockRequestRequest ObjRequest);

        public abstract SelectByStockRequestDetailsResponse Selectint_stockreceiptDetails(SelectByStockRequestDetailsRequest ObjRequest);
        public abstract SelectByStockRequestDetailsResponse SelectWithOutint_stockreceipt(SelectByStockRequestDetailsRequest ObjRequest);
        public abstract SelectAllStockRequestResponse API_SelectALL(SelectAllStockRequestRequest objRequest);
    }
}
