﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.Brand
{
   public interface IBrandMasterListView : IBaseView
    {
       List<BrandMaster> BrandList { get; set; }
    }
}
