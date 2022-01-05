using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizResponse.Masters.CurrencyResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseCurrencyDAL:BaseDAL
    {
       public abstract SelectCurrencyLookUpResponse SelectCurrencyLookUp(SelectCurrencyLookUpRequest ObjRequest);
	   public abstract SelectCurreucyDetailsResponse SelectCurrencyDetails(SelectCurrencyDetailsRequest ObjRequest);
        public abstract SelectAllCurrencyResponse API_SelectALL(SelectAllCurrencyRequest requestData);
        public abstract SelectCurrencyLookUpResponse API_SelectCurrencyLookUp(SelectCurrencyLookUpRequest objRequest);
    }
}
