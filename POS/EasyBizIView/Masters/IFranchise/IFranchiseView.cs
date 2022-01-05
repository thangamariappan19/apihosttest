using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IFranchise
{
   public interface IFranchiseView:IBaseView
    {
       int ID { get; set; }
       string FranchiseCode { get; set; }
       string franchiseName { get; set; }
       String Remarks { get; set; }
       bool Active { get; set; }
       List<FranchiseType> FranchiseList { get; set; }

    }
}
