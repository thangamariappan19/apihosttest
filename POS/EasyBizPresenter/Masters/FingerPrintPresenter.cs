using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
//using EasyBizIView.Masters.IFingerPrint;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class FingerPrintPresenter
    {
        //IFingerPrintView _IFingerPrintView;
        //EmployeeMasterBLL _EmployeeMasterBLL = new EmployeeMasterBLL();



        //public FingerPrintPresenter(IFingerPrintView ViewObj)
        //{
        //    _IFingerPrintView = ViewObj;
        //}

        //public void GetEmployeeByStore()
        //{
        //    try
        //    {
        //        var RequestData = new GetEmployeeByStoreRequest();
        //        RequestData.ShowInActiveRecords = false;
        //        RequestData.StoreID = _IFingerPrintView.UserInformation.StoreID;
        //        var ResponseData = _EmployeeMasterBLL.GetEmployeeByStore(RequestData);
        //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            _IFingerPrintView.EmployeeLookUp = ResponseData.EmployeeList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
