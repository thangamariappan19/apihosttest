using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.PaymentTypeResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllPaymentTypeResponse:BaseResponseType
    {

        [DataMember]
        public List<PaymentType> PaymentTypeList { get; set; }
    }
}
