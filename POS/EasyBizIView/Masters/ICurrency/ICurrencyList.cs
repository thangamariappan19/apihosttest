using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.Currency
{
   
   public interface ICurrencyList:IBaseView
    {
             List<CurrencyMaster> ICurrencyMasterList { get; set; }
    }
}
