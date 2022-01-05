using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Common;
using EasyBizRequest.Masters.ShiftRequest;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Masters.ShiftMasterResponse;
using EasyBizResponse.Masters.ShiftResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class ShiftBLL
    {
        public SelectByCountryIDResponse SelectCountryRecord(SelectByCountryIDRequest objRequest)
        {
            SelectByCountryIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
               
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectByCountryIDResponse)objBaseAgentMasterDAL.SelectCountryByID(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByCountryIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectShiftLogResponse SelectShiftLogRecordbyID(SelectShiftLogRequest objRequest)
        {
            SelectShiftLogResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseShiftMasterDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectShiftLogResponse)objBaseShiftMasterDAL.SelectShiftLogRecordbyID(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectShiftLogResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllShiftResponse API_SelectALL(SelectAllShiftRequest requestData)
        {
            SelectAllShiftResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectAllShiftResponse)objBaseShiftDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllShiftResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SelectShiftLogResponse SelectAllShiftLog(SelectShiftLogRequest objRequest)
        {
            SelectShiftLogResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseShiftMasterDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectShiftLogResponse)objBaseShiftMasterDAL.SelectAllShiftLog(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectShiftLogResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectShiftLogResponse SelectJoinShiftMasterandLog(SelectShiftLogRequest objRequest)
        {
            SelectShiftLogResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseShiftMasterDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectShiftLogResponse)objBaseShiftMasterDAL.SelectJoinShiftMasterandLog(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectShiftLogResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectShiftLogResponse SelectShiftInEnabled(SelectShiftLogRequest objRequest)
        {
            SelectShiftLogResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseShiftMasterDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectShiftLogResponse)objBaseShiftMasterDAL.SelectShiftInEnabled(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectShiftLogResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectShiftLogResponse SelectMaxShiftInEnabled(SelectShiftLogRequest objRequest)
        {
            SelectShiftLogResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseShiftMasterDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectShiftLogResponse)objBaseShiftMasterDAL.SelectMaxShiftInEnabled(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectShiftLogResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SaveShiftResponse SaveShift(SaveShiftRequest objRequest)
        {
            SaveShiftResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                //objRequest.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    //var objShift = new ShiftMaster();
                    //objShift = (ShiftMaster)objRequest.RequestDynamicData;
                    //objRequest.Shiftlist = objShift.Shiftlist;

                    var objShift = new List<ShiftMaster>();
                    objShift.AddRange(objRequest.RequestDynamicData);
                    objRequest.Shiftlist = objShift;
                }
                objResponse = (SaveShiftResponse)objBaseShiftDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);                    
                    objRequest.DocumentType = Enums.DocumentType.SHIFT;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ShiftBLL", "SaveShift");
                }
            

            }
            catch (Exception ex)
            {
                objResponse = new SaveShiftResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeleteShiftResponse DeleteShift(DeleteShiftRequest objRequest)
        {
            DeleteShiftResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (DeleteShiftResponse)objBaseShiftDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.SHIFT;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AFSegamationMasterBLL", "DeleteShift");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteShiftResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllShiftResponse SelectAllShiftRecords(SelectAllShiftRequest objRequest)
        {
            SelectAllShiftResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectAllShiftResponse)objBaseShiftDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllShiftResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public UpdateShiftResponse UpdateShift(UpdateShiftRequest objRequest)
        {
            UpdateShiftResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objShift = new ShiftMaster();
                    objShift = (ShiftMaster)objRequest.RequestDynamicData;
                  
                }
                objResponse = (UpdateShiftResponse)objBaseShiftDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.SHIFT;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AFSegamationMasterBLL", "UpdateShift");
                }

            }
            catch (Exception ex)
            {
                objResponse = new UpdateShiftResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectShiftLookUpResponse ShiftLookUp(SelectShiftLookUpRequest objRequest)
        {
            SelectShiftLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectShiftLookUpResponse)objBaseShiftDAL.SelectShiftLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectShiftLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectShiftListForCategoryResponse ShiftByCountry(SelectShiftListForCategoryRequest objRequest)
        {
            SelectShiftListForCategoryResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                if(objRequest.CountryID == 0)
                {
                    if(! string.IsNullOrEmpty(objRequest.DocumentIDs))
                    {
                        long temp;
                        long.TryParse(objRequest.DocumentIDs, out temp);
                        objRequest.CountryID = temp;
                    }
                }
                objResponse = (SelectShiftListForCategoryResponse)objBaseShiftDAL.SelectShiftListByCountry(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectShiftListForCategoryResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }
        public SelectZReportByDetailsResponse SelectZReportRecords(SelectZReportByDetailsRequest objRequest)
        {
            SelectZReportByDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectZReportByDetailsResponse)objBaseShiftDAL.GetZReceipt(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectZReportByDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        } 
        public SelectZReportByDetailsResponse SelectZReport1Records(SelectZReportByDetailsRequest objRequest)
        {
            SelectZReportByDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectZReportByDetailsResponse)objBaseShiftDAL.GetZReceipt1(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectZReportByDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectZReportByDetailsResponse SelectZReport2Records(SelectZReportByDetailsRequest objRequest)
        {
            SelectZReportByDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectZReportByDetailsResponse)objBaseShiftDAL.GetZReceipt2(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectZReportByDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectXReportByDetailsResponse SelectXReport1Records(SelectXReportByDetailsRequest objRequest)
        {
            SelectXReportByDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectXReportByDetailsResponse)objBaseShiftDAL.GetXReceipt(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectXReportByDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectXReportByDetailsResponse SelectXReport2Records(SelectXReportByDetailsRequest objRequest)
        {
            SelectXReportByDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectXReportByDetailsResponse)objBaseShiftDAL.GetXReceipt1(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectXReportByDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectNewZReportByDetailsResponse SelectNewZReportRecords(SelectNewZReportByDetailsRequest objRequest)
        {
            SelectNewZReportByDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectNewZReportByDetailsResponse)objBaseShiftDAL.GetZReceiptdetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectNewZReportByDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectXReportByDetailsResponse SelectNewXReportRecords(SelectXReportByDetailsRequest objRequest)
        {
            SelectXReportByDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectXReportByDetailsResponse)objBaseShiftDAL.GetXReceiptDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectXReportByDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectNewXReportByDetailsReponse SelectNewXReportRecord(SelectNewXReportByDetailsRequest objRequest)
        {
            SelectNewXReportByDetailsReponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseShiftDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectNewXReportByDetailsReponse)objBaseShiftDAL.GetNewXReceiptDetails(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectNewXReportByDetailsReponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Shift Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }

    }
}