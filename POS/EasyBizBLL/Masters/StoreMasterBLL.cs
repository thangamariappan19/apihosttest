using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.StoreMasterResponse;
//using EasyBizRequest.Transactions.Promotions.FamilyDiscount;
using EasyBizResponse.Masters.StoreMasterResponse;
//using EasyBizResponse.Transactions.Promotions.FamilyDiscount;
using MsSqlDAL.SyncSettings;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class StoreMasterBLL
    {
        public SelectStoreMasterLookUpResponse SelectStoreMasterLookUp(SelectStoreMasterLookUpRequest objRequest)
        {
            SelectStoreMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreMasterDAL = objFactory.GetDALRepository().GetStoreMasterDAL();
                objResponse = (SelectStoreMasterLookUpResponse)objBaseStoreMasterDAL.SelectStoreMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectStoreMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectStoreMasterLookUpResponse SelectStorename(SelectStoreMasterLookUpRequest objRequest)
        {
            SelectStoreMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreMasterDAL = objFactory.GetDALRepository().GetStoreMasterDAL();
                objResponse = (SelectStoreMasterLookUpResponse)objBaseStoreMasterDAL.SelectStoreNameRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectStoreMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllStoreMasterResponse API_SelectALL(SelectAllStoreMasterRequest requestData)
        {
            SelectAllStoreMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreMasterDAL = objFactory.GetDALRepository().GetStoreMasterDAL();
                objResponse = (SelectAllStoreMasterResponse)objBaseStoreMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStoreMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SaveStoreMasterResponse SaveStoreMaster(SaveStoreMasterRequest objRequest)
        {
            SaveStoreMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseStoreMasterDAL = objFactory.GetDALRepository().GetStoreMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStoreMaster = new StoreMaster();
                    objStoreMaster = (StoreMaster)objRequest.RequestDynamicData;
                    objRequest.StoreMasterRecord = objStoreMaster;
                    objRequest.StoreBrandMappingList = objStoreMaster.SelectStoreBrandMappingList;
                    objRequest.StoreImageList = objStoreMaster.StoreImageList;

					// Changed by Senthamil @ 11.09.2018
					objRequest.StoreBrandMappingList = objStoreMaster.SelectStoreBrandMappingList;
                }
                objResponse = (SaveStoreMasterResponse)objBaseStoreMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.StoreMasterRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STORE;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StoreMasterBLL", "SaveStoreMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStoreMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public UpdateStoreMasterResponse UpdateStoreMaster(UpdateStoreMasterRequest objRequest)
        {
            UpdateStoreMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                //objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseStoreMasterDAL = objFactory.GetDALRepository().GetStoreMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objStoreMaster = new StoreMaster();
                    objStoreMaster = (StoreMaster)objRequest.RequestDynamicData;
                    objRequest.StoreMasterRecord = objStoreMaster;
                    objRequest.StoreImageList = objStoreMaster.StoreImageList;
					// Changed by Senthamil @ 11.09.2018
					objRequest.StoreBrandMappingList = objStoreMaster.SelectStoreBrandMappingList;
                }
                objResponse = (UpdateStoreMasterResponse)objBaseStoreMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.StoreMasterRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.STORE;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StoreMasterBLL", "UpdateStoreMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateStoreMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public DeleteStoreMasterResponse DeleteStoreMaster(DeleteStoreMasterRequest objRequest)
        {
            DeleteStoreMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseStoreMasterDAL = objFactory.GetDALRepository().GetStoreMasterDAL();
                objResponse = (DeleteStoreMasterResponse)objBaseStoreMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                  //  objRequest.DocumentIDs = Convert.ToString(objRequest.StoreMasterRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.STORE;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StoreMasterBLL", "DeleteStoreMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteStoreMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectAllStoreMasterResponse SelectAllStoreMaster(SelectAllStoreMasterRequest objRequest)
        {
            SelectAllStoreMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStoreMasterDAL = objFactory.GetDALRepository().GetStoreMasterDAL();
                objResponse = (SelectAllStoreMasterResponse)objBaseStoreMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStoreMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }


        public SelectByIDStoreMasterResponse SelectByIDStoreMaster(SelectByIDStoreMasterRequest objRequest)
        {
            SelectByIDStoreMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  
                var BaseStoreMasterDAL = objFactory.GetDALRepository().GetStoreMasterDAL();
                objResponse = (SelectByIDStoreMasterResponse)BaseStoreMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDStoreMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectStoreGradeLookUpResponse GradeLookUp(SelectStoreGradeLookUpRequest objRequest)
        {
            SelectStoreGradeLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseCollection = objFactory.GetDALRepository().GetStoreMasterDAL();
                objResponse = (SelectStoreGradeLookUpResponse)BaseCollection.SelectStoreGradeLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectStoreGradeLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateUniqueIDResponse UpdateUniqueID(UpdateUniqueIDRequest objRequest)
        {
            UpdateUniqueIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseCollection = objFactory.GetDALRepository().GetStoreMasterDAL();
                objResponse = (UpdateUniqueIDResponse)BaseCollection.UpdateUniqueID(objRequest);

                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.DocumentIDs = Convert.ToString(objRequest.ID);                   
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;
                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StoreMasterBLL", "UpdateUniqueID");
                }

            }
            catch (Exception ex)
            {
                objResponse = new UpdateUniqueIDResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public void InsertDBConnections(string ConnectionString)
        {
            SyncSettingsDAL _SyncSettingsDAL = new SyncSettingsDAL();
            _SyncSettingsDAL.InsertDBConnection(ConnectionString);
        }

        public SelectByIDStoreMasterResponse SelectedStoreId(SelectByIDStoreMasterRequest objRequest)
        {
            SelectByIDStoreMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }
                var BaseStoreMasterDAL = objFactory.GetDALRepository().GetStoreMasterDAL();
                objResponse = (SelectByIDStoreMasterResponse)BaseStoreMasterDAL.SelectedStoreId(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDStoreMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
           

        public StoreBrandMapResponse GetStoreBrandMappingDetails(StoreBrandMapRequest objRequest)
        {
            StoreBrandMapResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseStoreMasterDAL = objFactory.GetDALRepository().GetStoreMasterDAL();
                objResponse = (StoreBrandMapResponse)BaseStoreMasterDAL.GetStoreBrandMapping(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new StoreBrandMapResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreBrand Map");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
    }
       
}
