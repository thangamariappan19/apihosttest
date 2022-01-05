using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.LabelPrintingRequest
{
    [DataContract]
    [Serializable]
   public class CommonLabelReportRequest : BaseRequestType
    {
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string ProductCode { get; set; }
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string SizeCode { get; set; }
        [DataMember]
        public int NoOfLabel { get; set; }
        [DataMember]
        public Boolean PrintPrice { get; set; }
        [DataMember]
        public int StoreID { get; set; }
    }
}
