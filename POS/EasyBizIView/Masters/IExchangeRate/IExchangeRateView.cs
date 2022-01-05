using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IExchangeRate
{
    public interface IExchangeRateView :IBaseView
    {
        List<CurrencyMaster> CurrencyList { get; set; }
        List<ExchangeRates> ExchangeRatesList { get; set; }
        DateTime ExchangeRateDate { get; set; }
        string ExchangeRatesCode { get; set; }

        List<ExpandoObject> ExchangeExpandoList { set; }
    }
}
