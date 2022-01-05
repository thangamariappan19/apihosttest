using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Pricing;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizIView.Transactions.IPricePoint;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Transactions.Pricing.PricePointRequest;
using EasyBizResponse.Masters.Brand_Response;
using EasyBizResponse.Transactions.Pricing.PricePointResponse;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Pricing
{

    public class PricePointPresenter
    {
        PricePointBLL _PricePointBLL = new PricePointBLL();
        BrandBLL _BrandBLL = new BrandBLL();
        CurrencyBLL _CurrencyBLL = new CurrencyBLL();
        IPricePointView _IPricePointView;
        public PricePointPresenter(IPricePointView ViewObj)
        {
            _IPricePointView = ViewObj;
        }
        public void GetCurrencyList()
        {
            var RequestData = new SelectAllCurrencyRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = _CurrencyBLL.SelectAllCurrencyMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IPricePointView.CurrencyList = ResponseData.CurrencyMasterList;
            }
        }
        public void GetBrandList()
        {
            var RequestData = new SelectBrandLookUpRequest();
            var ResponseData = new SelectBrandLookUpResponse();
            ResponseData = _BrandBLL.BrandLookUp(RequestData);

            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IPricePointView.BrandList = ResponseData.BrandList;
            }
        }
        public void SavePricePoint()
        {
            try
            {
                var RequestData = new SavePricePointRequest();
                var ResponseData = new SavePricePointResponse();

                RequestData.PricePointList = _IPricePointView.PricePointList;
                RequestData.PricePointCode = _IPricePointView.PricePointCode;
                RequestData.PricePointName = _IPricePointView.PricePointName;
                RequestData.Mode = _IPricePointView.Mode;

                ResponseData = _PricePointBLL.SavePricePointList(RequestData);

                _IPricePointView.Message = ResponseData.DisplayMessage;
                _IPricePointView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeletePricePoint()
        {
            try
            {
                //var RequestData = new DeletePricePointRequest();
                //RequestData.ID = _IPricePointView.Mode;
                //var ResponseData = _PricePointBLL.DeletePricePoint(RequestData);
                //_IPricePointView.Message = ResponseData.DisplayMessage;
                //_IPricePointView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetPricePointRecord()
        {
            try
            {
                var RequestData = new SelectPricePointByIDRequest();
                var ResponseData = new SelectPricePointByIDResponse();

                
                RequestData.PricePointCode = _IPricePointView.PricePointCode;
                

                ResponseData = _PricePointBLL.GetPricePointRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPricePointView.PricePointList = ResponseData.PricePointList;
                }
                else
                {
                    _IPricePointView.Message = ResponseData.DisplayMessage;
                    _IPricePointView.ProcessStatus = ResponseData.StatusCode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class PricePointListViewPresenter
    {
        PricePointBLL _PricePointBLL = new PricePointBLL();
        IPricePointListView _IPricePointListView;
        public PricePointListViewPresenter(IPricePointListView ViewObj)
        {
            _IPricePointListView = ViewObj;
        }
        public void GetPricePointList()
        {
            try
            {
                SelectAllPricePointRequest RequestData = new SelectAllPricePointRequest();
                SelectAllPricePointResponse ResponseData = new SelectAllPricePointResponse();
                RequestData.ProcessMode = Enums.ProcessMode.ViewMode;
                ResponseData = _PricePointBLL.GetPricePointList(RequestData);
                if (ResponseData.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    _IPricePointListView.PricePointList = ResponseData.PricePointList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
