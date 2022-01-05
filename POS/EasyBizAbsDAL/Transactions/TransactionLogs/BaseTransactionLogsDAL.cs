using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.POS.NonTradingStockRequest;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Transactions.POS.NonTradingStockResponse;
using EasyBizResponse.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.TransactionLogs
{
    public abstract class BaseTransactionLogsDAL : BaseDAL
    {
        public abstract GetStockByStyleCodeResponse GetStockByStyleCode(GetStockByStyleCodeRequest RequestObj);
        public abstract GetStockByStyleCodeResponse GetStockByStyleOverView(GetStockByStyleCodeRequest RequestObj);
        public abstract GetStockBySkuResponse GetStockBySku(GetStockBySkuRequest RequestObj);
        public abstract GetStockBySkuResponse GetStockBySku1(GetStockBySkuRequest RequestObj);
        public abstract FindStockResponse GetStoreStockByCountry(FindStockRequest RequestObj);
        public abstract FindStockResponse GetStyleSummary(FindStockRequest RequestObj);
        public abstract GetQuantityBySKUResponse GetQuantityBySku(GetQuantityBySKURequest RequestObj);
        public abstract GetStockByStyleCodeResponse GetStockPivotByStyle(GetStockByStyleCodeRequest RequestObj);
        public abstract GetNonTradingStockBySKUResponse GetNonTradingStockBySku(GetNonTradingStockBySKURequest objRequest);

        public abstract FindStockByCountryResponse GetFindStockByCountry(FindStockRequest objRequest);
        public abstract GetProductDescSearchResponse GetProductDescSearch(GetProductDescSearchRequest objRequest);
        public abstract GetProductDescSearchResponse GetPOSProductDescSearch(GetProductDescSearchRequest objRequest);
        public abstract GetStockByStyleCodeResponse GetStoreStockByStyleOverView(GetStockByStyleCodeRequest objRequest);

        public abstract GetProductCommonSearchResponse GetProductCommonSearch(GetProductCommonSearchRequest objRequest);
    }
}
