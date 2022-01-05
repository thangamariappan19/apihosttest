using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.PaymentTypeSettingRequest;
using EasyBizResponse.Masters.PaymentTypeSettingResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class PaymentTypeMasterBLL
    {


        public SavePaymentTypeResponse SavePaymentType(SavePaymentTypeRequest objRequest)
        {

            SavePaymentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePaymentTypeSettingDAL = objFactory.GetDALRepository().GetPaymentTypeSettingMaster();
                if (objRequest.RequestDynamicData != null)
                {
                    var objPaymentTypeMaster = new PaymentTypeMasterType();
                    objPaymentTypeMaster = (PaymentTypeMasterType)objRequest.RequestDynamicData;
                    objRequest.PaymentTypeMasterData = objPaymentTypeMaster;
                }
                objResponse = (SavePaymentTypeResponse)objBasePaymentTypeSettingDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.PaymentTypeMasterData.ID = Convert.ToInt32(objResponse.IDs);
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.PAYMENTTYPE;
                //    objRequest.ProcessMode = Enums.ProcessMode.New;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PaymentTypeMasterBLL", "SavePaymentType");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SavePaymentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Payment Type Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllPaymentTypeResponse API_SelectALL(SelectAllPaymentTypeRequest requestData)
        {
            SelectAllPaymentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBasePaymentTypeSettingDAL = objFactory.GetDALRepository().GetPaymentTypeSettingMaster();
                objResponse = (SelectAllPaymentTypeResponse)objBasePaymentTypeSettingDAL.API_SelectALL(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPaymentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Payment Type Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public UpdatePaymentTypeResponse UpdatePaymentType(UpdatePaymentTypeRequest objRequest)
        {

            UpdatePaymentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePaymentTypeSettingDAL = objFactory.GetDALRepository().GetPaymentTypeSettingMaster();
                if (objRequest.RequestDynamicData != null)
                {
                    var objPaymentTypeMaster = new PaymentTypeMasterType();
                    objPaymentTypeMaster = (PaymentTypeMasterType)objRequest.RequestDynamicData;
                    objRequest.PaymentTypeMasterData = objPaymentTypeMaster;
                }
                objResponse = (UpdatePaymentTypeResponse)objBasePaymentTypeSettingDAL.UpdateRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objRequest.PaymentTypeMasterData.ID);
                //    objRequest.DocumentType = Enums.DocumentType.PAYMENTTYPE;
                //    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PaymentTypeMasterBLL", "UpdatePaymentType");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new UpdatePaymentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Payment Type Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }



        public DeletePaymentTypeResponse DeletePaymentType(DeletePaymentTypeRequest objRequest)
        {
            DeletePaymentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePaymentTypeSettingDAL = objFactory.GetDALRepository().GetPaymentTypeSettingMaster();
                objResponse = (DeletePaymentTypeResponse)objBasePaymentTypeSettingDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.PaymentTypeMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.PAYMENTTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PaymentTypeMasterBLL", "DeletePaymentType");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeletePaymentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Payment Type Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllPaymentTypeResponse SelectAllPaymentType(SelectAllPaymentTypeRequest objRequest)
        {
            SelectAllPaymentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBasePaymentTypeSettingDAL = objFactory.GetDALRepository().GetPaymentTypeSettingMaster();
                objResponse = (SelectAllPaymentTypeResponse)objBasePaymentTypeSettingDAL.SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPaymentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Payment Type Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;

        }


        public SelectByIDPaymentTypeResponse SelectByIDPaymentType(SelectByIDPaymentTypeRequest objRequest)
        {
            SelectByIDPaymentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBasePaymentTypeSettingDAL = objFactory.GetDALRepository().GetPaymentTypeSettingMaster();
                objResponse = (SelectByIDPaymentTypeResponse)objBasePaymentTypeSettingDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDPaymentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Payment Type Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }





        public SelectPaymentTypeByCountryResponse SelectPaymentTypeByCountry(SelectPaymentTypeByCountryRequest objRequest)
        {
            SelectPaymentTypeByCountryResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.CountryID == 0)
                {
                    objRequest.CountryID = Convert.ToInt32(objRequest.DocumentIDs);
                }
                var objBasePaymentTypeSettingDAL = objFactory.GetDALRepository().GetPaymentTypeSettingMaster();
                objResponse = (SelectPaymentTypeByCountryResponse)objBasePaymentTypeSettingDAL.SelectPaymentTypeByCountry(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectPaymentTypeByCountryResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Payment Type Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SelectPaymentTypeLookUpResponse SelectPaymentTypeLookUp(SelectPaymentLookUpRequest RequestData)
        {
            SelectPaymentTypeLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BasePaymentTypeSettingDAL objBasePaymentTypeSettingDAL = objFactory.GetDALRepository().GetPaymentTypeSettingMaster();
                objResponse = (SelectPaymentTypeLookUpResponse)objBasePaymentTypeSettingDAL.SelectPaymentTypeLookUp(RequestData);
            }
            catch (Exception ex)
            {

                objResponse = new SelectPaymentTypeLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Payment Type Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
