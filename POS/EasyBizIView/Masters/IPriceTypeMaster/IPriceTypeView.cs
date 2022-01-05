using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IPriceTypeMaster
{
    public interface IPriceTypeView:IBaseView
    {
        int ID { get; set; }

        string PriceCode { get; set; }

        string PriceName { get; set; }

        string Remarks { get; set; }

        bool Active { get; set; }
    }
}
