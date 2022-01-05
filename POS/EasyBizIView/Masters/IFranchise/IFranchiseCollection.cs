using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IFranchise
{
    public interface IFranchiseCollection : IBaseView
    {
        List<FranchiseType> FranchiseTypeList { get; set; }
    }
}
