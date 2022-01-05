using EasyBizBLL.Masters;
using EasyBizBLL.Reports;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Reports;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Reports;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Reports
{
    public class SalesmanWiseReportPresenter
    {
        ISalesmanwiseReportView _ISalesmanwiseReportView;
        public SalesmanWiseReportPresenter(ISalesmanwiseReportView ViewObj)
        {
            _ISalesmanwiseReportView = ViewObj;
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
                    _ISalesmanwiseReportView.StoreList = ResponseData.StoreMasterList;
                }
                else
                {
                    _ISalesmanwiseReportView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetStoreListByID()
        {
            try
            {
                var _StoreMasterBLL = new StoreMasterBLL();
                var RequestData = new SelectByIDStoreMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectByIDStoreMasterResponse();
                RequestData.ID = _ISalesmanwiseReportView.SelectedStoreId; 
                ResponseData = _StoreMasterBLL.SelectedStoreId(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesmanwiseReportView.StoreMasterRecord = ResponseData.StoreMasterData;
               
                }
                else
                {
                    _ISalesmanwiseReportView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSalesmanWiseReportData()
        {
            var _ReportsBLL = new ReportsBLL();
            var RequestData = new CommonReportRequest();
            var ResponseData = new CommonReportRespose();

            RequestData.FromDate = _ISalesmanwiseReportView.BusinessDate;
            RequestData.StoreID = _ISalesmanwiseReportView.SelectedStoreId;            

            ResponseData = _ReportsBLL.GetSalesManWiseReport(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ISalesmanwiseReportView.SalesmanWiseReportTable = ResponseData.ReportDataTable;
            }
            else
            {
                _ISalesmanwiseReportView.Message = ResponseData.DisplayMessage;
                _ISalesmanwiseReportView.SalesmanWiseReportTable = new System.Data.DataTable();
            }
        }        
    }

}
