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
    public class ColumnInfo : BaseType
    {
        [DataMember]
        public string COLUMN_NAME { get; set; }

        [DataMember]
        public string DATA_TYPE { get; set; }

        [DataMember]
        public string EXCEL_COLUMN { get; set; }
    }
}
