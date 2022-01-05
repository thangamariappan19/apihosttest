using EasyBizDBTypes.Transactions.Cardex.CardexLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Cardex.CardexLocationResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllCardexLocationResponse : BaseResponseType
    {
        [DataMember]
        public List<CardexLocationDetails> CardexLOcationData = new List<CardexLocationDetails>();
        [DataMember]
        public CardexLocationTotalDetails CardexLocationTotalData { get; set; }
       
    }
}
