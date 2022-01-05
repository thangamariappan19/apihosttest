﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IFreight
{
    public interface IFreightMasterList:IBaseView
    {
        List<FreightMaster> FreightMasterList { get; set; }
    }
}
