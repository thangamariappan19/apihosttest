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
    public class DeleteExpenseMasterRequest : BaseRequestType
    {
        [DataMember]
        public ExpenseMasterTypes ExpenseMasterTypesData { get; set; }
    }
}
