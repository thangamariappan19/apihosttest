using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.TransactionLogs
{
  public class GetQuantityBySKUResponse : BaseResponseType
    {
      public List<TransactionLog> QuantityBySKUList { get; set; }

      public TransactionLog QuantityBySKUData { get; set; }

       
    }
}
