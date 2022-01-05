using EasyBizDBTypes.Masters;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ManagerOverrideResponse
{
    [DataContract]
    [Serializable]
   public class SelectAllManagerOverrideResponse : BaseResponseType
    {
        public List<ManagerOverride> ManagerOverrideList { get; set; }
    }
}
