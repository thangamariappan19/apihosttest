using EasyBizRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SKUMasterRequest
{
    [Serializable]
    [DataContract]
    public class DeleteSKUMasterRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int BrandID { get; set; }

    }
}
