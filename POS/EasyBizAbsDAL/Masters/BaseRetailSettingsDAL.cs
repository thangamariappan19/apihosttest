using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizResponse.Masters.RetailSettingsResponse;
using EasyBizResponse.Masters.RetailSettingsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public  abstract class BaseRetailSettingsDAL:BaseDAL
    {
        
        public abstract SelectRetailSettingsLookUpResponse SelectRetailSettingsLookUp(SelectRetailSettingsLookUpRequest RequestObj);
        public abstract SelectAllRetailResponse API_SelectALL(SelectAllRetailRequest requestData);
    }
}
