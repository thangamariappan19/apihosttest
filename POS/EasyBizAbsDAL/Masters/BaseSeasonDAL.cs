using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizResponse.Masters.SeasonResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseSeasonDAL:BaseDAL
    {
       public abstract SelectSeasonLookUpResponse SelectSeasonLookUp(SelectSeasonLookUpRequest ObjRequest);
       public abstract SelectAllSeasonResponse API_SelectALL(SelectAllSeasonRequest requestData);
    }
}
