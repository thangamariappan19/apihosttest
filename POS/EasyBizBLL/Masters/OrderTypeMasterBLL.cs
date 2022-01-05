using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.OrderTypeMasterRequest;
using EasyBizResponse.Masters.OrderTypeMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
  public   class OrderTypeMasterBLL
    {
        public SaveOrderTypeMasterResponse SaveOrderTypeMaster(SaveOrderTypeMasterRequest objRequest)
        {
            SaveOrderTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseOrderTypeMasterDAL = objFactory.GetDALRepository().GetOrderTypeMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objOrderTypeMaster = new OrderTypeMaster();
                    objOrderTypeMaster = (OrderTypeMaster)objRequest.RequestDynamicData;
                    objRequest.OrderTypeMasterData = objOrderTypeMaster;
                }
                objResponse = (SaveOrderTypeMasterResponse)objBaseOrderTypeMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.OrderTypeMasterData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.ORDERTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.OrderTypeMasterBLL", "SaveOrderTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveOrderTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "OrderType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllOrderTypeMasterResponse SelectAllOrderTypeMaster(SelectAllOrderTypeMasterRequest objRequest)
        {
            SelectAllOrderTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseOrderTypeMasterDAL = objFactory.GetDALRepository().GetOrderTypeMasterDAL();
                objResponse = (SelectAllOrderTypeMasterResponse)objBaseOrderTypeMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllOrderTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "OrderType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDOrderTypeMasterResponse SelectOrderTypeMasterRecord(SelectByIDOrderTypeMasterRequest objRequest)
        {
            SelectByIDOrderTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  
                var objBaseOrderTypeMasterDAL = objFactory.GetDALRepository().GetOrderTypeMasterDAL();
                objResponse = (SelectByIDOrderTypeMasterResponse)objBaseOrderTypeMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDOrderTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "OrderType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateOrderTypeMasterResponse UpdateOrderTypeMaster(UpdateOrderTypeMasterRequest objRequest)
        {
            UpdateOrderTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseOrderTypeMasterDAL = objFactory.GetDALRepository().GetOrderTypeMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objOrderTypeMaster = new OrderTypeMaster();
                    objOrderTypeMaster = (OrderTypeMaster)objRequest.RequestDynamicData;
                    objRequest.OrderTypeMasterData = objOrderTypeMaster;
                }
                objResponse = (UpdateOrderTypeMasterResponse)objBaseOrderTypeMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.OrderTypeMasterData.ID);
                    //objRequest.DocumentType = Enums.DocumentType.ORDERTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.OrderTypeMasterBLL", "UpdateOrderTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateOrderTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "OrderType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteOrderTypeMasterResponse DeleteOrderTypeMaster(DeleteOrderTypeMasterRequest objRequest)
        {
            DeleteOrderTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseOrderTypeMasterDAL = objFactory.GetDALRepository().GetOrderTypeMasterDAL();
                objResponse = (DeleteOrderTypeMasterResponse)objBaseOrderTypeMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.OrderTypeMasterData.ID);
                    //objRequest.DocumentType = Enums.DocumentType.ORDERTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.OrderTypeMasterBLL", "DeleteOrderTypeMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteOrderTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "OrderType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectOrderTypeMasterLookUpResponse SelectOrderTypeMasterLookUp(SelectOrderTypeMasterLookUpRequest objRequest)
        {
            SelectOrderTypeMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseOrderTypeMasterDAL = objFactory.GetDALRepository().GetOrderTypeMasterDAL();
                objResponse = (SelectOrderTypeMasterLookUpResponse)objBaseOrderTypeMasterDAL.SelectOrderTypeMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectOrderTypeMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "OrderType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }
}
