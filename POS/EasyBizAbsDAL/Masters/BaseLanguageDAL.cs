using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.LanguageRequest;
using EasyBizResponse.Masters.LanguageResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseLanguageDAL:BaseDAL
    {
       public abstract SelectLookUpResponse SelectLanguageLookUp(SelectLookUpRequest ObjRequest);
        public abstract SelectAllLanguageResponse API_SelectAll(SelectAllLanguageRequest objRequest);
        public abstract SelectAllLanguageResponse API_SelectLanguageLookUp(SelectAllLanguageRequest objRequest);
    }
}
