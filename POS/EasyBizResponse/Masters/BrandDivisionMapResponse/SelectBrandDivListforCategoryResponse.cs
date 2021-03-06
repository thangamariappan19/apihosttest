using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.BrandDivisionMapResponse
{
    [DataContract]
    [Serializable]
   public class SelectBrandDivListforCategoryResponse : BaseResponseType
    {
        [DataMember]
        public List<BrandDivisionTypes> BrandDivisionList { get; set; }

        public BrandDivisionTypes BrandDivisionRecord { get; set; }
    }
}
