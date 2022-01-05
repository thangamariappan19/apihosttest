using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ItemTypeMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllItemTypeMasterResponse : BaseResponseType
    {
        [DataMember]
        public List<ItemTypeMasterTypes> ItemTypeMasterTypesList { get; set; }

    }
}
