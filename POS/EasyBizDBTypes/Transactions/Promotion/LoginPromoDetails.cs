using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Promotion
{
    [DataContract]
    [Serializable]
    public class LoginPromoDetails 
    {

       
        [DataMember]
        public string PromotionCode { get; set; }
      
        [DataMember]
        public string PromotionName { get; set; }
    }
}
