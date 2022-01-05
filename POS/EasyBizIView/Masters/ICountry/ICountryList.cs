using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.Country
{
    public interface ICountryList : IBaseView
    {
        List<CountryMaster> CountryMasterList { get; set; }
    }
}
