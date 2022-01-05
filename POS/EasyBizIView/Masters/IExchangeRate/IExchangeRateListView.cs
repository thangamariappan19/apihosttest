using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IExchangeRate
{
    public interface IExchangeRateListView :IBaseView
    {
        List<ExchangeRates> ExchangeRatesList { get; set; }

        DateTime BusinessDate { get; }
    }
}
