using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ItemMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectByItemIDResponse : BaseResponseType
    {
        [DataMember]
        public ItemMaster ItemRecord { get; set; }
    }
}
