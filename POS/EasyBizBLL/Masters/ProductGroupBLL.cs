using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizResponse.Masters.ProductGroupResponse;
using EasyBizResponse.Masters.ProductSubGroupMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
  public  class ProductGroupBLL
    {
      public SaveProductGroupResponse SaveProductGroup(SaveProductGroupRequest objRequest)
      {
          SaveProductGroupResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
              BaseProductGroupDAL objProductGroupDAL = objFactory.GetDALRepository().GetProductGroupDAL();
              if (objRequest.RequestDynamicData != null)
              {
                  var objProductGroupMaster = new ProductGroupMaster();
                  objProductGroupMaster = (ProductGroupMaster)objRequest.RequestDynamicData;
                  objRequest.ProductGroupRecord = objProductGroupMaster;
              }
              objResponse = (SaveProductGroupResponse)objProductGroupDAL.InsertRecord(objRequest);
              if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
              {
                  objRequest.RequestFrom = objRequest.RequestFrom;
                  objRequest.ProductGroupRecord.ID = Convert.ToInt32(objResponse.IDs);
                  objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                  objRequest.DocumentType = Enums.DocumentType.PRODUCTGROUP;
                  objRequest.ProcessMode = Enums.ProcessMode.New;

                  //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ProductGroupBLL", "SaveProductGroup");
              }
          }
          catch (Exception ex)
          {
              objResponse = new SaveProductGroupResponse();
              objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Product Group Master");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }

        public SelectAllProductGroupResponse API_SelectALL(SelectAllProductGroupRequest requestData)
        {
            var _ProductSubGroupBLL = new ProductSubGroupBLL();
            SelectAllProductGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseProductGroupDAL objProductGroupDAL = objFactory.GetDALRepository().GetProductGroupDAL();
                objResponse = (SelectAllProductGroupResponse)objProductGroupDAL.API_SelectALL(requestData);
                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    var ProductSubGroupList = new List<ProductSubGroupMaster>();
                    foreach (ProductGroupMaster objProductGroup in objResponse.ProductGroupList)
                    {
                        var objSelectProductGroupListForProductSubGroupRequest = new SelectProductGroupListForProductSubGroupRequest();
                        objSelectProductGroupListForProductSubGroupRequest.ProductGroupID = objProductGroup.ID;
                        objSelectProductGroupListForProductSubGroupRequest.ShowInActiveRecords = true;
                        var objSelectSubBrandListForCategoryResponse = new SelectProductGroupListForProductSubGroupResponse();
                        objSelectSubBrandListForCategoryResponse = _ProductSubGroupBLL.ProductSubGroupByProductGroup(objSelectProductGroupListForProductSubGroupRequest);
                        if (objSelectSubBrandListForCategoryResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objProductGroup.ProductSubGroupList = objSelectSubBrandListForCategoryResponse.ProductSubGroupList;
                        }
                        else
                        {
                            objProductGroup.ProductSubGroupList = new List<ProductSubGroupMaster>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllProductGroupResponse();
                objResponse.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Product Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateProductGroupResponse UpdateProductGroup(UpdateProductGroupRequest objRequest)
      {
          UpdateProductGroupResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
              BaseProductGroupDAL objProductGroupDAL = objFactory.GetDALRepository().GetProductGroupDAL();
              if (objRequest.RequestDynamicData != null)
              {
                  var objProductGroupMaster = new ProductGroupMaster();
                  objProductGroupMaster = (ProductGroupMaster)objRequest.RequestDynamicData;
                  objRequest.ProductGroupRecord = objProductGroupMaster;
              }
              objResponse = (UpdateProductGroupResponse)objProductGroupDAL.UpdateRecord(objRequest);
              if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
              {
                  objRequest.RequestFrom = objRequest.RequestFrom;
                  objRequest.DocumentIDs = Convert.ToString(objRequest.ProductGroupRecord.ID);
                  objRequest.DocumentType = Enums.DocumentType.PRODUCTGROUP;
                  objRequest.ProcessMode = Enums.ProcessMode.Edit;

                  //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ProductGroupBLL", "UpdateProductGroup");
              }
          }
          catch (Exception ex)
          {
              objResponse = new UpdateProductGroupResponse();
              objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Product Group Master");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }
      public DeleteProductGroupResponse DeleteProductGroup(DeleteProductGroupRequest objRequest)
      {
          DeleteProductGroupResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
              BaseProductGroupDAL objProductGroupDAL = objFactory.GetDALRepository().GetProductGroupDAL();
              objResponse = (DeleteProductGroupResponse)objProductGroupDAL.DeleteRecord(objRequest);
              if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
              {
                  objRequest.RequestFrom = objRequest.RequestFrom;
                 // objRequest.DocumentIDs = Convert.ToString(objRequest.ProductGroupRecord.ID);
                  objRequest.DocumentType = Enums.DocumentType.PRODUCTGROUP;
                  objRequest.ProcessMode = Enums.ProcessMode.Delete;

                  BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ProductGroupBLL", "DeleteProductGroup");
              }
          }
          catch (Exception ex)
          {
              objResponse = new DeleteProductGroupResponse();
              objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Product Group Master");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }
      public SelectByIDProductGroupResponse SelectProductGroup(SelectByIDProductGroupRequest objRequest)
      {
          SelectByIDProductGroupResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              if (objRequest.ID == 0)
              {
                  objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
              } 
              BaseProductGroupDAL objProductGroupDAL = objFactory.GetDALRepository().GetProductGroupDAL();
              objResponse = (SelectByIDProductGroupResponse)objProductGroupDAL.SelectRecord(objRequest);
          }
          catch (Exception ex)
          {
              objResponse = new SelectByIDProductGroupResponse();
              objResponse.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Product Group Master");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }
      public SelectAllProductGroupResponse SelectAllProductGroup(SelectAllProductGroupRequest objRequest)
      {
          var _ProductSubGroupBLL = new ProductSubGroupBLL();   
          SelectAllProductGroupResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              BaseProductGroupDAL objProductGroupDAL = objFactory.GetDALRepository().GetProductGroupDAL();
              objResponse = (SelectAllProductGroupResponse)objProductGroupDAL.SelectAll(objRequest);              
              if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
              {
                  var ProductSubGroupList = new List<ProductSubGroupMaster>();
                  foreach (ProductGroupMaster objProductGroup in objResponse.ProductGroupList)
                  {
                      var objSelectProductGroupListForProductSubGroupRequest = new SelectProductGroupListForProductSubGroupRequest();
                      objSelectProductGroupListForProductSubGroupRequest.ProductGroupID = objProductGroup.ID;
                      objSelectProductGroupListForProductSubGroupRequest.ShowInActiveRecords = true;
                      var objSelectSubBrandListForCategoryResponse = new SelectProductGroupListForProductSubGroupResponse();
                      objSelectSubBrandListForCategoryResponse = _ProductSubGroupBLL.ProductSubGroupByProductGroup(objSelectProductGroupListForProductSubGroupRequest);
                      if (objSelectSubBrandListForCategoryResponse.StatusCode == Enums.OpStatusCode.Success)
                      {
                          objProductGroup.ProductSubGroupList = objSelectSubBrandListForCategoryResponse.ProductSubGroupList;
                      }
                      else
                      {
                          objProductGroup.ProductSubGroupList = new List<ProductSubGroupMaster>();
                      }
                  }
              }
          }
          catch (Exception ex)
          {
              objResponse = new SelectAllProductGroupResponse();
              objResponse.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Product Group Master");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }

        public SelectAllProductGroupResponse API_SelectAllProductGroup(SelectAllProductGroupRequest objRequest)
        {
            var _ProductSubGroupBLL = new ProductSubGroupBLL();
            SelectAllProductGroupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseProductGroupDAL objProductGroupDAL = objFactory.GetDALRepository().GetProductGroupDAL();
                objResponse = (SelectAllProductGroupResponse)objProductGroupDAL.API_SelectALL(objRequest);
                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    var ProductSubGroupList = new List<ProductSubGroupMaster>();
                    foreach (ProductGroupMaster objProductGroup in objResponse.ProductGroupList)
                    {
                        var objSelectProductGroupListForProductSubGroupRequest = new SelectProductGroupListForProductSubGroupRequest();
                        objSelectProductGroupListForProductSubGroupRequest.ProductGroupID = objProductGroup.ID;
                        objSelectProductGroupListForProductSubGroupRequest.ShowInActiveRecords = true;
                        var objSelectSubBrandListForCategoryResponse = new SelectProductGroupListForProductSubGroupResponse();
                        objSelectSubBrandListForCategoryResponse = _ProductSubGroupBLL.ProductSubGroupByProductGroup(objSelectProductGroupListForProductSubGroupRequest);
                        if (objSelectSubBrandListForCategoryResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objProductGroup.ProductSubGroupList = objSelectSubBrandListForCategoryResponse.ProductSubGroupList;
                        }
                        else
                        {
                            objProductGroup.ProductSubGroupList = new List<ProductSubGroupMaster>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllProductGroupResponse();
                objResponse.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Product Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectProductGroupLookUpResponse ProductGroupLookUp(SelectProductGroupLookUpRequest objRequest)
      {
          SelectProductGroupLookUpResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              BaseProductGroupDAL objProductGroupDAL = objFactory.GetDALRepository().GetProductGroupDAL();
              objResponse = (SelectProductGroupLookUpResponse)objProductGroupDAL.SelectProductGroupLookUp(objRequest);
          }
          catch (Exception ex)
          {
              objResponse = new SelectProductGroupLookUpResponse();
              objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Product Group Master");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }
    }
}
