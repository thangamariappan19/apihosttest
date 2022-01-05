using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.CardDetails;
using EasyBizResponse.Transactions.POS.CardDetails;
using EasyBizResponse.Transactions.POS.Invoice;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
  public  class InvoiceCardDetailsBLL
    {
      public SaveCardDetailsResponse SaveInvoiceCardDetails(SaveCardDetailsRequest objRequest)
      {
          SaveCardDetailsResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              var objBaseInvoiceDAL = objFactory.GetDALRepository().GetCardDetailsDAL();
              if (objRequest.RequestDynamicData != null)
              {
                  var ObjInvoiceCardDetails = new InvoiceCardDetails();
                  ObjInvoiceCardDetails = (InvoiceCardDetails)objRequest.RequestDynamicData;
                  objRequest.InvoiceCardDetailsrData = ObjInvoiceCardDetails;

              }
              objResponse = (SaveCardDetailsResponse)objBaseInvoiceDAL.InsertRecord(objRequest);
              
              if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
              {
                  objRequest.RequestFrom = Enums.RequestFrom.StoreServer;
                  objRequest.DocumentIDs = Convert.ToString(objRequest.DocumentIDs);
                  objRequest.DocumentType = Enums.DocumentType.PAYMENTS;
                  objRequest.ProcessMode = Enums.ProcessMode.New;

                  BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.InvoiceCardDetailsBLL", "SaveInvoiceCardDetails");
              }
          }
          catch (Exception ex)
          {
              objResponse = new SaveCardDetailsResponse();
              objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SaveInvoice");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }

      public SelectCreditCardDetailsByInvoiceNoResponse SelectCreditCardDetailsByInvoiceNo(SelectCreditCardDetailsByInvoiceNoRequest objRequest)
      {
          SelectCreditCardDetailsByInvoiceNoResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              var objBaseInvoiceDAL = objFactory.GetDALRepository().GetCardDetailsDAL();
              objResponse = (SelectCreditCardDetailsByInvoiceNoResponse)objBaseInvoiceDAL.SelectCreditCardDetailsByInvoiceNo(objRequest);
          }
          catch (Exception ex)
          {
              objResponse = new SelectCreditCardDetailsByInvoiceNoResponse();
              objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SaveInvoice");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }

        public SaveCardDetailsResponse SavePaymentProcessor(SaveCardDetailsRequest objRequest)
        {
            SaveCardDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseInvoiceDAL = objFactory.GetDALRepository().GetCardDetailsDAL();
                objResponse = (SaveCardDetailsResponse)objBaseInvoiceDAL.InsertRecord(objRequest);

                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = Enums.RequestFrom.StoreServer;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.DocumentIDs);
                    objRequest.DocumentType = Enums.DocumentType.PAYMENTS;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.InvoiceCardDetailsBLL", "SavePaymentProcessor");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveCardDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SaveInvoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
