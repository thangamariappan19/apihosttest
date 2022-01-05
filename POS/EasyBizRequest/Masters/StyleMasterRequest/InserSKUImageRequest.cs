using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StyleMasterRequest
{
    [DataContract]
    [Serializable]
    public  class InserSKUImageRequest :BaseRequestType
    {
        [DataMember]
        public List<ItemImageMaster> ImageList { get; set; }

        [DataMember]
        public int BrandID { get; set; }
    }
}
