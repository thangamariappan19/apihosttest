using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CurrencyRequest
{
    [DataContract]
    [Serializable]
   public class DeleteCurrencyRequest:BaseRequestType
    {      
        public int ID { get; set; }
    }
}
