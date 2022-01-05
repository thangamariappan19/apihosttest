using EasyBizDBTypes.Transactions.PriceChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.PriceChange
{
	[DataContract]
	[Serializable]
	public class SelectPriceChangeCountriesResponse : BaseResponseType
	{
		[DataMember]
		public List<PriceChangeCountries> PriceChangeCountriesList { get; set; }
	}
}
