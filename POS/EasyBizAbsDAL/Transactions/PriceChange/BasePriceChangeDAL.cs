using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.PriceChange;
using EasyBizResponse.Transactions.PriceChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.PriceChange
{
	public abstract class BasePriceChangeDAL : BaseDAL
	{
		public abstract ValidatePriceChangeResponse ValidatePriceChangeDetails(ValidatePriceChangeRequest ObjRequest);
		public abstract SelectPriceChangeDetailsResponse GetPriceChangeDetails(SelectPriceChangeDetailsRequest ObjRequest);
		public abstract SelectPriceChangeCountriesResponse GetPriceChangeCountries(SelectPriceChangeCountriesRequest ObjRequest);
        public abstract SelectPriceChangeStatusResponse SelectPriceChangeStatus(SelectPriceChangeStatusRequest ObjRequest);
        public abstract PriceUpdateResponse UpdateStylePrice(PriceUpdateRequest ObjRequest);
        public abstract SelectPriceChangeLogResponse SelectPriceChangeLog(SelectPriceChangeLogRequest ObjRequest);
        public abstract SelectAllPriceChangeResponse API_SelectALL(SelectAllPriceChangeRequest requestData);
    }
}
