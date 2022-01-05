using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

 
namespace EasyBizRequest.Masters.CustomerMasterRequest
{

    [DataContract]
    [Serializable]

    public class SelectByIDCustomerMasterRequest:BaseRequestType
    {
        [DataMember]

        public int ID { get; set; }
         [DataMember]
        public String Source { get; set; }       
       
         [DataMember]
        public String CustomerInfo { get; set; }

    }
}
