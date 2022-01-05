using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizIView.Transactions.IItemSummary;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Masters.StyleMasterResponse;
using EasyBizResponse.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Reports
{
    public class StyleSummaryPresenter
    {
        IStyleSummaryView _IStyleSummaryView;
        public StyleSummaryPresenter(IStyleSummaryView ViewObj)
        {
            _IStyleSummaryView = ViewObj;
        }
        public void GetCuntryMasterLookUp()
        {
            try
            {
                var _CountryBLL = new CountryBLL();
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStyleSummaryView.CountryList = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStyleDetails()
        {
            try
            {
                var _StyleMasterBLL = new StyleMasterBLL();
                var RequestData = new SelectByStyleIDRequest();
                var ResponseData = new SelectByStyleIDResponse();
                RequestData.StyleCode = _IStyleSummaryView.StyleCode;
                ResponseData = _StyleMasterBLL.SelectStyleRecord(RequestData);

                _IStyleSummaryView.ProcessStatus = ResponseData.StatusCode;

                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {                    
                    _IStyleSummaryView.StyleMasterData = ResponseData.StyleRecord;
                    GetStyleSummaryData();
                    GetSkuImageList();
                }
                else
                {
                    _IStyleSummaryView.StyleMasterData = null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void GetStyleSummaryData()
        {
            try
            {
                var _TransactionLogBLL = new TransactionLogBLL();
                var RequestData = new FindStockRequest();
                var ResponseData = new FindStockResponse();
                RequestData.SearchString = _IStyleSummaryView.StyleCode;
                RequestData.CountryID = _IStyleSummaryView.CountryID;
                ResponseData = _TransactionLogBLL.GetStyleSummary(RequestData);
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStyleSummaryView.StyleSummaryDataSet = ResponseData.StyleSummaryDataSet;
                }
                else
                {
                    _IStyleSummaryView.StyleSummaryDataSet = new System.Data.DataSet();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void GetSkuImageList()
        {
            var _SKUMasterBLL = new SKUMasterBLL();
            var RequestData = new SelectByALLSKUImagesRequest();
            var ResponseData = new SelectAllSKUImagesResponse();
            try
            {
                RequestData.SKUID = 0;
                RequestData.StyleID = _IStyleSummaryView.StyleID;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                ResponseData = _SKUMasterBLL.SelectAllSKUImages(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStyleSummaryView.StyleImage = ResponseData.SKUImageList.FirstOrDefault().SKUImage;
                }
                else
                {
                    _IStyleSummaryView.StyleImage = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
