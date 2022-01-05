using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS
{
    public interface IDenominationDetails : IBaseView
    {
        String PayCurrencyCode { get; set; }
         String PaymentType { get; set; }        
         decimal PaymentValue { get; set; }        
         int ValueCount { get; set; }
         decimal TotalValuse { get; set; }       
       
    }
}
