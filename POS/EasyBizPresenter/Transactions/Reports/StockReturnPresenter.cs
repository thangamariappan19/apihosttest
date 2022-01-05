using EasyBizBLL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockReturn;
using EasyBizIView.Transactions.IReports;
using EasyBizRequest.Transactions.Stocks.StockReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Reports
{
   public class StockReturnPresenter
    {
       IStockReturn _ISalesReturn;
       StockReturnBLL _StockReturnBLL = new StockReturnBLL();

        public StockReturnPresenter(IStockReturn ViewObj)
        {
            _ISalesReturn = ViewObj;
        }

        public void GetReturnHeaderList()
        {
            try
            {
                var RequestData = new SelectAllStockReturnRequest();
                RequestData.RequestFrom = _ISalesReturn.RequestFrom;
                RequestData.BusinessDate = _ISalesReturn.BusinessDate;
                RequestData.StoreID = _ISalesReturn.StoreID;
                RequestData.StoreCode = _ISalesReturn.StoreCode;
                var ResponseData = _StockReturnBLL.SelectAllStockReturn(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturn.StockReturnHeaderList = ResponseData.StockReturnHeaderList;
                }
                else
                {
                    var InvoiceList = new List<StockReturnHeader>();
                    _ISalesReturn.StockReturnHeaderList = InvoiceList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }

   public class StockReturnDetailsPresenter
   {
       IStockReturnDetailsView _IStockReturnDetailsView;
       StockReturnBLL _StockReturnBLL = new StockReturnBLL();
       public StockReturnDetailsPresenter(IStockReturnDetailsView ViewObj)
       {
           _IStockReturnDetailsView = ViewObj;
       }
       public void SelectReturndetaillist()
       {
           try
           {
               var RequestData = new SelectByStockReturnDetailsRequest();              
               RequestData.ID = _IStockReturnDetailsView.ID;
               var ResponseData = _StockReturnBLL.SelectStockReturnDetails(RequestData);
               _IStockReturnDetailsView.StockReturnDetailsList = ResponseData.StockReturnDetailsRecord;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
   }
}
