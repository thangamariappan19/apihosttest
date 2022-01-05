using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Reports.DayWiseTransaction
{
    public class StockReturnHeaderTransaction
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int StockRequestID { get; set; }
        [DataMember]
        public string StockRequestDocumentNo { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public String DocumentDate { get; set; }
        [DataMember]
        public int TotalQuantity { get; set; }
        [DataMember]
        public bool Type { get; set; }
        [DataMember]
        public int TotalReceivedQuantity { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string FromWarehouseCode { get; set; }
        [DataMember]
        public string Fromwarehousename { get; set; }
        [DataMember]
        public int FromWareHouseID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
          [DataMember]
        public string ToWareHouse { get; set; }
        
    }
}
