using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.CompanySettingResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseCompanySettingDAL: BaseDAL
    {
        public abstract SelectCompanySettingsLookUpResponse SelectCompanySettingsLookUp(SelectCompanySettingsLookUpRequest RequestObj);
        public abstract SelectAllCompanySettingResponse API_SelectAll(SelectAllCompanySettingRequest objRequest);
        public abstract SelectAllCompanySettingResponse API_SelectCompanySettingLookUp(object objRequest);
    }
}
