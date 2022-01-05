using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.StyleStatusMasterRequest;
using EasyBizRequest.Masters.StyleStatusMasterResponse;
using EasyBizResponse.Masters.StyleStatusMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseStyleStatusMasterDAL:BaseDAL
    {
        public abstract SelectStyleStatusLookUpResponse SelectStyleStatusLookUp(SelectStyleStatusLookUpRequest ObjRequest);
        public abstract SelectAllStyleStatusMasterResponse API_SelectALL(SelectAllStyleStatusMasterRequest requestData);
    }
}
