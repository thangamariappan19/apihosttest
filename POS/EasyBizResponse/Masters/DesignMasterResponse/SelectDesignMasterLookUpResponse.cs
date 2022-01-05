using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.DesignMasterResponse
{
    public class SelectDesignMasterLookUpResponse : BaseResponseType
    {

       public List<DesignMasterTypes> DesignMasterTypesList { get; set; }
    }
}
