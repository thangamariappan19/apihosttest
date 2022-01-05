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
    public class DetailSalesReportPresenter
    {
        IDetailSalesReportView _IDetailSalesReportView;
        public DetailSalesReportPresenter(IDetailSalesReportView ViewObj)
        {
            _IDetailSalesReportView = ViewObj;
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
                    _IDetailSalesReportView.StoreList = ResponseData.StoreMasterList;
                }
                else
                {
                    _IDetailSalesReportView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSalesInvoiceReportData()
        {
            var _ReportsBLL = new ReportsBLL();            
            var RequestData = new CommonReportRequest();
            var ResponseData = new CommonReportRespose();

            RequestData.InvoiceNo = _IDetailSalesReportView.InvoiceNo;
            RequestData.StoreID = _IDetailSalesReportView.SelectedStoreId;
            RequestData.MODE = 1;

            ResponseData = _ReportsBLL.GetSalesInvoiceReportData(RequestData);
            if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDetailSalesReportView.DetailSalesDataTable = ResponseData.ReportDataTable;
            }
            else
            {
                _IDetailSalesReportView.Message = ResponseData.DisplayMessage;
                _IDetailSalesReportView.DetailSalesDataTable = new System.Data.DataTable();
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
                RequestData.ID = _IDetailSalesReportView.SelectedStoreId;
                ResponseData = _StoreMasterBLL.SelectedStoreId(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDetailSalesReportView.StoreMasterRecord = ResponseData.StoreMasterData;

                }
                else
                {
                    _IDetailSalesReportView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
}
