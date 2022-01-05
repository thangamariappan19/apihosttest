using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.PosMasterRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.PosMasterResponse;
using EasyBizResponse.Masters.StoreGroupResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class PosMasterBLL
    {
        public SavePosMasterResponse SavePosMaster(SavePosMasterRequest objRequest)
        {
            SavePosMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;
                var objBasePosMasterDAL = objFactory.GetDALRepository().GetPosMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objPosMaster = new PosMaster();
                    objPosMaster = (PosMaster)objRequest.RequestDynamicData;
                    objRequest.PosMasterData = objPosMaster;
                }
                objResponse = (SavePosMasterResponse)objBasePosMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {

					// Changed by Senthamil @ 26.09.2018
					//objRequest.RequestFrom = objRequest.RequestFrom;
					objRequest.RequestFrom = Enums.RequestFrom.MainServer;
                    objRequest.PosMasterData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.POS;
                    objRequest.ProcessMode = Enums.ProcessMode.New;
                    objRequest.BaseIntegrateStoreID = objRequest.PosMasterData.StoreID;

                  //  BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PosMasterBLL", "SavePosMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SavePosMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Pos Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllPosMasterResponse API_SelectALL(SelectAllPosMasterRequest requestData)
        {
            SelectAllPosMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePosMasterDAL = objFactory.GetDALRepository().GetPosMasterDAL();
                objResponse = (SelectAllPosMasterResponse)objBasePosMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPosMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pos Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllPosMasterResponse SelectAllPosMaster(SelectAllPosMasterRequest objRequest)
        {
            SelectAllPosMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePosMasterDAL = objFactory.GetDALRepository().GetPosMasterDAL();
                objResponse = (SelectAllPosMasterResponse)objBasePosMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPosMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pos Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDPosMasterResponse SelectPosMasterRecord(SelectByIDPosMasterRequest objRequest)
        {
            SelectByIDPosMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }   
                var objBasePosMasterDAL = objFactory.GetDALRepository().GetPosMasterDAL();
                objResponse = (SelectByIDPosMasterResponse)objBasePosMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDPosMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pos Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdatePosMasterResponse UpdatePosMaster(UpdatePosMasterRequest objRequest)
        {
            UpdatePosMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;
                var objBasePosMasterDAL = objFactory.GetDALRepository().GetPosMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objPosMaster = new PosMaster();
                    objPosMaster = (PosMaster)objRequest.RequestDynamicData;
                    objRequest.PosMasterData = objPosMaster;
                }
                objResponse = (UpdatePosMasterResponse)objBasePosMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
					// Changed by Senthamil @ 26.09.2018
					//objRequest.RequestFrom = objRequest.RequestFrom;
					objRequest.RequestFrom = Enums.RequestFrom.MainServer;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.PosMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.POS;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;
                    objRequest.BaseIntegrateStoreID = objRequest.PosMasterData.StoreID;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PosMasterBLL", "UpdatePosMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdatePosMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pos Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeletePosMasterResponse DeletePosMaster(DeletePosMasterRequest objRequest)
        {
            DeletePosMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;
                var objBasePosMasterDAL = objFactory.GetDALRepository().GetPosMasterDAL();
                objResponse = (DeletePosMasterResponse)objBasePosMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.PosMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.POS;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;
                    objRequest.BaseIntegrateStoreID = objRequest.StoreID;

                  //  BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PosMasterBLL", "DeletePosMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeletePosMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Pos Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectPosMasterLookUpResponse SelectPosMasterLookUp(SelectPosMasterLookUpRequest objRequest)
        {
            SelectPosMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePosMasterDAL = objFactory.GetDALRepository().GetPosMasterDAL();
                objResponse = (SelectPosMasterLookUpResponse)objBasePosMasterDAL.SelectPosMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectPosMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pos Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }


        public SelectStoreMasterLookUpResponse SelectStoreMasterLookUp(SelectStoreMasterLookUpRequest objRequest)
        {
            SelectStoreMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreMasterDAL = objFactory.GetDALRepository().GetPosMasterDAL();
                objResponse = (SelectStoreMasterLookUpResponse)objBaseStoreMasterDAL.SelectStoreMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectStoreMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pos Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectStoreMasterLookUpResponse SelectStoreBasedOnStoreGroupandCountryMasterLookUp(SelectStoreMasterLookUpRequest objRequest)
        {
            SelectStoreMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreMasterDAL = objFactory.GetDALRepository().GetPosMasterDAL();
                objResponse = (SelectStoreMasterLookUpResponse)objBaseStoreMasterDAL.SelectStoreBasedOnStoreGroupandCountryMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectStoreMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pos Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectStoreGroupLookUpResponse SelectStoreGroupMasterLookUp(SelectStoreGroupLookUpRequest objRequest)
        {
            SelectStoreGroupLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreMasterDAL = objFactory.GetDALRepository().GetPosMasterDAL();
                objResponse = (SelectStoreGroupLookUpResponse)objBaseStoreMasterDAL.SelectStoreGroupLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectStoreGroupLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pos Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        
    }
}
