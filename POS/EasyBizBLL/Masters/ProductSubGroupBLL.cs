using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizResponse.Masters.ProductSubGroupMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class ProductSubGroupBLL
    {
        public SaveProductSubGroupResponse SaveProductSubGroup(SaveProductSubGroupRequest objRequest)
        {
            SaveProductSubGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                // Changed by Senthamil @ 06.09.2018
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseProductSubGroupDAL = objFactory.GetDALRepository().GetProductSubGroupMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    // Changed by Senthamil @ 06.09.2018
                    //objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                    //var objProductSubGroup = new ProductSubGroupMaster();
                    //objProductSubGroup = (ProductSubGroupMaster)objRequest.RequestDynamicData;
                    //objRequest.ProductSubGroupRecord = objProductSubGroup;
                    //objRequest.ProductSubGrouplist = objProductSubGroup.ProductSubGrouplist;

                    var objProductSubGroup = new List<ProductSubGroupMaster>();
                    objProductSubGroup.AddRange(objRequest.RequestDynamicData);
                    objRequest.ProductSubGrouplist = objProductSubGroup;
                }
                objResponse = (SaveProductSubGroupResponse)objBaseProductSubGroupDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PRODUCTSUBGROUP;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ProductSubGroupBLL", "SaveProductSubGroup");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveProductSubGroupResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ProductSubGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeleteProductSubGroupResponse DeleteProductSubGroup(DeleteProductSubGroupRequest objRequest)
        {
            DeleteProductSubGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseProductSubGroupDAL = objFactory.GetDALRepository().GetProductSubGroupMasterDAL();
                objResponse = (DeleteProductSubGroupResponse)objBaseProductSubGroupDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.ProductSubGroupData.ID);
                    objRequest.DocumentType = Enums.DocumentType.PRODUCTSUBGROUP;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ProductSubGroupBLL", "DeleteProductSubGroup");
                }
                
            }
            catch (Exception ex)
            {
                objResponse = new DeleteProductSubGroupResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "ProductSubGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllProductSubGroupResponse SelectAllProductSubGroupRecords(SelectAllProductSubGroupRequest objRequest)
        {
            SelectAllProductSubGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseProductSubGroupDAL = objFactory.GetDALRepository().GetProductSubGroupMasterDAL();
                objResponse = (SelectAllProductSubGroupResponse)objBaseProductSubGroupDAL.SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllProductSubGroupResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ProductSubGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public UpdateProductSubGroupResponse UpdateProductSubGroup(UpdateProductSubGroupRequest objRequest)
        {
            UpdateProductSubGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseProductSubGroupDAL = objFactory.GetDALRepository().GetProductSubGroupMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {                   
                    var objProductSubGroup = new ProductSubGroupMaster();
                    objProductSubGroup = (ProductSubGroupMaster)objRequest.RequestDynamicData;
                    objRequest.ProductSubGroupData = objProductSubGroup;                  
                }
                objResponse = (UpdateProductSubGroupResponse)objBaseProductSubGroupDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.ProductSubGroupData.ID);
                    objRequest.DocumentType = Enums.DocumentType.PRODUCTSUBGROUP;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ProductSubGroupBLL", "UpdateProductSubGroup");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateProductSubGroupResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "ProductSubGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByProductSubGroupIDResponse SelectProductSubGroupRecord(SelectByProductSubGroupIDRequest objRequest)
        {
            SelectByProductSubGroupIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseProductSubGroupDAL = objFactory.GetDALRepository().GetProductSubGroupMasterDAL();
                objResponse = (SelectByProductSubGroupIDResponse)objBaseProductSubGroupDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByProductSubGroupIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ProductSubGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectProductSubGroupLookUpResponse ProductSubGroupLookUp(SelectProductSubGroupLookUpRequest objRequest)
        {
            SelectProductSubGroupLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseProductSubGroupDAL = objFactory.GetDALRepository().GetProductSubGroupMasterDAL();
                objResponse = (SelectProductSubGroupLookUpResponse)objBaseProductSubGroupDAL.SelectProductSubGroupLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectProductSubGroupLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "ProductSubGroup Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectProductGroupListForProductSubGroupResponse ProductSubGroupByProductGroup(SelectProductGroupListForProductSubGroupRequest objRequest)
        {
            SelectProductGroupListForProductSubGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseProductSubGroupDAL = objFactory.GetDALRepository().GetProductSubGroupMasterDAL();
                if (objRequest.ProductGroupID == null || objRequest.ProductGroupID == 0)
                {
                    int ProductGroupId;
                    int.TryParse(objRequest.DocumentIDs,out ProductGroupId);
                    objRequest.ProductGroupID = ProductGroupId;
                }
                objResponse = (SelectProductGroupListForProductSubGroupResponse)objBaseProductSubGroupDAL.SelectProductSubGroupListByProductGroup(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectProductGroupListForProductSubGroupResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ProductSubGroup Brand");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }
    }
}
