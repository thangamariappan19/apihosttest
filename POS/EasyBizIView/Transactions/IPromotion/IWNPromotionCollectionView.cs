using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPromotion
{
    public interface IWNPromotionCollectionView :IBaseView
    {
        List<WNPromotion> WNPromotionList { set; }
    }
}
