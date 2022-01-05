using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.AFSegamationMasterResponse;
using EasyBizResponse.Masters.AFSegamationMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class AFSegamationMasterBLL
    {
        public SaveAFSegamationMasterResponse SaveAFSegamationMaster(SaveAFSegamationMasterRequest objRequest)
        {
            SaveAFSegamationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseAFSegamationMaster = objFactory.GetDALRepository().GetAFSegamationMasterDAL();
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if(objRequest.RequestDynamicData != null)
                {
                    var ObjAFSegamationMasterTypes = new AFSegamationMasterTypes();
                    ObjAFSegamationMasterTypes = (AFSegamationMasterTypes)objRequest.RequestDynamicData;
                    objRequest.AFSegamationMasterTypesRecord = ObjAFSegamationMasterTypes;
                    objRequest.AFSegmentationDetailMasterList = ObjAFSegamationMasterTypes.SegmentList;                    
                }


                objResponse = (SaveAFSegamationMasterResponse)BaseAFSegamationMaster.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.AFSegamationMasterTypesRecord.ID = Convert.ToInt32(objResponse.IDs);
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.STYLESEGMENTATION;
                //    objRequest.ProcessMode = Enums.ProcessMode.New;

                //    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AFSegamationMasterBLL", "SaveAFSegamationMaster");
                //}
            
            }
            catch (Exception ex)
            {
                objResponse = new SaveAFSegamationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "AF Segamation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllAFSegamationMasterResponse API_SelectALL(SelectAllAFSegamationMasterRequest requestData)
        {
            SelectAllAFSegamationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseAFSegamationMaster = objFactory.GetDALRepository().GetAFSegamationMasterDAL();
                objResponse = (SelectAllAFSegamationMasterResponse)BaseAFSegamationMaster.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllAFSegamationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "AF Segamation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateAFSegamationMasterResponse UpdateAFSegamationMaster(UpdateAFSegamationMasterRequest objRequest)
        {
            UpdateAFSegamationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseAFSegamationMaster = objFactory.GetDALRepository().GetAFSegamationMasterDAL();
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;

                if (objRequest.RequestDynamicData != null)
                {
                    var ObjAFSegamationMasterTypes = new AFSegamationMasterTypes();
                    ObjAFSegamationMasterTypes = (AFSegamationMasterTypes)objRequest.RequestDynamicData;
                    objRequest.AFSegamationMasterTypesRecord = ObjAFSegamationMasterTypes;                  
                }

                objResponse = (UpdateAFSegamationMasterResponse)BaseAFSegamationMaster.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.AFSegamationMasterTypesRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.STYLESEGMENTATION;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AFSegamationMasterBLL", "UpdateAFSegamationMaster");
                }
            
            }
            catch (Exception ex)
            {
                objResponse = new UpdateAFSegamationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "AF Segamation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }



        public DeleteAFSegamationMasterResponse DeleteAFSegamationMaster(DeleteAFSegamationMasterRequest objRequest)
        {
            DeleteAFSegamationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseAFSegamationMaster = objFactory.GetDALRepository().GetAFSegamationMasterDAL();
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                objResponse = (DeleteAFSegamationMasterResponse)BaseAFSegamationMaster.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.AFSegamationMasterTypesRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.STYLESEGMENTATION;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AFSegamationMasterBLL", "DeleteAFSegamationMaster");
                }
            
            }
            catch (Exception ex)
            {
                objResponse = new DeleteAFSegamationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "AF Segamation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectAllAFSegamationMasterResponse SelectAllAFSegamationMaster(SelectAllAFSegamationMasterRequest objRequest)
        {
            SelectAllAFSegamationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseAFSegamationMaster = objFactory.GetDALRepository().GetAFSegamationMasterDAL();
                objResponse = (SelectAllAFSegamationMasterResponse)BaseAFSegamationMaster.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllAFSegamationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "AF Segamation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectByIDAFSegamationMasterResponse SelectByIDAFSegamationMaster(SelectByIDAFSegamationMasterRequest objRequest)
        {
            SelectByIDAFSegamationMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseAFSegamationMaster = objFactory.GetDALRepository().GetAFSegamationMasterDAL();

                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  

                objResponse = (SelectByIDAFSegamationMasterResponse)BaseAFSegamationMaster.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDAFSegamationMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "AF Segamation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAFSegamationDetailsResponse SelectByIDAFSegamationDetils(SelectAFSegmationDetailsRequest objRequest)
        {
            SelectAFSegamationDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseAFSegamationMaster = objFactory.GetDALRepository().GetAFSegamationMasterDAL();
                objResponse = (SelectAFSegamationDetailsResponse)BaseAFSegamationMaster.SelectAFSegmentationDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAFSegamationDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "AF Segamation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectSegmentationDetailsResponse SelectSegmentationDetails(SelectSegmentationDetailsRequest objRequest)
        {
            SelectSegmentationDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objSegmentationDetailsDAL = objFactory.GetDALRepository().GetAFSegamationMasterDAL();
                objResponse = (SelectSegmentationDetailsResponse)objSegmentationDetailsDAL.SelectSegmentationDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectSegmentationDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Segmentation");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectAfSegmentationLookUpResponse SelectSegmentationLookUp(SelectAFsegmentationLookUpRequest objRequest)
        {
            SelectAfSegmentationLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objSegmentationDetailsDAL = objFactory.GetDALRepository().GetAFSegamationMasterDAL();
                objResponse = (SelectAfSegmentationLookUpResponse)objSegmentationDetailsDAL.SelectAfSegmentationLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAfSegmentationLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Segmentation");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectSegamationDetailsLookUpResponse SegamationDetailsLookUp(SelectSegamationDetailsLookUpRequest objRequest)
        {
            SelectSegamationDetailsLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objSegmentationDetailsDAL = objFactory.GetDALRepository().GetAFSegamationMasterDAL();
                objResponse = (SelectSegamationDetailsLookUpResponse)objSegmentationDetailsDAL.SelectSegamationDetailsLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectSegamationDetailsLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Segmentation Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
