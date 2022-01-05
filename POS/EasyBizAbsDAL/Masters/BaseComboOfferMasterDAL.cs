using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ComboOfferRequest;
using EasyBizResponse.Masters.ComboOfferResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseComboOfferMasterDAL : BaseDAL
    {
        public abstract SelectComboOfferLookUpResponse SelectComboOfferLookUp(SelectComboOfferLookUpRequest ObjRequest);
        public abstract SelectByComboOfferIDResponse SelectComboOfferDetailsList(SelectByComboOfferIDRequest ObjRequest);
        public abstract SelectCPOStyleDetailsResponse SelectCPOStyleDetailsRecord(SelectAllComboOfferRequest ObjRequest);
        public abstract SelectByComboOfferIDResponse SelectStylePricingList(SelectByComboOfferIDRequest ObjRequest);
    }
}
