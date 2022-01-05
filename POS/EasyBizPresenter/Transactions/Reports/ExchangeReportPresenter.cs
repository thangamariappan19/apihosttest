using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS.SalesExchange;
using EasyBizIView.Transactions.IReports;
using EasyBizRequest.Transactions.POS.SalesExchangeRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Reports
{
    public class ExchangeReportPresenter
    {
        IExchangeReport _IExchangeReport;
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        SalesExchangeBLL _SalesExchangeBLL = new SalesExchangeBLL();
        public ExchangeReportPresenter(IExchangeReport ViewObj)
        {
            _IExchangeReport = ViewObj;
        }
        public void GetExchangeHeaderList()
        {
            try
            {
                var RequestData = new SelectAllSalesExchangeRequest();
                RequestData.RequestFrom = _IExchangeReport.RequestFrom;
                RequestData.BusinessDate = _IExchangeReport.BusinessDate;
                RequestData.StoreID = _IExchangeReport.StoreID;
                var ResponseData = _SalesExchangeBLL.SelectAllSalesExchangeList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IExchangeReport.SalesExchangeHeaderList = ResponseData.SalesExchangeList;
                }
                else
                {
                    var SalesExchangeList = new List<SalesExchangeHeader>();
                    _IExchangeReport.SalesExchangeHeaderList = SalesExchangeList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
    public class ExchangeDetailReportPresenter
    {
        SalesExchangeBLL _SalesExchangeBLL = new SalesExchangeBLL();
        IExchangeDetailsView _IExchangeDetailsView;
        public ExchangeDetailReportPresenter(IExchangeDetailsView ViewObj)
        {
            _IExchangeDetailsView = ViewObj;
        }
        public void SelectExchangedetaillist()
        {
            try
            {
                var RequestData = new SelectAllSalesExchangeDetailRequest();
                RequestData.SalesExchangeID = _IExchangeDetailsView.ID;
                var ResponseData = _SalesExchangeBLL.SelectAllSalesExchangeDetailList(RequestData);
                _IExchangeDetailsView.SalesExchangeDetailList = ResponseData.SalesExchangeDetailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
