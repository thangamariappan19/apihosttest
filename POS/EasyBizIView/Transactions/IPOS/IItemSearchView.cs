using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS
{
    public interface IItemSearchView
    {
        List<SKUMasterTypes> DefaultSKUList { get; set; }
        List<SKUMasterTypes> SearchSKUList { get; set; }
        Enums.RequestFrom RequestFrom { get; set; }
        string SearchString { get; set; }
        UsersSettings UserInformation { get; }
    }
}
