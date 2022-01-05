using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ComboOfferResponse
{
    [DataContract]
    [Serializable]
    public class SelectByComboOfferIDResponse : BaseResponseType
    {
        //[DataMember]
        //public ComboOfferMaster ComboOfferRecord { get; set; }
        [DataMember]
        public List<PriceListType> PriceTypeList { get; set; }
        [DataMember]
        public ComboOfferMaster ComboOfferRecord { get; set; }
    }
}
