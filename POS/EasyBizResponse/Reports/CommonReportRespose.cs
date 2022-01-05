using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Reports
{
    [DataContract]
    [Serializable]
    public class CommonReportRespose :BaseResponseType
    {
        [DataMember]
        public DataTable ReportDataTable { get; set; }

        [DataMember]
        public DataSet ReportDataSet { get; set; }

        [DataMember]
        public IList<dynamic> ReportList { get; set; }
    }
}
