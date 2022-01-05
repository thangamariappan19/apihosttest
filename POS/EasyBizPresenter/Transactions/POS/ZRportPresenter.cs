using EasyBizBLL.Common;
using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizIView.Reports;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS
{
    public class ZRportPresenter
    {
        IZReport _IZReport;
        DayShiftLOGBLL _DayShiftLOGBLL = new DayShiftLOGBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        public ZRportPresenter(IZReport ViewObj)
        {
            _IZReport = ViewObj;
        }

        public void SelectZReport()
        {
            try
            {
                var RequestData = new SelectZReportByDetailsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.BusinessDate = _IZReport.BusinessDate;
                var ResponseData = new SelectZReportByDetailsResponse();
                RequestData.StoreID = _IZReport.UserInformation.StoreID;
                ResponseData = _DayShiftLOGBLL.GetZReceipt(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IZReport.ZReportList = ResponseData.ZReportList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectZReport1()
        {
            try
            {
                var RequestData = new SelectZReportByDetailsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.BusinessDate = _IZReport.BusinessDate;
                var ResponseData = new SelectZReportByDetailsResponse();
                RequestData.StoreID = _IZReport.UserInformation.StoreID;
                ResponseData = _DayShiftLOGBLL.GetZReceipt1(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IZReport.ZReportList1 = ResponseData.ZReportList1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectZReport2()
        {
            try
            {
                var RequestData = new SelectZReportByDetailsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.BusinessDate = _IZReport.BusinessDate;
                var ResponseData = new SelectZReportByDetailsResponse();
                RequestData.StoreID = _IZReport.UserInformation.StoreID;
                ResponseData = _DayShiftLOGBLL.GetZReceipt2(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IZReport.ZReportList2 = ResponseData.Zreport2;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStoreMasterLookUP()
        {
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.StoreID = _IZReport.StoreID;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStorename(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IZReport.StoreMasterRecord = ResponseData.StoreMasterData;
            }
        }
    }
}
