using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPromotionPriority
{
   public  interface IPromotionPriorityCollectionView:IBaseView
    {
       List<PromotionPriorityType> PromotionPriorityTypeList { get; set; }
     

       

       
    }
}
