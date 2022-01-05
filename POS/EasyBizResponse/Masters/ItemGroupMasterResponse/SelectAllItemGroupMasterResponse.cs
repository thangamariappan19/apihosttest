using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ItemGroupMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllItemGroupMasterResponse : BaseResponseType
    {
        [DataMember]
        public List<ItemGroupMasterTypes> ItemGroupMasterTypesList { get; set; }

    }
}
