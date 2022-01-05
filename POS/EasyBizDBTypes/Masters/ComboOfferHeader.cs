using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;

namespace EasyBizRequest.Masters.ComboOfferRequest
{
    [DataContract]
    [Serializable]
    public class ComboOfferHeader : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string productBarcode { get; set; }
        [DataMember]
        public string productSKU { get; set; }
        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public List<ComboOfferDetails> ComboOfferDetailsList { get; set; }


    }
}