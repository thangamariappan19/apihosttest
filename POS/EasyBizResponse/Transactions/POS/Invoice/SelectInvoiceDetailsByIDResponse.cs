﻿using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.Invoice
{
    [DataContract]
    [Serializable]
   public class SelectInvoiceDetailsByIDResponse:BaseResponseType
    {
        [DataMember]
        public List<InvoiceDetails> InvoiceDetailByIDList { get; set; }
    }
}
