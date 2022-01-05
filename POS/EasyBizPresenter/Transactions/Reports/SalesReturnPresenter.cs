using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS.SalesReturn;
using EasyBizIView.Transactions.IReports;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.Sales_Return;
using EasyBizRequest.Transactions.POS.SalesReturnRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Reports
{
   public class SalesReturnPresenter
    {
       ISalesReturn _ISalesReturn;
       SalesReturnBLL _SalesReturnBLL = new SalesReturnBLL();      

        public SalesReturnPresenter(ISalesReturn ViewObj)
        {
            _ISalesReturn = ViewObj;
        }

        public void GetReturnHeaderList()
        {
            try
            {
                var RequestData = new SelectAllSalesReturnRequest();
                RequestData.RequestFrom = _ISalesReturn.RequestFrom;
                RequestData.BusinessDate = _ISalesReturn.BusinessDate;
                RequestData.StoreID = _ISalesReturn.StoreID;
                var ResponseData = _SalesReturnBLL.SelectAllSalesReturn(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISalesReturn.SalesReturnList = ResponseData.SalesReturnHeaderList;
                }
                else
                {
                    var InvoiceList = new List<SalesReturnHeader>();
                    _ISalesReturn.SalesReturnList = InvoiceList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }

   public class SalesReturnDetailViewPresenter
   {
       ISalesReturnDetailsView _ISalesReturnDetailsView;
       SalesReturnBLL _SalesReturnBLL = new SalesReturnBLL();      
       public SalesReturnDetailViewPresenter(ISalesReturnDetailsView ViewObj)
       {
           _ISalesReturnDetailsView = ViewObj;
       }
       public void Selectinvoicedetaillist()
       {
           try
           {
               var RequestData = new SelectSalesReturnDetailsByIDRequest();               
               RequestData.ID = _ISalesReturnDetailsView.ID;
               var ResponseData = _SalesReturnBLL.SelectInvoiceDetailsByID(RequestData);
               _ISalesReturnDetailsView.SalesReturnDetailList = ResponseData.SalesReturnDetailData;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
   }
}
