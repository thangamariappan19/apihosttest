using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CurrencyResponse
{
	[DataContract]
	[Serializable]
	public class SelectCurreucyDetailsResponse : BaseResponseType
	{
        [DataMember]
		public List<CurrencyDetails> CurrencyDetailsList { get; set; }
	}
}
