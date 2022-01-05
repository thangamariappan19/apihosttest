using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IEmployeeMaster
{
    public interface IEmployeeFingerPrintView : IBaseView
    {
        int ID { get; set;}
        string EmployeeCode { get; set;}
        string EmployeeName { get; set;}
        int StoreID { get; set; }
        List<EmployeeFingerPrintMaster> EmpFingerPrintList { get; set; }
        /*int StoreID { get; set; }
        string StoreCode { get; set; }*/

    }
}
