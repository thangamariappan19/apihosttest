using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizResponse.Masters.SubBrandMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class SubBrandBLL
    {
        public SaveSubBrandResponse SaveSubBrand(SaveSubBrandRequest objRequest)
        {
            SaveSubBrandResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                // Changed by Senthamil @ 11.09.2018
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseSubBrandDAL = objFactory.GetDALRepository().GetSubBrandMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    //var objSubBrand = new SubBrandMaster();
                    //objSubBrand = (SubBrandMaster)objRequest.RequestDynamicData;
                    //objRequest.SubBrandlist = objSubBrand.SubBrandlist;

                    var objSubBrand = new List<SubBrandMaster>();
                    objSubBrand.AddRange(objRequest.RequestDynamicData);
                    objRequest.SubBrandlist = objSubBrand;
                }
                objResponse = (SaveSubBrandResponse)objBaseSubBrandDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.SUBBRAND;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SubBrandBLL", "SaveSubBrand");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveSubBrandResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SubBrand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllSubBrandResponse API_SelectALL(SelectAllSubBrandRequest requestData)
        {
            SelectAllSubBrandResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSubBrandDAL = objFactory.GetDALRepository().GetSubBrandMasterDAL();
                objResponse = (SelectAllSubBrandResponse)objBaseSubBrandDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSubBrandResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "SubBrand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public DeleteSubBrandResponse DeleteSubBrand(DeleteSubBrandRequest objRequest)
        {
            DeleteSubBrandResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseSubBrandDAL = objFactory.GetDALRepository().GetSubBrandMasterDAL();
                objResponse = (DeleteSubBrandResponse)objBaseSubBrandDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.SubBrandData.ID);
                    objRequest.DocumentType = Enums.DocumentType.SUBBRAND;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SubBrandBLL", "DeleteSubBrand");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteSubBrandResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "SubBrand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllSubBrandResponse SelectAllSubBrandRecords(SelectAllSubBrandRequest objRequest)
        {
            SelectAllSubBrandResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSubBrandDAL = objFactory.GetDALRepository().GetSubBrandMasterDAL();
                objResponse = (SelectAllSubBrandResponse)objBaseSubBrandDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSubBrandResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "SubBrand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public UpdateSubBrandResponse UpdateSubBrand(UpdateSubBrandRequest objRequest)
        {
            UpdateSubBrandResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseSubBrandDAL = objFactory.GetDALRepository().GetSubBrandMasterDAL();
                objResponse = (UpdateSubBrandResponse)objBaseSubBrandDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.SubBrandData.ID);
                    objRequest.DocumentType = Enums.DocumentType.SUBBRAND;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SubBrandBLL", "UpdateSubBrand");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateSubBrandResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "SubBrand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectBySubBrandIDResponse SelectSubBrandRecord(SelectBySubBrandIDRequest objRequest)
        {
            SelectBySubBrandIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSubBrandDAL = objFactory.GetDALRepository().GetSubBrandMasterDAL();
                objResponse = (SelectBySubBrandIDResponse)objBaseSubBrandDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectBySubBrandIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "SubBrand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectSubBrandLookUpResponse SubBrandLookUp(SelectSubBrandLookUpRequest objRequest)
        {
            SelectSubBrandLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSubBrandDAL = objFactory.GetDALRepository().GetSubBrandMasterDAL();
                objResponse = (SelectSubBrandLookUpResponse)objBaseSubBrandDAL.SelectSubBrandLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectSubBrandLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "SubBrand Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectSubBrandListForCategoryResponse SubBrandByBrand(SelectSubBrandListForCategoryRequest objRequest)
        {
            SelectSubBrandListForCategoryResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                // Changed by Senthamil @ 11.09.2018
                var objBaseSubBrandDAL = objFactory.GetDALRepository().GetSubBrandMasterDAL();
                if(objRequest.BrandID == 0)
                {
                    if(!string.IsNullOrEmpty(objRequest.DocumentIDs))
                    {
                        int Doc_Id;
                        int.TryParse(objRequest.DocumentIDs, out Doc_Id);
                        objRequest.BrandID = Doc_Id;
                    }
                }
                objResponse = (SelectSubBrandListForCategoryResponse)objBaseSubBrandDAL.SelectSubBrandListByBrand(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectSubBrandListForCategoryResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Sub Brand");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }
    }
}
