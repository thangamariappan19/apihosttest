using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SKUMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllSKUImagesResponse : BaseResponseType
    {
        [DataMember]

        public List<ItemImageMaster> SKUImageList { get; set; }
    }
}
