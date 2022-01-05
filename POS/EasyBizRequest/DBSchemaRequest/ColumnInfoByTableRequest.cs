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
    public class ColumnInfoByTableRequest : BaseRequestType
    {
        [DataMember]
        public string DbName { get; set; }

        [DataMember]
        public string TableName { get; set; }
    }
}
