using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.DenominationResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllDenominationResponse : BaseResponseType
    {
        [DataMember]
        public List<ReceivedDenomination> ReceivedDenominationList { get; set; }
       
    }
}
