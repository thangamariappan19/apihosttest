using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizIView.Transactions.IPOS;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS
{
    public class FindItemPresenter
    {
        IFindItemView _IFindItemView;
        public FindItemPresenter(IFindItemView ViewObj)
        {
            _IFindItemView = ViewObj;
        }
        public void GetStock()
        {
            try
            {
                var _TransactionLogBLL = new TransactionLogBLL();
                var RequestData = new FindStockRequest();
                var ResponseData = new FindStockResponse();

                RequestData.ConnectionString = _IFindItemView.MainServerConnection;
                RequestData.SearchString = _IFindItemView.SearchString;
                RequestData.CountryID = _IFindItemView.CountryID;
                ResponseData = _TransactionLogBLL.GetStoreStockByCountry(RequestData);

                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IFindItemView.StockList = ResponseData.StockList;
                }
                else
                {
                    _IFindItemView.StockList = new List<TransactionLog>();
                    _IFindItemView.Message = ResponseData.DisplayMessage;
                }
            }
            catch(Exception ex)
            {
                _IFindItemView.StockList = new List<TransactionLog>();
                throw ex;
            }
        }
        public void GetStylePricingBySKUCode()
        {

            try
            {
                var _SKUMasterBLL = new SKUMasterBLL();
                var RequestData = new GetStylePricingBySKUCodeRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;

                RequestData.SKUCode = _IFindItemView.SkuCode;

                var ResponseData = _SKUMasterBLL.SelectGetStylePricingBySKUCode(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IFindItemView.StylePricingList = ResponseData.StylePricingList;
                }
                else
                {
                    _IFindItemView.StylePricingList = new List<StylePricing>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSKU()
        {
            try
            {
                var _SKUMasterBLL = new SKUMasterBLL();
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.Count = 1;
                RequestData.SearchString = _IFindItemView.SearchString;
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {                    
                    _IFindItemView.SKURecord = ResponseData.SKUMasterTypesList.FirstOrDefault();
                }
                else
                {
                    _IFindItemView.SKURecord = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
