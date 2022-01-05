using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.OnAccountPaymentResponse
{
    [DataContract]
    public class SelectAllOnAccountPaymentResponse :BaseResponseType
    {
        [DataMember]
        public List<OnAccountPayment> OnAccountPaymentList { get; set; }
    }
}
