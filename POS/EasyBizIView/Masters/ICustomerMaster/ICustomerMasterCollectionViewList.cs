using EasyBizDBTypes.Masters;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICustomerMaster
{
    public interface ICustomerMasterCollectionViewList:IBaseView
    {
        List<CustomerMaster> _CustomerMasterList { get; set; }

        string SearchString { get; set; }
        int CustomerID { get; set; }
        List<CustomerMaster> CustomerMasterList { get; set; }

        ManagerOverride DefaultManagerOverrideSetting { get; set; }
        ManagerOverride ManagerOverrideSetting { get; set; }
        long ManagerOverrideID { get; set; }
        string CustomerSearchString { get; set; }
      
    }
}
