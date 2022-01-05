using EasyBizDBTypes.Common;
using EasyBizFactory;
using EasyBizRequest.Common;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Common;
using EasyBizResponse.Transactions.POS.Invoice;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Common
{
   public class DayShiftLOGBLL
    {
       public SaveShiftLOGResponse SaveDayClosing(SaveShiftLOGRequest objRequest)
       {
           SaveShiftLOGResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
               if (objRequest.RequestDynamicData != null)
               {
                   objRequest.ShiftRecord = (ShiftLOGTypes)objRequest.RequestDynamicData;
               }

               var objBaseShiftLOGDAL = objFactory.GetDALRepository().GetBaseShiftLOGDAL();
               objResponse = (SaveShiftLOGResponse)objBaseShiftLOGDAL.InsertRecord(objRequest);

               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.ShiftRecord.ID = Convert.ToInt32(objResponse.IDs);
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.SHIFT;
                   objRequest.ProcessMode = Enums.ProcessMode.New;

                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Common.DayShiftLOGBLL", "SaveDayClosing");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveShiftLOGResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DayClosing Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SaveShiftLOGResponse UpdateDayClosing(SaveShiftLOGRequest objRequest)
       {
           SaveShiftLOGResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
               if (objRequest.RequestDynamicData != null)
               {
                   objRequest.ShiftRecord = (ShiftLOGTypes)objRequest.RequestDynamicData;
               }

               var objBaseShiftLOGDAL = objFactory.GetDALRepository().GetBaseShiftLOGDAL();
               objResponse = (SaveShiftLOGResponse)objBaseShiftLOGDAL.UpdateRecord(objRequest);
               

               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.ShiftRecord.ID = Convert.ToInt32(objResponse.IDs);
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.DAYCLOSING;
                   objRequest.ProcessMode = Enums.ProcessMode.Edit;
                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Common.DayShiftLOGBLL", "UpdateDayClosing");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveShiftLOGResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DayClosing Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectXReportByDetailsResponse GetXReceipt(SelectXReportByDetailsRequest objRequest)
       {
           SelectXReportByDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseInvoice = objFactory.GetDALRepository().GetShiftMasterDAL();
               objResponse = (SelectXReportByDetailsResponse)BaseInvoice.GetXReceipt(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectXReportByDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectXReportByDetailsResponse GetXReceipt1(SelectXReportByDetailsRequest objRequest)
       {
           SelectXReportByDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseInvoice = objFactory.GetDALRepository().GetShiftMasterDAL();
               objResponse = (SelectXReportByDetailsResponse)BaseInvoice.GetXReceipt1(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectXReportByDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectZReportByDetailsResponse GetZReceipt(SelectZReportByDetailsRequest objRequest)
       {
           SelectZReportByDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseInvoice = objFactory.GetDALRepository().GetShiftMasterDAL();
               objResponse = (SelectZReportByDetailsResponse)BaseInvoice.GetZReceipt(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectZReportByDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectZReportByDetailsResponse GetZReceipt1(SelectZReportByDetailsRequest objRequest)
       {
           SelectZReportByDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseInvoice = objFactory.GetDALRepository().GetShiftMasterDAL();
               objResponse = (SelectZReportByDetailsResponse)BaseInvoice.GetZReceipt1(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectZReportByDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectZReportByDetailsResponse GetZReceipt2(SelectZReportByDetailsRequest objRequest)
       {
           SelectZReportByDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseInvoice = objFactory.GetDALRepository().GetShiftMasterDAL();
               objResponse = (SelectZReportByDetailsResponse)BaseInvoice.GetZReceipt2(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectZReportByDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

        public SelectDayInResponse GetSelectDayIn(SelectDayInRequest objRequest)
        {
            SelectDayInResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectDayInResponse)BaseInvoice.GetDayIn(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectDayInResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectDayInResponse ShiftOut(SelectDayInRequest objRequest)
        {
            SelectDayInResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectDayInResponse)BaseInvoice.UpdateShift(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectDayInResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
