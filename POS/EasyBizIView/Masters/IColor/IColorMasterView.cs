using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IColor
{
   public interface IColorMasterView : IBaseView
    {
        int ID { get; set; }
        string InternalCode { get; set; }
        string ColorCode { get; set; }
        string Description { get; set; }
        string Shade { get; set; }
        string NRFCode { get; set; }
        int Colors { get; set; }
        string Attribute1 { get; set; }
        string Attribute2 { get; set; }
        string Remarks { get; set; }
        Boolean Active { get; set; }
        List<ColorMaster> ColorList { get; set; }
        string MulticolorImage { get; set; }
    }
}
