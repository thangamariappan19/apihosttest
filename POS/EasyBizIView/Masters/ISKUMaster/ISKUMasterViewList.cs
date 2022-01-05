using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ISKUMaster
{
   public interface ISKUMasterViewList:IBaseView
    {
       List<SKUMasterTypes> SKUMasterList { get; set; }

    }
}
