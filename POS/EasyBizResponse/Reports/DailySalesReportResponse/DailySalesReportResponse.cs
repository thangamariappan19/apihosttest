using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Reports;
using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Reports.DailySalesReportResponse
{
    [DataContract]
    [Serializable]
   public class DailySalesReportResponse : BaseResponseType
    {
         [DataMember]
        public List<DailySalesReport> DailySalesReportList { get; set; }

         [DataMember]
         public DataTable SalesDataTable { get; set; }
    }
}
