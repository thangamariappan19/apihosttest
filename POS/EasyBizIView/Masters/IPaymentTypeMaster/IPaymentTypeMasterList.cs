using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IPaymentTypeMaster
{
    public interface IPaymentTypeMasterList :IBaseView
    {
        List<PaymentTypeMasterType> _PaymentTypeMasterList { get; set; }
    }
}
