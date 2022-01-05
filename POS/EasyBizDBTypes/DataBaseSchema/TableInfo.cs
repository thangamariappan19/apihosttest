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
    public class TableInfo : BaseType
    {
        [DataMember]
        public string TABLE_NAME { get; set; }

        [DataMember]
        public string TABLE_CATALOG { get; set; }
    }
}
