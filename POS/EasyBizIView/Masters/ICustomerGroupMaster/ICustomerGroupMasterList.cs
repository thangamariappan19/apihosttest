using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICustomerGroupMaster
{
   public interface ICustomerGroupMasterList:IBaseView
    {

       List<CustomerGroupMaster> CustomerGroupMasterList { get; set; }
    }
}
