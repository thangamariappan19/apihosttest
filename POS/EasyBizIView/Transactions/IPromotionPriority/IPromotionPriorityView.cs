using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPromotionPriority
{
    public interface IPromotionPriorityView : IBaseView
    {
        List<PromotionPriorityType> PromotionPriorityTypeList { get; set; }
        List<PromotionsMaster> PromotionsMasterList { get; set; }
        int PriceListID { get; set; }
        List<PriceListType> PriceListLookUp { set; }
        PromotionPriorityType PromotionPriorityTypeRecord { get; set; }
      

    }
}
