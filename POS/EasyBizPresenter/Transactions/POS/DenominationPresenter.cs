using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView.Transactions.IPOS;
using EasyBizIView.Transactions.IPOS.IInvoice;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.PaymentTypeSettingRequest;
using EasyBizRequest.Transactions.POS.DenominationRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS
{
   public class DenominationPresenter
    {
       IDenomination _IDenomination;
       PaymentTypeMasterBLL _PaymentTypeMasterBLL = new PaymentTypeMasterBLL();
       CurrencyBLL _CurrencyBLL=new CurrencyBLL();
       DenominationBLL _DenominationBLL = new DenominationBLL();
       public DenominationPresenter(IDenomination ViewObj)
       {
           _IDenomination = ViewObj;
       }
       public void GetPaymentTypeList()
       {
           var RequestData = new SelectPaymentTypeByCountryRequest();
           RequestData.ShowInActiveRecords = false;
           RequestData.CountryID = _IDenomination.CountyID;
           var ResponseData = _PaymentTypeMasterBLL.SelectPaymentTypeByCountry(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _IDenomination.PaymentTypeList = ResponseData.PaymentDetailsList;
               //_IDenomination.InvoiceCardRecord.InvoiceHeaderID = ResponseData.InvoiceNoCreditCardDetails.InvoiceHeaderID;
           }
       }
       public void GetCashList()
       {
           var RequestData = new SelectCurrencyDetailsRequest();
           RequestData.ShowInActiveRecords = false;
           RequestData.CurrencyCode = _IDenomination.PayCurrencyCode;
           var ResponseData = _CurrencyBLL.SelectCurrencyDetails(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _IDenomination.CurrencyDetailsList = ResponseData.CurrencyDetailsList;
               //_IDenomination.InvoiceCardRecord.InvoiceHeaderID = ResponseData.InvoiceNoCreditCardDetails.InvoiceHeaderID;
           }
       }
       public void SaveDenomination()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new SaveDenominationRequest();
                   RequestData.DenominationForShiftoutTypeHeader = new DenominationForShiftoutTypeHeader();
                   RequestData.DenominationForShiftOutTypeList = _IDenomination.SaveDenominationForShiftOutTypeList;
                   RequestData.PaymentTypeMasterTypeList = _IDenomination.PaymentTypeList;
                   RequestData.DenominationForShiftoutTypeHeader.ID = _IDenomination.ID;
                   RequestData.DenominationForShiftoutTypeHeader.ShifLogId = _IDenomination.ShiftLogID;
                   RequestData.DenominationForShiftoutTypeHeader.StoreCode = _IDenomination.StoreCode;
                   RequestData.DenominationForShiftoutTypeHeader.ShiftCode = _IDenomination.ShiftCode;
                   RequestData.DenominationForShiftoutTypeHeader.POSCode = _IDenomination.POSCode;
                   RequestData.DenominationForShiftoutTypeHeader.ShiftInAmount = _IDenomination.ShiftInAmount;
                   RequestData.DenominationForShiftoutTypeHeader.ShiftOutAmount = _IDenomination.ShiftOutAmount;
                   RequestData.DenominationForShiftoutTypeHeader.GrandTotalValue = _IDenomination.GrandTotalValue;
                   RequestData.DenominationForShiftoutTypeHeader.TotalValueCount = _IDenomination.TotalValueCount;
                   RequestData.DenominationForShiftoutTypeHeader.TotalCardValue = _IDenomination.TotalCardValue;
                   RequestData.DenominationForShiftoutTypeHeader.remarks = _IDenomination.Remarks;


                   //RequestData.DenominationForShiftoutTypeHeader.remarks = _IDenomination.Remarks;
                   //RequestData.StockReceiptHeaderRecord.Status = _IStockReceiptView.Status;
                   //RequestData.StockReceiptHeaderRecord.StoreID = _IStockReceiptView.StoreID;                  

                   //RequestData.StockReceiptHeaderRecord.CreateBy = _IStockReceiptView.UserID;
                   //RequestData.StockReceiptHeaderRecord.CreateOn = DateTime.Now;
                   //RequestData.StockReceiptHeaderRecord.Active = true;
                   //RequestData.StockReceiptHeaderRecord.Type = _IStockReceiptView.Type;
                   //RequestData.StockReceiptHeaderRecord.SCN = _IStockReceiptView.SCN;


                   var ResponseData = _DenominationBLL.SaveDenomination(RequestData);
                   _IDenomination.Message = ResponseData.DisplayMessage;
                   _IDenomination.ProcessStatus = ResponseData.StatusCode;
                   //_IStockReceiptView.HeaderID = Convert.ToInt32(ResponseData.);
                   //_IStockReceiptView.HeaderID = Convert.ToInt32(ResponseData.IDs);             

               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public bool IsValidForm()
       {                   
           bool objBool = false; 
               if (_IDenomination.DenominationForShiftOutTypeList.Count == 0)
               {
                   _IDenomination.Message = "DenominationList is missing Please Select it.";
               }           
               else
               {
                   objBool = true;
               }
               return objBool;                  
       }
    }
}
