using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizResponse.Masters.CurrencyResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class CurrencyBLL
    {
        public SaveCurrencyResponse SaveCurrencyMaster(SaveCurrencyRequest objRequest)
        {
            SaveCurrencyResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseCurrencyDAL objBaseCurrencyMasterDAL = objFactory.GetDALRepository().GetCurrencyDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCurrencyMaster = new CurrencyMaster();
                    objCurrencyMaster = (CurrencyMaster)objRequest.RequestDynamicData;
                    objRequest.CurrencyMasterData = objCurrencyMaster;
                }
                objResponse = (SaveCurrencyResponse)objBaseCurrencyMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.CurrencyMasterData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.CURRENCY;
                    objRequest.ProcessMode = Enums.ProcessMode.New;


                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CurrencyBLL", "SaveCurrencyMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveCurrencyResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Currency Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectAllCurrencyResponse API_SelectALL(SelectAllCurrencyRequest requestData)
        {
            SelectAllCurrencyResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseCurrencyDAL objBaseCurrencyMasterDAL = objFactory.GetDALRepository().GetCurrencyDAL();
                objResponse = (SelectAllCurrencyResponse)objBaseCurrencyMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCurrencyResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Currency Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateCurrencyResponse UpdateCurrencyMaster(UpdateCurrencyRequest objRequest)
        {
            UpdateCurrencyResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseCurrencyDAL objBaseCurrencyMasterDAL = objFactory.GetDALRepository().GetCurrencyDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCurrencyMaster = new CurrencyMaster();
                    objCurrencyMaster = (CurrencyMaster)objRequest.RequestDynamicData;
                    objRequest.CurrencyMasterData = objCurrencyMaster;
                }
                objResponse = (UpdateCurrencyResponse)objBaseCurrencyMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.CurrencyMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.CURRENCY;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CurrencyBLL", "UpdateCurrencyMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateCurrencyResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Currency Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteCurrencyResponse DeleteCurrencyMaster(DeleteCurrencyRequest objRequest)
        {
            DeleteCurrencyResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseCurrencyDAL objBaseCurrencyMasterDAL = objFactory.GetDALRepository().GetCurrencyDAL();
                objResponse = (DeleteCurrencyResponse)objBaseCurrencyMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.CurrencyMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.CURRENCY;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CurrencyBLL", "DeleteCurrencyMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteCurrencyResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Currency Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDCurrencyResponse SelectCurrencyMaster(SelectByIDCurrencyRequest objRequest)
        {
            SelectByIDCurrencyResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  
                BaseCurrencyDAL objBaseCurrencyMasterDAL = objFactory.GetDALRepository().GetCurrencyDAL();
                objResponse = (SelectByIDCurrencyResponse)objBaseCurrencyMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDCurrencyResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Currency Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllCurrencyResponse SelectAllCurrencyMaster(SelectAllCurrencyRequest objRequest)
        {
            SelectAllCurrencyResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseCurrencyDAL objBaseCurrencyMasterDAL = objFactory.GetDALRepository().GetCurrencyDAL();
                objResponse = (SelectAllCurrencyResponse)objBaseCurrencyMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllCurrencyResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Currency Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }
		public SelectCurreucyDetailsResponse SelectCurrencyDetails(SelectCurrencyDetailsRequest objRequest)
		{
			SelectCurreucyDetailsResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				BaseCurrencyDAL objBaseCurrencyMasterDAL = objFactory.GetDALRepository().GetCurrencyDAL();
				objResponse = (SelectCurreucyDetailsResponse)objBaseCurrencyMasterDAL.SelectCurrencyDetails(objRequest);
			}
			catch (Exception ex)
			{

				objResponse = new SelectCurreucyDetailsResponse();
				objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Currency Master");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;

		}
        public SelectCurrencyLookUpResponse SelectCurrencyLookUp(SelectCurrencyLookUpRequest objRequest)
        {
            SelectCurrencyLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseCurrencyDAL objBaseCurrencyMasterDAL = objFactory.GetDALRepository().GetCurrencyDAL();
                objResponse = (SelectCurrencyLookUpResponse)objBaseCurrencyMasterDAL.SelectCurrencyLookUp(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectCurrencyLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Currency Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectCurrencyLookUpResponse API_SelectCurrencyLookUp(SelectCurrencyLookUpRequest objRequest)
        {
            SelectCurrencyLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseCurrencyDAL objBaseCurrencyMasterDAL = objFactory.GetDALRepository().GetCurrencyDAL();
                objResponse = (SelectCurrencyLookUpResponse)objBaseCurrencyMasterDAL.API_SelectCurrencyLookUp(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectCurrencyLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Currency Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}



