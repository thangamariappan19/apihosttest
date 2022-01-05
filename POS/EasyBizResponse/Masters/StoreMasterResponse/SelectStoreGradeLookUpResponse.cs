using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StoreMasterResponse
{
   public class SelectStoreGradeLookUpResponse : BaseResponseType
    {
       public List<StoreGradeTypes> StoreGradeList { get; set; }
    }
}
