using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Promotions;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ILabelPrintingMaster;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.Promotions.WNPromotionRequest;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Transactions.Promotions.WNPromotionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class SalesLabelPrintingPresenter
    {
        SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
        WNPromotionBLL _WNPromotionBLL = new WNPromotionBLL();
        ISalesLabelPrintingView _ISalesLabelPrintingView;

        public SalesLabelPrintingPresenter(ISalesLabelPrintingView ViewObj)
        {
            _ISalesLabelPrintingView = ViewObj;
        }

        public void GetStoreListByID()
        {
            try
            {
                var _StoreMasterBLL = new StoreMasterBLL();
                var RequestData = new SelectByIDStoreMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectByIDStoreMasterResponse();
                RequestData.ID = _ISalesLabelPrintingView.SelectedStoreId;
                ResponseData = _StoreMasterBLL.SelectedStoreId(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesLabelPrintingView.StoreMasterRecord = ResponseData.StoreMasterData;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetCurrencyByStore()
        {
            try
            {
                var _CountryBLL = new CountryBLL();
                var RequestData = new GetCurrencyByStoreRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new GetCurrencyByStoreResponse();
                RequestData.ID = _ISalesLabelPrintingView.SelectedStoreId;
                ResponseData = _CountryBLL.GetCurencyByStore(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesLabelPrintingView.CurrencyRecord = ResponseData.CurrencyByStore;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetWNPrice()
        {
            try
            {
                var RequestData = new SelectWNPromotionDetailsRequest();
                var ResponseData = new SelectWNPromotionDetailsResponse();
                RequestData.Department = _ISalesLabelPrintingView.Department;
                RequestData.ProductCode = _ISalesLabelPrintingView.ProductCode;
                ResponseData = _WNPromotionBLL.GetWNPrice(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _ISalesLabelPrintingView.WNPriceRecord = ResponseData.WNPriceData;
                    }
                    else
                    {
                        _ISalesLabelPrintingView.WNPriceRecord = null;
                    }                            
            }
            catch (Exception ex)
            {
                throw ex;
            }
            }

        public void GetStoreList()
        {
            try
            {
                var _StoreMasterBLL = new StoreMasterBLL();
                var RequestData = new SelectAllStoreMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllStoreMasterResponse();
                ResponseData = _StoreMasterBLL.SelectAllStoreMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesLabelPrintingView.StoreList = ResponseData.StoreMasterList;
                }
                else
                {
                    _ISalesLabelPrintingView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    }
