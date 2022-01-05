using EasyBizBLL.Masters;
using EasyBizBLL.Reports;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports;
using EasyBizIView.Reports;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Reports.CurrentStockReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Reports
{
 public class CurrentStoctReportPresenter
    {
        ICurrentStockReportView _ICurrentStockReportView;
        CountryBLL _CountryBLL = new CountryBLL();
        BrandBLL _BrandBLL = new BrandBLL();
        StyleMasterBLL _StyleMasterBLL = new StyleMasterBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        CurrentStockReportBLL _CurrentStockReportBLL = new CurrentStockReportBLL();
     public CurrentStoctReportPresenter(ICurrentStockReportView ViewObj)
        {
            _ICurrentStockReportView = ViewObj;
        }
        public void GetCountryLookUp()
        {
            try
            {
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICurrentStockReportView.CountryLookUp = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetStoreMasterLookUp()
        {
            try
            {
                var RequestData = new SelectStoreMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CountryID = _ICurrentStockReportView.CountryID;
                var ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICurrentStockReportView.StoreMasterLookUp = ResponseData.StoreMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetBrandMasterLookUp()
        {
            try
            {
                var RequestData = new SelectBrandLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.StoreID = _ICurrentStockReportView.StoreMasterID;
                RequestData.Type = "Report";
                var ResponseData = _BrandBLL.BrandLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICurrentStockReportView.BrandMasterLookUp = ResponseData.BrandList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetFromStyleMasterLookUp()
        {
            try
            {
                var RequestData = new SelectStyleLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _StyleMasterBLL.SelectStyleLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICurrentStockReportView.SelectFromStyleLookUp = ResponseData.StyleMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetToStyleMasterLookUp()
        {
            //try
            //{
            //    var RequestData = new SelectStyleLookUpRequest();
            //    RequestData.ShowInActiveRecords = false;
            //    var ResponseData = _StyleMasterBLL.SelectStyleLookUp(RequestData);
            //    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            //    {
            //        _ICurrentStockReportView.SelectToStyleLookUp = ResponseData.StyleMasterList;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


        public void GetCurrentStockReport()
        {
            try
            {
                var RequestData = new CurrentStockReportRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.StoreID = _ICurrentStockReportView.StoreMasterID;
                RequestData.CountryID = _ICurrentStockReportView.CountryID;
                RequestData.StyleCode = _ICurrentStockReportView.StyleCode;
                var ResponseData = _CurrentStockReportBLL.CurrentStockReport(RequestData);
                _ICurrentStockReportView.ProcessStatus = ResponseData.StatusCode;
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICurrentStockReportView.CurrentStockReport = ResponseData.CurrentStockReportList;
                   
                }
                else
                {
                    _ICurrentStockReportView.CurrentStockReport = new List<CurrentStockReport>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 }


}
