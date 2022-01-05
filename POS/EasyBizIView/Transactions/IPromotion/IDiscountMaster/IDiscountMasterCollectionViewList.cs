using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPromotion.IFamilyDiscount
{
   public interface IDiscountMasterCollectionViewList : IBaseView
    {
       List<FamilyDiscountDetailTypes> _FamilyDiscountList { get; set; }
    }
}
