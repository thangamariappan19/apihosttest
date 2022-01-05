using EasyBizDBTypes.Common;
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
    public class SelectAllSKUMasterRequest : BaseRequestType
    {
        [DataMember]
        public string SearchString { get; set; }

        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public string Mode { get; set; }
        [DataMember]
        public int PriceListID { get; set; }
        [DataMember]
        public string BINCode { get; set; }
    }
}
