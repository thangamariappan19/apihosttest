using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.ExpenseMasterRequest;
using EasyBizResponse.Masters.ExpenseMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class ExpenseMasterBLL
    {

        public SaveExpenseMasterResponse SaveExpenseMaster(SaveExpenseMasterRequest objRequest)
        {

            SaveExpenseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                //objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseExpenseMasterDAL = objFactory.GetDALRepository().GetExpenseMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objExpenseMasterTypes = new List<ExpenseMasterTypes>();
                    objExpenseMasterTypes = (List<ExpenseMasterTypes>)objRequest.RequestDynamicData;
                    objRequest.ExpenseMasterTypesData = objExpenseMasterTypes;
                }
                objResponse = (SaveExpenseMasterResponse)objBaseExpenseMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.RequestFrom = Enums.RequestFrom.MainServer;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.EXPENSEMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ExpenseMasterBLL", "SaveExpenseMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveExpenseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Expense Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllExpenseMasterResponse API_SelectALL(SelectAllExpenseMasterRequest requestData)
        {
            SelectAllExpenseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseExpenseMasterDAL = objFactory.GetDALRepository().GetExpenseMasterDAL();
                objResponse = (SelectAllExpenseMasterResponse)objBaseExpenseMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllExpenseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Expense Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateExpenseMasterResponse UpdateExpenseMaster(UpdateExpenseMasterRequest objRequest)
        {

            UpdateExpenseMasterResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                //objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseExpenseMasterDAL = objFactory.GetDALRepository().GetExpenseMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objExpenseMasterTypes = new ExpenseMasterTypes();
                    objExpenseMasterTypes = (ExpenseMasterTypes)objRequest.RequestDynamicData;
                    objRequest.ExpenseMasterTypesData = objExpenseMasterTypes;
                }
                objResponse = (UpdateExpenseMasterResponse)objBaseExpenseMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;                  
                    objRequest.DocumentIDs = Convert.ToString(objRequest.ExpenseMasterTypesData.ID);
                    objRequest.DocumentType = Enums.DocumentType.EXPENSEMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ExpenseMasterBLL", "UpdateExpenseMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateExpenseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Expense Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeleteExpenseMasterResponse DeleteExpenseMaster(DeleteExpenseMasterRequest objRequest)
        {

            DeleteExpenseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                //objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseExpenseMasterDAL = objFactory.GetDALRepository().GetExpenseMasterDAL();
                objResponse = (DeleteExpenseMasterResponse)objBaseExpenseMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.ExpenseMasterTypesData.ID);
                    objRequest.DocumentType = Enums.DocumentType.EXPENSEMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ExpenseMasterBLL", "DeleteExpenseMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteExpenseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Expense Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectAllExpenseMasterResponse SelectAllExpenseMaster(SelectAllExpenseMasterRequest objRequest)
        {

            SelectAllExpenseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseExpenseMasterDAL = objFactory.GetDALRepository().GetExpenseMasterDAL();
                objResponse = (SelectAllExpenseMasterResponse)objBaseExpenseMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllExpenseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Expense Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByIDExpenseMasterResponse SelectByIDExpenseMasterResponse(SelectByIDExpenseMasterRequest objRequest)
        {
            SelectByIDExpenseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseExpenseMasterDAL = objFactory.GetDALRepository().GetExpenseMasterDAL();
                objResponse = (SelectByIDExpenseMasterResponse)objBaseExpenseMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDExpenseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Expense Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }


        public SelectIDExpenseMasterResponse SelectIDAllExpenseMaster(SelectIDExpenseMasterRequest objRequest)
        {

            SelectIDExpenseMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseExpenseMasterDAL = objFactory.GetDALRepository().GetExpenseMasterDAL();
                objResponse = (SelectIDExpenseMasterResponse)objBaseExpenseMasterDAL.SelectByIDs(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectIDExpenseMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Expense Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


    }
}
