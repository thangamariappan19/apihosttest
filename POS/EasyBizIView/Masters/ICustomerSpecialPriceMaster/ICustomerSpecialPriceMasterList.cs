using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICustomerSpecialPriceMaster
{
    public interface ICustomerSpecialPriceMasterList : IBaseView
    {
        List<CustomerSpecialPriceMasterTypes> CustomerSpecialPriceMasterList { get; set; }
    }
}
