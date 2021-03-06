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
    public class SelectAllComboOfferResponse : BaseResponseType
    {
       [DataMember]
        public List<ComboOfferMaster> ComboOfferList { get; set; }
    }
}
