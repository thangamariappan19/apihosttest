using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.ProductLineMasterRequest;
using EasyBizResponse.Masters.ProductLineMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class ProductLineMasterBLL
    {
        public SaveProductLineMasterResponse SaveProductLineMaster(SaveProductLineMasterRequest objRequest)
        {
            SaveProductLineMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;

                var objBaseProductLineMasterDAL = objFactory.GetDALRepository().GetProductLineMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objProductLineMaster = new ProductLineMaster();
                    objProductLineMaster = (ProductLineMaster)objRequest.RequestDynamicData;
                    objRequest.ProductLineMasterData = objProductLineMaster;
                }
                objResponse = (SaveProductLineMasterResponse)objBaseProductLineMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.ProductLineMasterData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PRODUCTLINE;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ProductLineMasterBLL", "SaveProductLineMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveProductLineMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ProductLine Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllProductLineMasterResponse API_SelectALL(SelectAllProductLineMasterRequest requestData)
        {
            SelectAllProductLineMasterResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                var objBaseProductLineMasterDAL = objFactory.GetDALRepository().GetProductLineMasterDAL();
                objResponse = (SelectAllProductLineMasterResponse)objBaseProductLineMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllProductLineMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ProductLine Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllProductLineMasterResponse SelectAllProductLineMaster(SelectAllProductLineMasterRequest objRequest)
        {
            SelectAllProductLineMasterResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                var objBaseProductLineMasterDAL = objFactory.GetDALRepository().GetProductLineMasterDAL();
                objResponse = (SelectAllProductLineMasterResponse)objBaseProductLineMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllProductLineMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ProductLine Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDProductLineMasterResponse SelectProductLineMasterRecord(SelectByIDProductLineMasterRequest objRequest)
        {
            SelectByIDProductLineMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  
                var objBaseProductLineMasterDAL = objFactory.GetDALRepository().GetProductLineMasterDAL();
                objResponse = (SelectByIDProductLineMasterResponse)objBaseProductLineMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDProductLineMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ProductLine Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateProductLineMasterResponse UpdateProductLineMaster(UpdateProductLineMasterRequest objRequest)
        {
            UpdateProductLineMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseProductLineMasterDAL = objFactory.GetDALRepository().GetProductLineMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objProductLineMaster = new ProductLineMaster();
                    objProductLineMaster = (ProductLineMaster)objRequest.RequestDynamicData;
                    objRequest.ProductLineMasterData = objProductLineMaster;
                }
                objResponse = (UpdateProductLineMasterResponse)objBaseProductLineMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.ProductLineMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.PRODUCTLINE;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ProductLineMasterBLL", "UpdateProductLineMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateProductLineMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ProductLine Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteProductLineMasterResponse DeleteProductLineMaster(DeleteProductLineMasterRequest objRequest)
        {
            DeleteProductLineMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseProductLineMasterDAL = objFactory.GetDALRepository().GetProductLineMasterDAL();
                objResponse = (DeleteProductLineMasterResponse)objBaseProductLineMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.ProductLineMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.PRODUCTLINE;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ProductLineMasterBLL", "DeleteProductLineMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteProductLineMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "ProductLine Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectProductLineLookUpResponse SelectProductLineLookUP(SelectProductLineLookUpRequest objRequest)
        {
            SelectProductLineLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseProductLineMasterDAL = objFactory.GetDALRepository().GetProductLineMasterDAL();
                objResponse = (SelectProductLineLookUpResponse)objBaseProductLineMasterDAL.SelectProductLineLookUp(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectProductLineLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "ProductLine Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }


}
