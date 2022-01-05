using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICompanySettingMaster
{
    public interface ICompanySettingCollectionList:IBaseView
    {


       List<CompanySettings> CompanySettingsList { get; set; }
    }
}
