using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ISegmentMaster
{
   
    public interface ISegmentMasterViewList:IBaseView
    {
        List<SegmentMaster> SegmentMasterTypesList { get; set; }
    }
}
