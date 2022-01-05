using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IScale
{
   public interface IScaleView : IBaseView
    {
        int ID { get; set; }

        string ScaleCode { get; set; }

        string ScaleName { get; set; }
        string InternalCode { get; set; }
        string VisualOrder { get; set; }

        List<ScaleMaster> ScaleList { get; set; }
        List<ScaleDetailMaster> ScaleDetailMasterList { get; set; }
        List<BrandMaster> BrandMasterList { get; set; }
        bool Active { get; set; }
        List<BrandMaster> ScaleWithBrandMasterList { get; set; }

        bool ApplytoAll { get; set; }
        List<BrandMaster> SaveBrandMasterList { get; set; }
    }
}
