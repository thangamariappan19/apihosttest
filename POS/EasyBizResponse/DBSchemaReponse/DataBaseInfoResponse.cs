﻿using EasyBizDBTypes.DataBaseSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.DBSchemaReponse
{
    [DataContract]
    [Serializable]
    public class DataBaseInfoResponse : BaseResponseType
    {
        [DataMember]
        public List<DataBaseInfo> DataBaseInfoList { get; set; }
    }
}
