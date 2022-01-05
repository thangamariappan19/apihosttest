﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace EasyBizRequest.Transactions.POS.Invoice
{
    [DataContract]
    [Serializable]
    public class SearchCommonInvoiceRequest : BaseRequestType
    {

        [DataMember]
        public string SearchString { get; set; }
        [DataMember]
        public int Storeid { get; set; }
    }
}