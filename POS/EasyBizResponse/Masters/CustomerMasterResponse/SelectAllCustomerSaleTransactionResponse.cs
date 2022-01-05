using EasyBizDBTypes;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CustomerMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllCustomerSaleTransactionResponse: BaseResponseType
    { 
         [DataMember]
        public List<CustomerViewTransactionTypes> CustomerViewTransactionList = new List<CustomerViewTransactionTypes>();
        [DataMember]
        public List<CutomerViewReturnTransaction> CutomerViewReturnTransaction = new List<CutomerViewReturnTransaction>();
     }
}
