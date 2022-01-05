using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IExpenseMaster
{
    public interface IExpenseMasterView:IBaseView
    {

      

         int ID { get; set; }
         string ExpenseCode { get; set; } 

         string ExpenseName { get; set; }


         List<ExpenseMasterTypes> ExpenseMasterList { get; set; }
    }
}
