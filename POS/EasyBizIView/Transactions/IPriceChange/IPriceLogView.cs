using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPriceChange
{
    public interface IPriceLogView : IBaseView
    {
        string FromStyleCode { get; set; }
        string ToStyleCode { get; set; }
        System.Data.DataTable DT_PriceChange { set; }
    }
}
