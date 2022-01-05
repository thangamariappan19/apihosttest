using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IFingerPrint
{
   public interface IFingerPrintView : IBaseView
    {
        int ID { get; set; }
        string StoreCode { get; }
        List<EmployeeMaster> EmployeeLookUp { get; set; }
        UsersSettings UserInformation { get; }
        long EmployeeID { get; set; }
        string EmployeeName { get; set; }
        string EmployeeCode { get; set; }
        int CountryID { get; }
        int StoreID { get; }
    }
}
