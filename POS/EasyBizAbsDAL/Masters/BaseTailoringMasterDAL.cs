using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.TailoringMasterRequest;
using EasyBizResponse.Masters.TailoringMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseTailoringMasterDAL : BaseDAL
    {
		public abstract SelectAllTailoringMasterByStoreResponse SelectTailorMasterLookUp(SelectAllTailoringMasterByStoreRequest ObjRequest);
        public abstract SelectAllTailoringResponse API_SelectALL(SelectAllTailoringRequest objRequest);
    }
}
