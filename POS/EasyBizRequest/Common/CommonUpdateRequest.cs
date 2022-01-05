using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Common
{
    [DataContract]
    [Serializable]
    public class CommonUpdateRequest : BaseRequestType
    {
        [DataMember]
        public string TableName { get; set; }

        [DataMember]
        public int DocumentID { get; set; }
        
    }
}
