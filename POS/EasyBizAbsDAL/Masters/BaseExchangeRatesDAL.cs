using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ExchangeRatesRequest;
using EasyBizResponse.Masters.ExchangeRatesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseExchangeRatesDAL : BaseDAL
    {
        public abstract SelectCurrecnyExchangeRatesResponse SelectCurrecnyExchangeRates(SelectCurrecnyExchangeRatesRequest ObjRequest);
        public abstract SelectAllExchangeRatesResponse API_SelectALL(SelectAllExchangeRatesRequest requestData);
        public abstract SelectCurrecnyExchangeRatesResponse API_SelectCurrencyALL(SelectCurrecnyExchangeRatesRequest requestData);
    }
}
