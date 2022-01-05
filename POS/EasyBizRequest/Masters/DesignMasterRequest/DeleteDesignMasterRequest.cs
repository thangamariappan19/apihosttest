using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.DesignMasterRequest
{

    [DataContract]
    [Serializable]
    public class DeleteDesignMasterRequest : BaseRequestType
    {
        [DataMember]
       public  int ID { get; set; }

        [DataMember]
        public int BrandID { get; set; }
    }
}
