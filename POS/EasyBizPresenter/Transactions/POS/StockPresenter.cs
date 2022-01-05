using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizIView.Transactions.IPOS;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS
{
    public class StockPresenter
    {
        IStyleStockView _IStyleStockView;
        public StockPresenter(IStyleStockView ViewObj)
        {
            _IStyleStockView = ViewObj;
        }
        public void GetStyleListOverView()
        {
            try
            {
                var RequestData = new GetStockByStyleCodeRequest();
                var ResponseData = new GetStockByStyleCodeResponse();
                var _TransactionLogBLL = new TransactionLogBLL();
                
                RequestData.SearchValue = _IStyleStockView.SearchValue;
                RequestData.RequestFrom = Enums.RequestFrom.Search;
               
                ResponseData = _TransactionLogBLL.StyleStockOverView(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStyleStockView.StockList = ResponseData.StockList;
                }
                else
                {
                    ResponseData.StockList = new List<TransactionLog>();
                    _IStyleStockView.StockList = ResponseData.StockList;
                    _IStyleStockView.Message = "No Records found !.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
