using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizResponse.Masters.Brand_Response;
using EasyBizResponse.Masters.SubBrandMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class BrandBLL
    {
        public SaveBrandResponse SaveBrand(SaveBrandRequest objRequest)
        {
            SaveBrandResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.BrandRecord = (BrandMaster)objRequest.RequestDynamicData;
                }
                var objBaseBrandDAL = objFactory.GetDALRepository().GetBrandMasterDAL();
                objResponse = (SaveBrandResponse)objBaseBrandDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.BrandRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.BRAND;
                    objRequest.ProcessMode = Enums.ProcessMode.New;


                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.BrandBLL", "SaveBrand");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveBrandResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Brand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectAllBrandResponse API_SelectALL(SelectAllBrandRequest requestData)
        {
            var _SubBrandBLL = new SubBrandBLL();
            SelectAllBrandResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseBrandDAL = objFactory.GetDALRepository().GetBrandMasterDAL();
                objResponse = (SelectAllBrandResponse)objBaseBrandDAL.API_SelectALL(requestData);
                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    var SubBrandList = new List<SubBrandMaster>();
                    foreach (BrandMaster objBrand in objResponse.BrandList)
                    {
                        var objSelectSubBrandListForCategoryRequest = new SelectSubBrandListForCategoryRequest();
                        objSelectSubBrandListForCategoryRequest.BrandID = objBrand.ID;
                        objSelectSubBrandListForCategoryRequest.ShowInActiveRecords = true;
                        var objSelectSubBrandListForCategoryResponse = new SelectSubBrandListForCategoryResponse();
                        objSelectSubBrandListForCategoryResponse = _SubBrandBLL.SubBrandByBrand(objSelectSubBrandListForCategoryRequest);
                        if (objSelectSubBrandListForCategoryResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objBrand.SubBrandList = objSelectSubBrandListForCategoryResponse.SubBrandList;
                        }
                        else
                        {
                            objBrand.SubBrandList = new List<SubBrandMaster>();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                objResponse = new SelectAllBrandResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Brand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public DeleteBrandResponse DeleteBrand(DeleteBrandRequest objRequest)
        {
            DeleteBrandResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseBrandDAL = objFactory.GetDALRepository().GetBrandMasterDAL();
                objResponse = (DeleteBrandResponse)objBaseBrandDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.BrandRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.BRAND;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;


                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.BrandBLL", "DeleteBrand");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteBrandResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Brand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
             
        public SelectAllBrandResponse SelectAllBrandRecords(SelectAllBrandRequest objRequest)
        {
            var _SubBrandBLL = new SubBrandBLL();            
            SelectAllBrandResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseBrandDAL = objFactory.GetDALRepository().GetBrandMasterDAL();
                objResponse = (SelectAllBrandResponse)objBaseBrandDAL.SelectAll(objRequest);
                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {                   
                    var SubBrandList = new List<SubBrandMaster>();
                    foreach (BrandMaster objBrand in objResponse.BrandList)
                    {
                        var objSelectSubBrandListForCategoryRequest = new SelectSubBrandListForCategoryRequest();
                        objSelectSubBrandListForCategoryRequest.BrandID = objBrand.ID;
                        objSelectSubBrandListForCategoryRequest.ShowInActiveRecords = true;
                        var objSelectSubBrandListForCategoryResponse = new SelectSubBrandListForCategoryResponse();
                        objSelectSubBrandListForCategoryResponse = _SubBrandBLL.SubBrandByBrand(objSelectSubBrandListForCategoryRequest);
                        if (objSelectSubBrandListForCategoryResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objBrand.SubBrandList = objSelectSubBrandListForCategoryResponse.SubBrandList;
                        }
                        else
                        {
                            objBrand.SubBrandList = new List<SubBrandMaster>();
                        }                      
                    }
                }
            }
         
            catch (Exception ex)
            {
                objResponse = new SelectAllBrandResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Brand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public UpdateBrandResponse UpdateBrand(UpdateBrandRequest objRequest)
        {
            UpdateBrandResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.BrandRecord = (BrandMaster)objRequest.RequestDynamicData;
                }
                var objBaseBrandDAL = objFactory.GetDALRepository().GetBrandMasterDAL();
                objResponse = (UpdateBrandResponse)objBaseBrandDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.DocumentIDs = Convert.ToString(objRequest.BrandRecord.ID);                 

                    objRequest.RequestFrom = objRequest.RequestFrom;                 
                    objRequest.DocumentType = Enums.DocumentType.BRAND;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.BrandBLL", "UpdateBrand");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateBrandResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Brand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByBrandIDResponse SelectBrandRecord(SelectByBrandIDRequest objRequest)
        {
            SelectByBrandIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }      
                var objBaseBrandDAL = objFactory.GetDALRepository().GetBrandMasterDAL();
                objResponse = (SelectByBrandIDResponse)objBaseBrandDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByBrandIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Brand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectBrandLookUpResponse API_SelectBrandMasterLookUp(SelectBrandLookUpRequest requestData)
        {
            SelectBrandLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBrandDAL = objFactory.GetDALRepository().GetBrandMasterDAL();
                objResponse = (SelectBrandLookUpResponse)objBaseBrandDAL.API_SelectBrandMasterLookUp(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectBrandLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Brand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectBrandLookUpResponse BrandLookUp(SelectBrandLookUpRequest objRequest)
        {
            SelectBrandLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBrandDAL = objFactory.GetDALRepository().GetBrandMasterDAL();
                objResponse = (SelectBrandLookUpResponse)objBaseBrandDAL.SelectBrandLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectBrandLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Brand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
