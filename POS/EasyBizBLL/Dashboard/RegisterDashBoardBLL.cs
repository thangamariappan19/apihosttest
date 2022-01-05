using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Dashboard;
using EasyBizFactory;
using EasyBizRequest.DashBoardRequest;
using EasyBizRequest.Masters.DashboardRequest;
using EasyBizResponse.DashBoard;
using EasyBizResponse.Masters.DashboardReponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Dashboard
{
    public class RegisterDashBoardBLL
    {
        public SaveDashBoardResponse SaveDashBoard(SaveDashBoardRequest objRequest)
        {
            SaveDashBoardResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseCollection = objFactory.GetDALRepository().GetRegisterDashBoardDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objReportGenerator = new RegisterDashboard();
                    objReportGenerator = (RegisterDashboard)objRequest.RequestDynamicData;
                    objRequest.DashBoardReportsRecord = objReportGenerator;
                }
                objResponse = (SaveDashBoardResponse)BaseCollection.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.REGISTERDASHBOARD;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Dashboard.RegisterDashBoardBLL", "SaveDashBoard");
                }
               
            }
            catch (Exception ex)
            {
              //  objResponse = new SaveCollectionMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateRegisterDashBoardResponse UpdateDashBoard(UpdateDashBoardRequest objRequest)
        {
            UpdateRegisterDashBoardResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                var objBaseDashBoardReportsDAL = objFactory.GetDALRepository().GetRegisterDashBoardDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objReportGenerator = new RegisterDashboard();
                    objReportGenerator = (RegisterDashboard)objRequest.RequestDynamicData;
                    objRequest.DashBoardReportsRecord = objReportGenerator;
                }
                objResponse = (UpdateRegisterDashBoardResponse)objBaseDashBoardReportsDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.REGISTERDASHBOARD;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Dashboard.RegisterDashBoardBLL", "UpdateDashBoard");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateRegisterDashBoardResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DashBoard Reports");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
            
        }
        public SelectAllRegisterDashBoardResponse GetAllDashBoardReportList(SelectAllRegisterDashboardRequest objRequest)
        {
            SelectAllRegisterDashBoardResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                var objBaseDashBoardReportsDAL = objFactory.GetDALRepository().GetRegisterDashBoardDAL();
                objResponse = (SelectAllRegisterDashBoardResponse)objBaseDashBoardReportsDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllRegisterDashBoardResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DashBoard Reports");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
           
        }
        public  SelectRegisterDashBoardResponse SelectDashBoardReportRecord(SelectDashBoardRequest objRequest)
        {
            SelectRegisterDashBoardResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                var objBaseDashBoardReportsDAL = objFactory.GetDALRepository().GetRegisterDashBoardDAL();
                objResponse = (SelectRegisterDashBoardResponse)objBaseDashBoardReportsDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectRegisterDashBoardResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DashBoard Reports");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
          
        }
        public DeleteRegisterDashBoardResponse DeleteDashBoard(DeleteRegisterDashboardRequest objRequest)
        {
            DeleteRegisterDashBoardResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                var objBaseDashBoardReportsDAL = objFactory.GetDALRepository().GetRegisterDashBoardDAL();
                objResponse = (DeleteRegisterDashBoardResponse)objBaseDashBoardReportsDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.REGISTERDASHBOARD;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Dashboard.RegisterDashBoardBLL", "DeleteDashBoard");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteRegisterDashBoardResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DashBoard Reports");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
           
        }
        public SelectDashboardResponse SelectInbetweendateDetail(SelectDashboardRequest objRequest)
        {
            SelectDashboardResponse objResponse = null;
            DALFactory objFactory = new DALFactory();
            try
            {
                var objBaseDashBoardReportsDAL = objFactory.GetDALRepository().GetDashBoardDAL();
                objResponse = (SelectDashboardResponse)objBaseDashBoardReportsDAL.API_SelectBetweenDayDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectDashboardResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DashBoard Reports");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
 
}
