using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.CityMasterRequest;
using EasyBizResponse.Masters.CityMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseCityMasterDAL : BaseDAL
    {
        public abstract SelectCityLookUPResponse SelectCityLookUP(SelectCityLookUPRequest objRequest);
        public abstract SelectAllCityResponse API_SelectALL(SelectAllCityRequest requestData);
    }
}
