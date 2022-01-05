﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IPromotions
{
    public interface IPromotionsCollectionView : IBaseView
    {
        List<PromotionsMaster> PromotionsList { get; set; }
    }
}
