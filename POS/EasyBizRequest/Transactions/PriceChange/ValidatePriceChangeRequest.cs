using EasyBizDBTypes.Transactions.PriceChange;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.PriceChange
{
    [DataContract]
    [Serializable]
    public class ValidatePriceChangeRequest : BaseRequestType
	{
        [DataMember]
        public List<PriceChangeDetails> ValidatingPriceChangeDetailsList { get; set; }
        [DataMember]
        public int SourceCountryID { get; set; }
        [DataMember]
        public string PriceChangeType { get; set; }
	}
}
