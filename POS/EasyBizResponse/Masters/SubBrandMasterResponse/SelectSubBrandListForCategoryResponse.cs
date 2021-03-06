using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SubBrandMasterResponse
{

    [DataContract]
    [Serializable]
   public class SelectSubBrandListForCategoryResponse : BaseResponseType
    {
        [DataMember]
        public List<SubBrandMaster> SubBrandList { get; set; }
        public SubBrandMaster SubBrandMasterRecord { get; set; }
    }
}
