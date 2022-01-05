using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SegmentMasterRequest
{
    [Serializable]
    [DataContract]
  public  class DeleteSegmentRequest : BaseRequestType
    {
        [DataMember]
        public string SegmentName { get; set; }

        public int ID { get; set; }
    }
}
