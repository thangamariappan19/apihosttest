﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IWarehouseMaster
{
    public interface IWarehouseMasterList:IBaseView
    {
        List<WarehouseMaster> WarehouseMasterList { get; set; }
    }
}
