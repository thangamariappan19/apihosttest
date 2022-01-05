using EasyBizBLL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizIView.Transactions.IReports;
using EasyBizRequest.Transactions.Stocks.StockReceiptRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Reports
{
    public class StockReceiptPresenter
    {
        IStockReceipt _IStockRequest;
        StockReceiptBLL _StockReceiptBLL = new StockReceiptBLL();
        public StockReceiptPresenter(IStockReceipt ViewObj)
        {
            _IStockRequest = ViewObj;
        }
        public void GetReceiptHeaderList()
        {
            try
            {
                var RequestData = new SelectAllStockReceiptRequest();
                RequestData.RequestFrom = _IStockRequest.RequestFrom;
                RequestData.BusinessDate = _IStockRequest.BusinessDate;
                RequestData.StoreID = _IStockRequest.StoreID;
                var ResponseData = _StockReceiptBLL.SelectAllStockReceiptWms(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStockRequest.StockReceiptHeaderList = ResponseData.StockReceiptHeaderList;
                }
                else
                {
                    var InvoiceList = new List<StockReceiptHeader>();
                    _IStockRequest.StockReceiptHeaderList = InvoiceList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
    public class StockDetailViewPresenter
    {
        IStockReceiptDetailsView _IStockReceiptDetailsView;
        StockReceiptBLL _StockReceiptBLL = new StockReceiptBLL();
        public StockDetailViewPresenter(IStockReceiptDetailsView ViewObj)
        {
            _IStockReceiptDetailsView = ViewObj;
        }
        public void SelectStockReceiptdetaillist()
        {
            try
                {
                    var RequestData = new SelectByStockReceiptDetailsRequest();                    
                    RequestData.ID = _IStockReceiptDetailsView.ID;
                    var ResponseData = _StockReceiptBLL.SelectStockReceiptDetails(RequestData);
                    _IStockReceiptDetailsView.StockReceiptDetailsList = ResponseData.StockReceiptDetailsRecord;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }           
        }    
}
