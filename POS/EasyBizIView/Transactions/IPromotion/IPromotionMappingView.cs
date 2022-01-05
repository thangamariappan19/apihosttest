using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPromotion
{
   public interface IPromotionMappingView : IBaseView
    {
        int ID { get; set; }
        string Countries { get; }
        string WNPromotionCode { get; }
        long WNPromotionID { get; set; }
        List<WNPromotion> WNPromotionLookUp { set; }
        List<PromotionMappingTypes> PromotionMappingList { set; }
        List<PromotionMappingTypes> MappingList { get; set; }
        List<CountryMaster> CountryMasterLookUp {set; }
    }
}
