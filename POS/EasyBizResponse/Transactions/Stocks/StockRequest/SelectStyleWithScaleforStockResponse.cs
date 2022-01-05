﻿using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockRequest
{
    [DataContract]
    [Serializable]
   public class SelectStyleWithScaleforStockResponse:BaseResponseType
    {
        [DataMember]
        public List<ScaleDetailMaster> ScaleDetailMasterRecordForStock { get; set; }
    }
}
