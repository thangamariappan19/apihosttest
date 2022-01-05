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
   public class StockReceiptReportPresenter
    {
        StockReceiptReportView _StockReceiptReportView;
        public StockReceiptReportPresenter(StockReceiptReportView ViewObj)
        {
            _StockReceiptReportView = ViewObj;
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
                    _StockReceiptReportView.StoreList = ResponseData.StoreMasterList;
                }
                else
                {
                    _StockReceiptReportView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetStockReceiptReportData()
        {
            var _ReportsBLL = new ReportsBLL();
            var RequestData = new CommonReportRequest();
            var ResponseData = new CommonReportRespose();

            RequestData.FromDate = _StockReceiptReportView.BusinessDate;
            RequestData.ToDate = _StockReceiptReportView.ToBusinessDate;
            RequestData.StoreID = _StockReceiptReportView.SelectedStoreId;

            ResponseData = _ReportsBLL.GetStockReceiptReport(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _StockReceiptReportView.StockReceiptReportTable = ResponseData.ReportDataTable;
            }
            else
            {
                _StockReceiptReportView.Message = ResponseData.DisplayMessage;
                _StockReceiptReportView.StockReceiptReportTable = new System.Data.DataTable();
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
                RequestData.ID = _StockReceiptReportView.SelectedStoreId;
                ResponseData = _StoreMasterBLL.SelectedStoreId(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _StockReceiptReportView.StoreMasterRecord = ResponseData.StoreMasterData;

                }
                else
                {
                    _StockReceiptReportView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
