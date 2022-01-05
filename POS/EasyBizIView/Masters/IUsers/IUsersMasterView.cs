using EasyBizDBTypes.Masters;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IUsers
{
    public interface IUsersMasterView : IBaseView
    {
        int ID { get; set; }
        string UserCode { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        int EmployeeID { get; set; }
        int RoleID { get; set; }
        int StateID { get; set; }
        // int CountryID { get; set; }
        //int CompanyID { get; set; }
        //int StoreID { get; set; }
        bool Active { get; set; }

        List<UsersSettings> UsersList { get; set; }
        //List<CountryMaster> CountryLookUp { set; }
        //List<CompanySettings> CompanyLookUp { set; } 
        List<RoleMaster> RoleLookUp { get; set; }
        List<EmployeeMaster> EmployeeLookUp {get;  set; }
        // List<StoreMaster> StoreMasterLookUp { set; }
        List<StateMaster> StateMasterLookUp { set; }

        int ManagerOverrideID { get; set; }
        int RetailID { get; set; }
        List<ManagerOverride> ManagerLookUp { get; set; }
        List<RetailSettingsType> RetailLookUp { get; set; }

        bool PasswordReset { get; set; }

        bool IsLoggedIn { get; set; }
        string ManagerOverrideCode { get; }
        String RoleCode { get; }
        string EmployeeCode { get; }
        string RetailCode { get; }
        bool AllowStockEdit { get; set; }
        bool MobileUser { get; set; }
    }
}
