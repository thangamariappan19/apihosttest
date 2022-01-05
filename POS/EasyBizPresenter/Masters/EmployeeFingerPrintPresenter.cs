using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IEmployeeMaster;
using EasyBizRequest.Masters.EmployeeFingerPrintRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class EmployeeFingerPrintPresenter
    {
        IEmployeeFingerPrintView _IEmployeeFingerPrintView;
        EmployeeFingerPrintBLL _EmployeeFingerPrintBLL = new EmployeeFingerPrintBLL();

        public EmployeeFingerPrintPresenter(IEmployeeFingerPrintView ViewObj)
        {
            _IEmployeeFingerPrintView = ViewObj;
        }

        public void SelectEmployeeFingerPrintbyID()
        {

            var RequestData = new SelectEmployeeFingerPrintByIDRequest();
            RequestData.ID = _IEmployeeFingerPrintView.ID;
            var ResponseData = _EmployeeFingerPrintBLL.SelectEmployeeFingerPrintByID(RequestData);
            _IEmployeeFingerPrintView.EmployeeCode = ResponseData.EmployeeCode;
            _IEmployeeFingerPrintView.EmployeeName = ResponseData.EmployeeName;
            _IEmployeeFingerPrintView.StoreID = ResponseData.StoreID;
            ////_IEmployeeFingerPrintView.StoreCode = ResponseData.EmployeeFingerPrintRecord.StoreCode;
            //_IEmployeeFingerPrintView.FingerPrint = ResponseData.EmployeeFingerPrintRecord.FingerPrint;
            //_IEmployeeFingerPrintView.EmpFingerPrintList.Add(ResponseData.EmployeeFingerPrintRecord);
            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IEmployeeFingerPrintView.Message = ResponseData.DisplayMessage;
            }
            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IEmployeeFingerPrintView.Message = ResponseData.DisplayMessage;
            }

            _IEmployeeFingerPrintView.EmpFingerPrintList = ResponseData.EmployeeFingerPrintList;
            //_IEmployeeFingerPrintView.EmpFingerPrintList.Add(ResponseData.EmployeeFingerPrintRecord);
            _IEmployeeFingerPrintView.ProcessStatus = ResponseData.StatusCode;
        }

        public void SaveEmployeeFingerPrint()
        {
            var RequestData = new SaveEmployeeFingerPrintRequest();
            RequestData.EmployeeFingerPrintRecord = new EmployeeFingerPrintMaster();

            var NewEmpFingerPrintList = new List<EmployeeFingerPrintMaster>();
            var EmpFingerPrintList = _IEmployeeFingerPrintView.EmpFingerPrintList;
            RequestData.EmployeeFingerPrintList = EmpFingerPrintList;
            RequestData.EmployeeFingerPrintRecord.ID = _IEmployeeFingerPrintView.ID;
            //RequestData.EmployeeFingerPrintRecord.EmployeeID = _IEmployeeFingerPrintView.;
            //RequestData.EmployeeFingerPrintRecord.EmployeeCode = _IEmployeeFingerPrintView.EmployeeCode;
            //RequestData.EmployeeFingerPrintRecord.EmployeeName = _IEmployeeFingerPrintView.EmployeeName;
            //RequestData.EmployeeFingerPrintRecord.StoreID = _IEmployeeFingerPrintView.StoreID;
            //RequestData.EmployeeFingerPrintRecord.FingerPrint = _IEmployeeFingerPrintView.FingerPrint;
            //RequestData.EmployeeFingerPrintRecord.StoreCode = _IEmployeeFingerPrintView.StoreCode;

            RequestData.EmployeeFingerPrintRecord.CreateOn = DateTime.Now;
            // RequestData.EmployeeFingerPrintRecord.CreateBy = _IEmployeeFingerPrintView.UserID;     

            var RequestExistsData = new SelectEmployeeFingerPrintByIDRequest();
            RequestExistsData.ID = _IEmployeeFingerPrintView.ID;
            var ResponseExistsData = _EmployeeFingerPrintBLL.SelectEmployeeFingerPrintByID(RequestExistsData);

            if(ResponseExistsData.StatusCode == Enums.OpStatusCode.Success)
            {
                if (ResponseExistsData.EmployeeFingerPrintList.Count > 0)
                {
                    var RequestDelData = new DeleteEmployeeFingerPrintRequest();
                    RequestDelData.ID = _IEmployeeFingerPrintView.ID;
                    var ResponseDelData = _EmployeeFingerPrintBLL.DeleteEmployeeFingerPrintRecords(RequestDelData);
                }
            }

            var ResponseData = _EmployeeFingerPrintBLL.SaveEmployeeFingerPrintStock(RequestData);
            _IEmployeeFingerPrintView.Message = ResponseData.DisplayMessage;
            _IEmployeeFingerPrintView.ProcessStatus = ResponseData.StatusCode;
            //_IEmployeeFingerPrintView. = Convert.ToInt32(ResponseData.IDs);
            //}
            if (_IEmployeeFingerPrintView.ProcessStatus == Enums.OpStatusCode.Success)
            {
                //UpdateRunningNo();
            }
            else
            {
                _IEmployeeFingerPrintView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }            
        }

        public void DeleteFingerPrintRecords()
        {
            try
            {
                var RequestData = new DeleteEmployeeFingerPrintRequest();
                RequestData.ID = _IEmployeeFingerPrintView.ID;
                var ResponseData = _EmployeeFingerPrintBLL.DeleteEmployeeFingerPrintRecords(RequestData);
                _IEmployeeFingerPrintView.Message = ResponseData.DisplayMessage;
                _IEmployeeFingerPrintView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }           
}

