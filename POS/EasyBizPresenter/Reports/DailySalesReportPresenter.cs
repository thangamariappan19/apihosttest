using EasyBizBLL.Masters;
using EasyBizBLL.Reports;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports;
using EasyBizIView.Reports;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.PosMasterRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Reports.DailySalesReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Reports
{
    public class DailySalesReportPresenter
    {
        IDailySalesReportView _IDailySalesReportView;
        CountryBLL _CountryBLL = new CountryBLL();
        StoreGroupBLL _StoreGroupBLL = new StoreGroupBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        PosMasterBLL _PosMasterBLL = new PosMasterBLL();
        StyleMasterBLL _StyleMasterBLL = new StyleMasterBLL();
        DailySalesReportBLL _DailySalesReportBLL = new DailySalesReportBLL();

        public DailySalesReportPresenter(IDailySalesReportView ViewObj)
        {
            _IDailySalesReportView = ViewObj;
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
                    _IDailySalesReportView.CountryLookUp = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GetStoreGroupMasterLookUp()
        {
            try
            {
                var RequestData = new SelectStoreGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.FormType = "Reports";
                RequestData.CountryID = _IDailySalesReportView.CountryID;
                var ResponseData = _StoreGroupBLL.SelectStoreGroupMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDailySalesReportView.StoreGroupLookUp = ResponseData.StoreGroupMasterList;
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
                RequestData.Type = "Reports";
                RequestData.StoreGroupID = _IDailySalesReportView.StoreGroupID;
                var ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDailySalesReportView.StoreMasterLookUp = ResponseData.StoreMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetPOSMasterLookUp()
        {
            try
            {
                var RequestData = new SelectPosMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.Type = "Reports";
                RequestData.StoreID = _IDailySalesReportView.StoreID;
                var ResponseData = _PosMasterBLL.SelectPosMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDailySalesReportView.POSMasterLookUp = ResponseData.PosMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetStyleMasterLookUp()
        {
            try
            {
                var RequestData = new SelectStyleLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _StyleMasterBLL.SelectStyleLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDailySalesReportView.SelectStyleLookUp = ResponseData.StyleMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetDailySalesReportDetails()
        {
            try
            {
                var RequestData = new DailySalesReportRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.FromDate = _IDailySalesReportView.FromDate;
                RequestData.ToDate = _IDailySalesReportView.ToDate;
                RequestData.CountryID = _IDailySalesReportView.CountryID;
                RequestData.StoreGroupID = _IDailySalesReportView.StoreGroupID;
                RequestData.StoreID = _IDailySalesReportView.StoreID;
                RequestData.PosID = _IDailySalesReportView.PosID;
                RequestData.StyleID = _IDailySalesReportView.StyleID;
                RequestData.StyleCode = _IDailySalesReportView.StyleCode;
                var ResponseData = _DailySalesReportBLL.DailySalesReport(RequestData);
                _IDailySalesReportView.ProcessStatus = ResponseData.StatusCode;
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDailySalesReportView.DailySalesReport = ResponseData.DailySalesReportList;
                    //_IDailySalesReportView.SalesDataTable = ResponseData.SalesDataTable;
                }
                else
                {
                    _IDailySalesReportView.DailySalesReport = new List<DailySalesReport>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
