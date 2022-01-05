using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IEmployeeMaster
{
    public interface IEmployeeView : IBaseView
    {
        int ID { get; set; }
        string EmployeeCode { get; set; }
        string EmployeeName { get; set; }
        string RoleName { get; set; }
        DateTime DateofJoining { get; set; }
        int Salary { get; set; }
        string Address { get; set; }
        string PhoneNo { get; set; }
        Boolean IsSelection { get; set; }
        List<RoleMaster> RoleLookUp { get; set; }
        List<EmployeeMaster> EmployeeMasterList { get; set; }
        string Remarks { get; set; }
        long BaseID { get; set; }
        int StoreID { get; set; }
        List<StoreMaster> StoreMasterLookUp { set; }
        int CountryID { get; set; }
        List<CountryMaster> CountryLookUp { set; }   

        string EmployeeImage { get; set; }
        string Designation { get; set; }
        List<DesignationMaster> DesignationLookUp { get; set; }
        string CountryCode { get; }
        string StoreCode { get; }
    }
}
