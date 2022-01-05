using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Settings
{
    [DataContract]
    [Serializable]
    public class DBConnection
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string ConnectionType { get; set; }
        [DataMember]
        public string ConnectionString { get; set; }
    }
}
