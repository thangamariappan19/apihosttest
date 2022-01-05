using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.LanguageRequest;
using EasyBizResponse.Masters.LanguageResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class LanguageBLL
    {
        public SaveLanguageResponse SaveLanguage(SaveLanguageRequest objRequest)
        {
            SaveLanguageResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseLanguageDAL objBaseLanguageDAL = objFactory.GetDALRepository().GetBaseLanguageDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var ObjLanguage = new LanguageMaster();
                    ObjLanguage = (LanguageMaster)objRequest.RequestDynamicData;
                    objRequest.LanguageMasterRecord = ObjLanguage;
                }
                objResponse = (SaveLanguageResponse)objBaseLanguageDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.LanguageMasterRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.LANGUAGESETTINGS;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.LanguageBLL", "SaveLanguage");
                }
            
            }
            catch (Exception ex)
            {
                objResponse = new SaveLanguageResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Language Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllLanguageResponse API_SelectLanguageLookUp(SelectAllLanguageRequest objRequest)
        {
            SelectAllLanguageResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseLanguageDAL objBaseLanguageDAL = objFactory.GetDALRepository().GetBaseLanguageDAL();                
                objResponse = (SelectAllLanguageResponse)objBaseLanguageDAL.API_SelectLanguageLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllLanguageResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Language Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllLanguageResponse API_SelectAllLanguage(SelectAllLanguageRequest objRequest)
        {
            SelectAllLanguageResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseLanguageDAL objBaseLanguageDAL = objFactory.GetDALRepository().GetBaseLanguageDAL();
                objResponse = (SelectAllLanguageResponse)objBaseLanguageDAL.API_SelectAll(objRequest);               
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllLanguageResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Language Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateLanguageResponse UpdateLanguage(UpdateLanguageRequest objRequest)
        {
            UpdateLanguageResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseLanguageDAL objBaseLanguageDAL = objFactory.GetDALRepository().GetBaseLanguageDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var ObjLanguage = new LanguageMaster();
                    ObjLanguage = (LanguageMaster)objRequest.RequestDynamicData;
                    objRequest.LanguageMasterRecord = ObjLanguage;
                }
                objResponse = (UpdateLanguageResponse)objBaseLanguageDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.LanguageMasterRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.LANGUAGESETTINGS;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.LanguageBLL", "UpdateLanguage");
                }
            
            }
            catch (Exception ex)
            {
                objResponse = new UpdateLanguageResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Language Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByLanguageIDResponse SelectLanguage(SelectByLanguageIDRequest objRequest)
        {
            SelectByLanguageIDResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  
                BaseLanguageDAL objBaseLanguageDAL = objFactory.GetDALRepository().GetBaseLanguageDAL();
                objResponse = (SelectByLanguageIDResponse)objBaseLanguageDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByLanguageIDResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Language Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllLanguageResponse SelectAllLanguage(SelectAllLanguageRequest objRequest)
        {
            SelectAllLanguageResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseLanguageDAL objBaseLanguageDAL = objFactory.GetDALRepository().GetBaseLanguageDAL();
                //objResponse = (SelectAllLanguageResponse)objBaseLanguageDAL.API_SelectAll(objRequest);
                objResponse = (SelectAllLanguageResponse)objBaseLanguageDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllLanguageResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Language Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteLanguageResponse DeleteLanguage(DeleteLanguageRequest objRequest)
        {
            DeleteLanguageResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseLanguageDAL objBaseLanguageDAL = objFactory.GetDALRepository().GetBaseLanguageDAL();
                objResponse = (DeleteLanguageResponse)objBaseLanguageDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.LanguageMasterRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.LANGUAGESETTINGS;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.LanguageBLL", "DeleteLanguage");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteLanguageResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Language Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectLookUpResponse SelectLookUpLanguage(SelectLookUpRequest objRequest)
        {
            SelectLookUpResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                BaseLanguageDAL objBaseLanguageDAL = objFactory.GetDALRepository().GetBaseLanguageDAL();
                objResponse = (SelectLookUpResponse)objBaseLanguageDAL.SelectLanguageLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Language Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
