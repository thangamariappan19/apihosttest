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
    public class DeleteCustomerMasterResponse:BaseResponseType
    {

        public int ID { get; set; }
    }
}
