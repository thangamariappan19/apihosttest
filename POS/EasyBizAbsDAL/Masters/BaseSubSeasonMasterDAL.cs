using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.SubSeasonMasterRequest;
using EasyBizResponse.Masters.SubSeasonMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseSubSeasonMasterDAL : BaseDAL
    {
        public abstract SelectSubSeasonLookUpResponse SelectSubSeasonLookUp(SelectSubSeasonLookUpRequest ObjRequest);
        public abstract SelectSeasonListForSubSeasonResponse SelectSubSeasonListBySeason(SelectSeasonListForSubSeasonRequest RequestObj);
    }
}
