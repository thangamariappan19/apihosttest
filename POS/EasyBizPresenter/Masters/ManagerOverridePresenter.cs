using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IManagerOverride;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizResponse.Masters.ManagerOverrideResponse;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class ManagerOverridePresenter
    {
       IManagerOverrideView _IManagerOverrideView;
       ManagerOverrideBLL _ManagerOverrideBLL = new ManagerOverrideBLL();

       public ManagerOverridePresenter(IManagerOverrideView ViewObj)
        {
            _IManagerOverrideView = ViewObj;
        }

       public void SelectManagerOverride()
       {
           var RequestData = new SelectByIDManagerOverrideRequest();
           RequestData.ID = _IManagerOverrideView.ID;
           var ResponseData = _ManagerOverrideBLL.SelectManagerOverride(RequestData);
           _IManagerOverrideView.Code = ResponseData.ManagerOverrideRecord.Code;
           _IManagerOverrideView.Name = ResponseData.ManagerOverrideRecord.Name;
           _IManagerOverrideView.CreditLimitOverride = ResponseData.ManagerOverrideRecord.CreditLimitOverride;
           _IManagerOverrideView.ReprintTransReceipt = ResponseData.ManagerOverrideRecord.ReprintTransReceipt;
           _IManagerOverrideView.changeSalesPersoninSOE = ResponseData.ManagerOverrideRecord.changeSalesPersoninSOE;
           _IManagerOverrideView.ChangeSalesPersonRefund = ResponseData.ManagerOverrideRecord.ChangeSalesPersonRefund;
           _IManagerOverrideView.DelSuspendedTransaction = ResponseData.ManagerOverrideRecord.DelSuspendedTransaction;
           _IManagerOverrideView.VoidSale = ResponseData.ManagerOverrideRecord.VoidSale;
           _IManagerOverrideView.voidItem = ResponseData.ManagerOverrideRecord.voidItem;
           _IManagerOverrideView.TransModeChange = ResponseData.ManagerOverrideRecord.TransModeChange;
           _IManagerOverrideView.CustomerSearch = ResponseData.ManagerOverrideRecord.CustomerSearch;
           _IManagerOverrideView.ProductSearch = ResponseData.ManagerOverrideRecord.ProductSearch;
           _IManagerOverrideView.SaleInfoEdit = ResponseData.ManagerOverrideRecord.SaleInfoEdit;
           _IManagerOverrideView.ItemInfoEdit = ResponseData.ManagerOverrideRecord.ItemInfoEdit;
           _IManagerOverrideView.TransactionSearch = ResponseData.ManagerOverrideRecord.TransactionSearch;
           _IManagerOverrideView.SuspendRecall = ResponseData.ManagerOverrideRecord.SuspendRecall;
           _IManagerOverrideView.CashOut = ResponseData.ManagerOverrideRecord.CashOut;
           _IManagerOverrideView.CashIn = ResponseData.ManagerOverrideRecord.CashIn;
           _IManagerOverrideView.DayInDayOut = ResponseData.ManagerOverrideRecord.DayInDayOut;
           _IManagerOverrideView.TransactionRefund = ResponseData.ManagerOverrideRecord.TransactionRefund;
           _IManagerOverrideView.Active = ResponseData.ManagerOverrideRecord.Active;
           _IManagerOverrideView.AllowEditcustomer = ResponseData.ManagerOverrideRecord.AllowEditcustomer;
           _IManagerOverrideView.TotalDiscount = ResponseData.ManagerOverrideRecord.TotalDiscount;                      
          
           if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
           {
               _IManagerOverrideView.Message = ResponseData.DisplayMessage;
           }

           else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
           {
               _IManagerOverrideView.Message = ResponseData.DisplayMessage;
           }

           _IManagerOverrideView.ProcessStatus = ResponseData.StatusCode;
       }

       public void SaveManagerOverride()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new SaveManagerOverrideRequest();
                   RequestData.ManagerOverrideData = new ManagerOverride();
                   RequestData.ManagerOverrideData.ID = _IManagerOverrideView.ID;
                   RequestData.ManagerOverrideData.Code = _IManagerOverrideView.Code;
                   RequestData.ManagerOverrideData.Name = _IManagerOverrideView.Name;
                   RequestData.ManagerOverrideData.CreditLimitOverride = _IManagerOverrideView.CreditLimitOverride;
                   RequestData.ManagerOverrideData.ReprintTransReceipt = _IManagerOverrideView.ReprintTransReceipt;
                   RequestData.ManagerOverrideData.changeSalesPersoninSOE = _IManagerOverrideView.changeSalesPersoninSOE;
                   RequestData.ManagerOverrideData.ChangeSalesPersonRefund = _IManagerOverrideView.ChangeSalesPersonRefund;
                   RequestData.ManagerOverrideData.DelSuspendedTransaction = _IManagerOverrideView.DelSuspendedTransaction;
                   RequestData.ManagerOverrideData.VoidSale = _IManagerOverrideView.VoidSale;
                   RequestData.ManagerOverrideData.voidItem = _IManagerOverrideView.voidItem;
                   RequestData.ManagerOverrideData.TransModeChange = _IManagerOverrideView.TransModeChange;
                   RequestData.ManagerOverrideData.CustomerSearch = _IManagerOverrideView.CustomerSearch;
                   RequestData.ManagerOverrideData.ProductSearch = _IManagerOverrideView.ProductSearch;
                   RequestData.ManagerOverrideData.SaleInfoEdit = _IManagerOverrideView.SaleInfoEdit;
                   RequestData.ManagerOverrideData.ItemInfoEdit = _IManagerOverrideView.ItemInfoEdit;
                   RequestData.ManagerOverrideData.TransactionSearch = _IManagerOverrideView.TransactionSearch;
                   RequestData.ManagerOverrideData.SuspendRecall = _IManagerOverrideView.SuspendRecall;
                   RequestData.ManagerOverrideData.CashOut = _IManagerOverrideView.CashOut;
                   RequestData.ManagerOverrideData.CashIn = _IManagerOverrideView.CashIn;
                   RequestData.ManagerOverrideData.TransactionRefund = _IManagerOverrideView.TransactionRefund;
                   RequestData.ManagerOverrideData.TotalDiscount = _IManagerOverrideView.TotalDiscount;
                   RequestData.ManagerOverrideData.DayInDayOut = _IManagerOverrideView.DayInDayOut;
                   RequestData.ManagerOverrideData.AllowEditcustomer = _IManagerOverrideView.AllowEditcustomer;
                   
                   RequestData.ManagerOverrideData.Active = _IManagerOverrideView.Active;

                   RequestData.ManagerOverrideData.CreateBy = _IManagerOverrideView.UserID;
                   RequestData.ManagerOverrideData.CreateOn = DateTime.Now;
                   RequestData.ManagerOverrideData.SCN = _IManagerOverrideView.SCN;

                   var ResponseData = _ManagerOverrideBLL.SaveManagerOverride(RequestData);

                   _IManagerOverrideView.Message = ResponseData.DisplayMessage;
                   _IManagerOverrideView.ProcessStatus = ResponseData.StatusCode;
               }
               else
               {
                   _IManagerOverrideView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       private bool IsValidForm()
       {
           bool objBool = false;
           if (_IManagerOverrideView.Code.Trim() == string.Empty)
           {
               _IManagerOverrideView.Message = "Code is missing Please Enter it.";
           }
           else if (_IManagerOverrideView.Name.Trim() ==string.Empty)
           {
               _IManagerOverrideView.Message = " Name is Missing.";
           }
           //else if (_IManagerOverrideView.CreditLimitOverride.Trim() == string.Empty)
           //{
           //    _IManagerOverrideView.Message = " CreditLimitOverride is Missing.";
           //}
           
            else
           {
               objBool = true;
           }
           return objBool;
       }

       public void UpdateManagerOverride()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new UpdateManagerOverrideRequest();
                   RequestData.ManagerOverrideData = new ManagerOverride();

                   RequestData.ManagerOverrideData.ID = _IManagerOverrideView.ID;
                   RequestData.ManagerOverrideData.Code = _IManagerOverrideView.Code;
                   RequestData.ManagerOverrideData.Name = _IManagerOverrideView.Name;
                   RequestData.ManagerOverrideData.CreditLimitOverride = _IManagerOverrideView.CreditLimitOverride;
                   RequestData.ManagerOverrideData.ReprintTransReceipt = _IManagerOverrideView.ReprintTransReceipt;
                   RequestData.ManagerOverrideData.changeSalesPersoninSOE = _IManagerOverrideView.changeSalesPersoninSOE;
                   RequestData.ManagerOverrideData.ChangeSalesPersonRefund = _IManagerOverrideView.ChangeSalesPersonRefund;
                   RequestData.ManagerOverrideData.DelSuspendedTransaction = _IManagerOverrideView.DelSuspendedTransaction;
                   RequestData.ManagerOverrideData.VoidSale = _IManagerOverrideView.VoidSale;
                   RequestData.ManagerOverrideData.voidItem = _IManagerOverrideView.voidItem;
                   RequestData.ManagerOverrideData.TransModeChange = _IManagerOverrideView.TransModeChange;
                   RequestData.ManagerOverrideData.CustomerSearch = _IManagerOverrideView.CustomerSearch;
                   RequestData.ManagerOverrideData.ProductSearch = _IManagerOverrideView.ProductSearch;
                   RequestData.ManagerOverrideData.SaleInfoEdit = _IManagerOverrideView.SaleInfoEdit;
                   RequestData.ManagerOverrideData.ItemInfoEdit = _IManagerOverrideView.ItemInfoEdit;
                   RequestData.ManagerOverrideData.TransactionSearch = _IManagerOverrideView.TransactionSearch;
                   RequestData.ManagerOverrideData.SuspendRecall = _IManagerOverrideView.SuspendRecall;
                   RequestData.ManagerOverrideData.CashOut = _IManagerOverrideView.CashOut;
                   RequestData.ManagerOverrideData.CashIn = _IManagerOverrideView.CashIn;
                   RequestData.ManagerOverrideData.TransactionRefund = _IManagerOverrideView.TransactionRefund;
                   RequestData.ManagerOverrideData.TotalDiscount = _IManagerOverrideView.TotalDiscount;
                   RequestData.ManagerOverrideData.DayInDayOut = _IManagerOverrideView.DayInDayOut;
                   RequestData.ManagerOverrideData.Active = _IManagerOverrideView.Active;
                   RequestData.ManagerOverrideData.AllowEditcustomer = _IManagerOverrideView.AllowEditcustomer;
                   RequestData.ManagerOverrideData.UpdateBy = _IManagerOverrideView.UserID;
                   RequestData.ManagerOverrideData.UpdateOn = DateTime.Now;
                   RequestData.ManagerOverrideData.SCN = _IManagerOverrideView.SCN;
                   var ResponseData = _ManagerOverrideBLL.UpdateManagerOverride(RequestData);

                   _IManagerOverrideView.Message = ResponseData.DisplayMessage;
                   _IManagerOverrideView.ProcessStatus = ResponseData.StatusCode;
               }
               else
               {
                   _IManagerOverrideView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void DeleteManagerOverride()
       {
          try
       
        {
            var RequestData = new DeleteManagerOverrideRequest();
            RequestData.ID = -_IManagerOverrideView.ID;
            var ResponseData = _ManagerOverrideBLL.DeleteManagerOverride(RequestData);
            _IManagerOverrideView.Message = ResponseData.DisplayMessage;
            _IManagerOverrideView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
       }
    }
    public class ManagerOverrideListPresenter
    {
        IManagerOverrideList _IManagerOverrideList;
        ManagerOverrideBLL _ManagerOverrideBLL = new ManagerOverrideBLL();
        public ManagerOverrideListPresenter(IManagerOverrideList ViewObj)
    {
        _IManagerOverrideList = ViewObj;
    }

        public void GetManagerOverrideList()
        {
            var RequestData = new SelectAllManagerOverrideRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = new SelectAllManagerOverrideResponse();
            ResponseData = _ManagerOverrideBLL.SelectAllManagerOverride(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IManagerOverrideList.ManagerOverrideList = ResponseData.ManagerOverrideList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {

            }
        }
    }
}
