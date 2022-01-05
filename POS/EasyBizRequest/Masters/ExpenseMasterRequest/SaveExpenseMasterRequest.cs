using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ExpenseMasterRequest
{

    [DataContract]
    [Serializable]
    public class SaveExpenseMasterRequest:BaseRequestType
    {

        [DataMember]


        public List<ExpenseMasterTypes> ExpenseMasterTypesData { get; set; }
    
    }
}
