using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ExpenseMasterResponse
{
   

    [DataContract]
    [Serializable]
    public class SelectByIDExpenseMasterResponse : BaseResponseType
    {

        public ExpenseMasterTypes ExpenseMasterTypesData { get; set; }
    }
}
