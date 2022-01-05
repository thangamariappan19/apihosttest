using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Dashboard
{
    [Serializable]
    [DataContract]
    public class RegisterDashboard : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ReportName { get; set; }      
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Byte[] ReportFile { get; set; }
        [DataMember]
        public bool IsActive { get; set; }

    }
}
