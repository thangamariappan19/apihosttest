using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.PaymentModeMaterRequest;
using EasyBizResponse.Masters.PaymentModeMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class PaymentModeMasterBLL
    {
        public SavePaymentModeMasterResponse SavePaymentModeMaster(SavePaymentModeMasterRequest objRequest)
        {

            SavePaymentModeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePaymentModeMaster = objFactory.GetDALRepository().GetPaymentModeMaster();
                if (objRequest.RequestDynamicData != null)
                {
                    var objPaymentModeMaster = new PaymentModeTypes();
                    objPaymentModeMaster = (PaymentModeTypes)objRequest.RequestDynamicData;
                    objRequest.PaymentModeTypesData = objPaymentModeMaster;
                }
                objResponse = (SavePaymentModeMasterResponse)objBasePaymentModeMaster.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentNos = objRequest.PaymentModeTypesData.PaymentModeCode;
                    objRequest.PaymentModeTypesData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.CUSTOMERMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                }
            }
            catch (Exception ex)
            {
                objResponse = new SavePaymentModeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdatePaymentModeMasterResponse UpdatePaymentMode(UpdatePaymentModeMasterRequest objRequest)
        {

            UpdatePaymentModeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePaymentModeMasterDAL = objFactory.GetDALRepository().GetPaymentModeMaster();
                if (objRequest.RequestDynamicData != null)
                {
                    var objBasePaymentModeMaster = new PaymentModeTypes();
                    objBasePaymentModeMaster = (PaymentModeTypes)objRequest.RequestDynamicData;
                    objRequest.PaymentModeMasterData = objBasePaymentModeMaster;
                }
                objResponse = (UpdatePaymentModeMasterResponse)objBasePaymentModeMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.PaymentModeMasterData.ID);
                    objRequest.DocumentNos = Convert.ToString(objRequest.PaymentModeMasterData.PaymentModeCode);
                  
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CustomerMasterBLL", "UpdateCustomerMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdatePaymentModeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllPaymentModeMasterResponse SelectAllPaymentModeMaster(SelectAllPaymentModeMasterRequest objRequest)
        {
            SelectAllPaymentModeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBasePaymentModeMaster = objFactory.GetDALRepository().GetPaymentModeMaster();
                objResponse = (SelectAllPaymentModeMasterResponse)objBasePaymentModeMaster.SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPaymentModeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Customer Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;

        }

        public SelectByIDPaymentModeMasterResponse SelectPaymentModeRecord(SelectByIDPaymentModeMasterRequest objRequest)
        {
            SelectByIDPaymentModeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }
                var objBasePaymentModeDAL = objFactory.GetDALRepository().GetPaymentModeMaster();
                objResponse = (SelectByIDPaymentModeMasterResponse)objBasePaymentModeDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDPaymentModeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Brand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectPaymentModeLooKUpResponse SelectPaymentModeLookUpRecord(SelectPaymentModeLooKUpRequest objRequest)
        {
            SelectPaymentModeLooKUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
               
                var objBasePaymentModeDAL = objFactory.GetDALRepository().GetPaymentModeMaster();
                objResponse = (SelectPaymentModeLooKUpResponse)objBasePaymentModeDAL.SelectPaymentModeRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectPaymentModeLooKUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Brand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
    }
}
