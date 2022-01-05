using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.Brand
{
    public interface IBrandMasterView : IBaseView
    {
        int ID { get; set; }
        string BrandCode { get; set; }
        string BrandName { get; set; }
        string ARBname { get; set; }
        string BrandType { get; set; }
        string BrandLogo { get; set; }
        string Remarks { get; set; }
        Boolean Active { get; set; }

        string ShortDescriptionName { get; set; }
        List<BrandMaster> BrandList { get; set; }
    }
}
