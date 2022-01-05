using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.LabelPrintingResponse
{
    [DataContract]
    [Serializable]
   public class CommonLabelPrintingReportResponse : BaseResponseType
    {
        [DataMember]
        public DataTable LabelPrintingDataTable { get; set; }
    }
}
