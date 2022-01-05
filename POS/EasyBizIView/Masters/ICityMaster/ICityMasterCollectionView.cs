using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICityMaster
{
    public interface ICityMasterCollectionView : IBaseView
    {
        List<CityMaster> CityList { get; set;}
    }
}
