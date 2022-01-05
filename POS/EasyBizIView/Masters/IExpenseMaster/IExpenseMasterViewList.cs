using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IExpenseMaster
{
    public interface IExpenseMasterViewList:IBaseView
    {
        List<ExpenseMasterTypes> ExpenseMasterList { get; set; }
    }
}
