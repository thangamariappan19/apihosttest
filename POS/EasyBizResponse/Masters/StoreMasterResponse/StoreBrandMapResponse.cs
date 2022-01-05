using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.StoreMasterResponse
{
   public class StoreBrandMapResponse : BaseResponseType
    {

       public List<StoreBrandMapping> StoreBrandMapList { get; set; }

       public StoreMaster StoreBrandData { get; set; }
    }
}
