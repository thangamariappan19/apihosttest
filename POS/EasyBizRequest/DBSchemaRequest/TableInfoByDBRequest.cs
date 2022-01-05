using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.DBSchemaRequest
{
    [DataContract]
    [Serializable]
    public class TableInfoByDBRequest :BaseRequestType
    {
        [DataMember]
        public string DbName { get; set; }
    }
}
