using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IDesignation
{    
    public interface IDesignationMasterList : IBaseView
    {
        List<DesignationMaster> DesignationMasterList { get; set; }
    }
}
