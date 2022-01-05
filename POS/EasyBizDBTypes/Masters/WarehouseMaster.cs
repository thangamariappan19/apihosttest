using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [Serializable]
    [DataContract]
    public class WarehouseMaster :  BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string WarehouseCode { get; set; }
        [DataMember]
        public string WarehouseName { get; set; }

        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int WarehouseTypeID { get; set; }

        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string WarehouseTypeName { get; set; }
        [DataMember]
        public string RowNumber { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string CompanyCode { get; set; }
        [DataMember]
        public string WarehouseTypeCode { get; set; }
    }
}
