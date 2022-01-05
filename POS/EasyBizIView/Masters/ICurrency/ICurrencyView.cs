using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.Currency
{
  public interface ICurrencyView:IBaseView
    {
      int ID { get; set; }
      string CurrencyCode { get; set; }
      string CurrencyName { get; set; }
      string InternationalCode { get; set; }
      int DecimalPlaces { get; set; }
      string CurrencyType { get; set; }
      Decimal MRoundValue { get; set; }
      List<CurrencyMaster> CurrencyMasterList { get; set; }
      string CurrencySymbol { get; set; }
      bool Active { get; set; }

      string InterDescription { get; set; }

      string HundredthName { get; set; }

      string English { get; set; }

      string EngHundredthName { get; set; }

	  List<CurrencyDetails> CurrencyDetailsList { get; set; }
	  List<CurrencyDetails> ViewCurrencyDetailsList { get; set; }

    }
}
