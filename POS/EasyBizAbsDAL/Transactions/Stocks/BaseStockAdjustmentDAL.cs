using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.Stocks.StockAdjustment;
using EasyBizResponse.Transactions.Stocks.StockAdjustment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.Stocks
{
    public abstract class BaseStockAdjustmentDAL :BaseDAL
    {
        public abstract SelectStockAdjustmentDetailsResponse SelectByStockAdjustmentDetails(SelectStockAdjustmentDetailsRequest ObjRequest);
        public abstract GetAllStockAdjustmentRecordResponse API_SelectALL(GetAllStockAdjustmentRecordRequest objRequest);
    }
}
