using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Reports
{
    public class UserReport : BaseType
    {
        public int ID { get; set; }
        public string ReportName { get; set; }
        public byte[] ReportFile { get; set; }
        public string ViewRoles { get; set; }
        public string Remarks { get; set; }
    }
}
