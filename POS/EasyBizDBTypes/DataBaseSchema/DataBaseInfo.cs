using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.DataBaseSchema
{
    [DataContract]
    [Serializable]
    public class DataBaseInfo : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string DataBaseName { get; set; }
    }
}
