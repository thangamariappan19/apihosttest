using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizRequest.Masters.EmployeeFingerPrintRequest;
using EasyBizResponse.Masters.EmployeeFingerPrintResponse;
using ResourceStrings;
using EasyBizTypes.Masters;

namespace EasyBizBLL.Masters
{
    public class EmployeeFingerPrintBLL
    {
        public SaveEmployeeFingerPrintMasterResponse SaveEmployeeFingerPrintStock(SaveEmployeeFingerPrintRequest objRequest)
        {
            SaveEmployeeFingerPrintMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseEmployeeFingerPrintMasterDAL objBaseEmployeeFingerPrintDAL = objFactory.GetDALRepository().GetEmployeeFingerPrintMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objEmployeeFingerPrintMaster = new EmployeeFingerPrintMaster();
                    objEmployeeFingerPrintMaster = (EmployeeFingerPrintMaster)objRequest.RequestDynamicData;
                    objRequest.EmployeeFingerPrintRecord = objEmployeeFingerPrintMaster;
                }
                objResponse = (SaveEmployeeFingerPrintMasterResponse)objBaseEmployeeFingerPrintDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.EmployeeFingerPrintRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.EMPLOYEES;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.EmployeeFingerPrintBLL", "SaveEmployeeFingerPrintStock");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveEmployeeFingerPrintMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Employee Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectEmployeeFingerPrintByIDResponse SelectEmployeeFingerPrintByID(SelectEmployeeFingerPrintByIDRequest objRequest)
        {
            SelectEmployeeFingerPrintByIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }
                BaseEmployeeFingerPrintMasterDAL objBaseEmployeeMasterDAL = objFactory.GetDALRepository().GetEmployeeFingerPrintMasterDAL();
                objResponse = (SelectEmployeeFingerPrintByIDResponse)objBaseEmployeeMasterDAL.SelectEmployeeFingerPrintByID(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectEmployeeFingerPrintByIDResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Employee Finger Print");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeleteEmployeeFingerPrintResponse DeleteEmployeeFingerPrintRecords(DeleteEmployeeFingerPrintRequest objRequest)
        {
            DeleteEmployeeFingerPrintResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseEmployeeFingerPrintDAL = objFactory.GetDALRepository().GetEmployeeFingerPrintMasterDAL();
                objResponse = (DeleteEmployeeFingerPrintResponse)objBaseEmployeeFingerPrintDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.EMPLOYEES;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.EmployeeFingerPrintBLL", "DeleteEmployeeFingerPrintRecords");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteEmployeeFingerPrintResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Employee Finger Print");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
