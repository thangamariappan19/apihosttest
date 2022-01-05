using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ITax
{
    public interface ITaxView : IBaseView
    {
        int ID { get; set; }

        //string TaxCode { get; set; }

        //Decimal TaxPercentage { get; set; }
        //bool Sales { get; set; }
        //bool Purchase { get; set; }

        List<TaxMaster> TaxList { get; set; }
    }
}
