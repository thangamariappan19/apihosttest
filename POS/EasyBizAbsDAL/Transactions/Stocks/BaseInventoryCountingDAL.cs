using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.Stocks.InventoryCounting;
using EasyBizResponse.Transactions.Stocks.InventoryCounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.Stocks
{
    public abstract class BaseInventoryCountingDAL : BaseDAL
    {
        public abstract SelectByInventoryCountingDetailsResponse SelectByInventoryCountingDetails(SelectByInventoryCountingDetailsRequest ObjRequest);
        public abstract GetSystemStockByStoreResponse GetSystemStockByStore(GetSystemStockByStoreRequest ObjRequest);
        public abstract SaveSystemStockResponse SaveSystemStock(SaveSystemStockRequest ObjRequest);
        public abstract GetInventoryCountingInitResponse GetInventoryCountingInitList(GetInventoryCountingInitRequest ObjRequest);
        public abstract GetInventoryCountingInitRecordResponse GetInventoryCountingInitRecord(GetInventoryCountingInitRecordRequest ObjRequest);
        public abstract SaveManualStockResponse SaveManualStock(SaveManualStockRequest ObjRequest);
        public abstract GetInventoryManualCountRecordResponse GetInventoryManualCountRecord(GetInventoryManualCountRecordRequest ObjRequest);
        public abstract InventoryFinalizeResponse InventoryFinalize(InventoryFinalizeRequest ObjRequest);
        public abstract InventorySyncResponse InventorySyncToServer(InventorySyncRequest ObjRequest);
        public abstract GetInventoryCountingInitResponse API_GetInventoryCountingInitList(GetInventoryCountingInitRequest ObjRequest);
        public abstract GetSystemStockByStoreResponse API_SystemStockByStoreCount(GetSystemStockByStoreRequest ObjRequest);
        public abstract GetSystemStockByStoreResponse API_SystemStockByStorelimit(GetSystemStockByStoreRequest ObjRequest);
    }
}
