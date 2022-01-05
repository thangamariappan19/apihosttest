using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.PriceChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPriceChange
{
	public interface IPriceChangeView : IBaseView
	{
		int ID { get; set; }
		string DocumentNo { get; set; }
		DateTime DocumentDate { get; set; }
		DateTime PriceChangeDate { get; set; }
		string PriceChangeType { get; set; }
		bool MultipleCountry { get; set; }
		int SourceCountryID { get; set; }
		string SourceCountryCode { get; set; }
		string Status { get; set; }
		string Remarks { get; set; }
		List<CountryMaster> CountryMasterLookup { set; }
		List<PriceChangeCountries> SelectedCountries { get; set; }
		List<PriceChangeDetails> ValidatingPriceChangeDetailsList { get; set; }

	}
}
