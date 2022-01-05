using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.DesignMasterResponse;
using EasyBizResponse.Masters.DesignMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseDesignMasterDAL:BaseDAL
    {

        public abstract SelectDesignMasterLookUpResponse DesignResponseLookUp(SelectDesignMasterLookUpRequest ObjRequest);
        public abstract SelectDesignGradeLookUpResponse SelectDesignGradeLookUp(SelectDesignGradeLookUpRequest ObjRequest);
        public abstract SelectDesignDevelopmentOfficeLookUpResponse SelectDesignDevelopmentOfficeLookUp(SelectDesignDevelopmentOfficeLookUpRequest ObjRequest);

        public abstract SaveDesignMasterResponse ImportExcelInsert(SaveDesignMasterRequest ObjRequest);

        public abstract SelectAllDesignMasterResponse API_SelectALL(SelectAllDesignMasterRequest requestData);

    }
}
