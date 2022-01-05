using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizRequest.Masters.CollectionMasterResponse;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.SubCollectionRequest;
using EasyBizRequest.Masters.SubCollectionResponse;
using EasyBizResponse.Masters.CollectionMasterResponse;
using EasyBizResponse.Masters.DesignMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class CollectionMasterBLL
    {
        public SaveCollectionMasterResponse SaveCollectionMaster(SaveCollectionMasterRequest objRequest)
        {
            SaveCollectionMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {               
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.CollectionMasterTypesRecord = (CollectionMasterTypes)objRequest.RequestDynamicData;
                }
                var BaseCollection = objFactory.GetDALRepository().GetCollectionMasterDAL();
                if(objRequest.RequestDynamicData != null)
                {
                    var objCollectionMasterTypes = new CollectionMasterTypes();
                    objCollectionMasterTypes = (CollectionMasterTypes)objRequest.RequestDynamicData;
                    objRequest.CollectionMasterTypesRecord = objCollectionMasterTypes;

                }
                objResponse = (SaveCollectionMasterResponse)BaseCollection.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.CollectionMasterTypesRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.COLLECTIONMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.New;


                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CollectionMasterBLL", "SaveCollectionMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveCollectionMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllCollectionMasterResponse API_SelectALL(SelectAllCollectionMasterRequest requestData)
        {
            SelectAllCollectionMasterResponse objResponse = null;
            var _SubCollectionBLL = new SubCollectionBLL();

            var objFactory = new DALFactory();
            try
            {
                var BaseCollection = objFactory.GetDALRepository().GetCollectionMasterDAL();
                objResponse = (SelectAllCollectionMasterResponse)BaseCollection.API_SelectALL(requestData);
                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    var SubBrandList = new List<SubCollectionMaster>();
                    foreach (CollectionMasterTypes objCollection in objResponse.CollectionMasterTypesList)
                    {
                        var objSelectSubCollectionListForCollectionRequest = new SelectSubCollectionListForCollectionRequest();
                        objSelectSubCollectionListForCollectionRequest.CollectionID = objCollection.ID;
                        objSelectSubCollectionListForCollectionRequest.ShowInActiveRecords = true;
                        var objSelectSubCollectionListForCollectionResponse = new SelectSubCollectionListForCollectionResponse();
                        objSelectSubCollectionListForCollectionResponse = _SubCollectionBLL.SelectSubCollectionByCollection(objSelectSubCollectionListForCollectionRequest);
                        if (objSelectSubCollectionListForCollectionResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objCollection.SubCollectionMasterList = objSelectSubCollectionListForCollectionResponse.SubCollectionMasterList;
                        }
                        else
                        {
                            objCollection.SubCollectionMasterList = new List<SubCollectionMaster>();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCollectionMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateCollectionMasterResponse UpdateCollectionMaster(UpdateCollectionMasterRequest objRequest)
        {
            UpdateCollectionMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var BaseCollection = objFactory.GetDALRepository().GetCollectionMasterDAL();
                if(objRequest.RequestDynamicData != null)
                {
                    var objCollectionMasterTypes = new CollectionMasterTypes();
                    objCollectionMasterTypes = (CollectionMasterTypes)objRequest.RequestDynamicData;
                    objRequest.CollectionMasterTypesData = objCollectionMasterTypes;
                }
                objResponse = (UpdateCollectionMasterResponse)BaseCollection.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.CollectionMasterTypesData.ID);
                    objRequest.DocumentType = Enums.DocumentType.COLLECTIONMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CollectionMasterBLL", "UpdateCollectionMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateCollectionMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public DeleteCollectionMasterResponse DeleteCollectionMaster(DeleteCollectionMasterRequest objRequest)
        {
            DeleteCollectionMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var BaseCollection = objFactory.GetDALRepository().GetCollectionMasterDAL();
                objResponse = (DeleteCollectionMasterResponse)BaseCollection.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.CollectionMasterTypesData.ID);
                    objRequest.DocumentType = Enums.DocumentType.COLLECTIONMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CollectionMasterBLL", "DeleteCollectionMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteCollectionMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectAllCollectionMasterResponse SelectAllCollectionMaster(SelectAllCollectionMasterRequest objRequest)
        {
            SelectAllCollectionMasterResponse objResponse = null;
            var _SubCollectionBLL = new SubCollectionBLL();
       
            var objFactory = new DALFactory();
            try
            {
                var BaseCollection = objFactory.GetDALRepository().GetCollectionMasterDAL();
                objResponse = (SelectAllCollectionMasterResponse)BaseCollection.SelectAll(objRequest);
                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    var SubBrandList = new List<SubCollectionMaster>();
                    foreach (CollectionMasterTypes objCollection in objResponse.CollectionMasterTypesList)
                    {
                        var objSelectSubCollectionListForCollectionRequest = new SelectSubCollectionListForCollectionRequest();
                        objSelectSubCollectionListForCollectionRequest.CollectionID = objCollection.ID;
                        objSelectSubCollectionListForCollectionRequest.ShowInActiveRecords = true;
                        var objSelectSubCollectionListForCollectionResponse = new SelectSubCollectionListForCollectionResponse();
                        objSelectSubCollectionListForCollectionResponse = _SubCollectionBLL.SelectSubCollectionByCollection(objSelectSubCollectionListForCollectionRequest);
                        if (objSelectSubCollectionListForCollectionResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objCollection.SubCollectionMasterList = objSelectSubCollectionListForCollectionResponse.SubCollectionMasterList;
                        }
                        else
                        {
                            objCollection.SubCollectionMasterList = new List<SubCollectionMaster>();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCollectionMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectByIDCollectionMasterResponse SelectByIDCollectionMaster(SelectByIDCollectionMasterRequest objRequest)
        {
            SelectByIDCollectionMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }      
                var BaseCollection = objFactory.GetDALRepository().GetCollectionMasterDAL();
                objResponse = (SelectByIDCollectionMasterResponse)BaseCollection.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDCollectionMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectCollectionLookUpResponse CollectionLookUp(SelectCollectionLookUpRequest objRequest)
        {
            SelectCollectionLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseCollection = objFactory.GetDALRepository().GetCollectionMasterDAL();
                objResponse = (SelectCollectionLookUpResponse)BaseCollection.SelectCollectionLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCollectionLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        

        



      

    }
}
