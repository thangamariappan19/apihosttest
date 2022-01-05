using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.PriceListResponse
{
  

    [DataContract]
    [Serializable]
    public class SelectAllPriceListResponse : BaseResponseType
    {
        [DataMember]
        public List<PriceListType> PriceListTypeList { get; set; }
    }
}
