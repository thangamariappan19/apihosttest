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
    public class PaymentTypeMasterType : BaseType
    {

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string PaymentCode { get; set; }

        [DataMember]
        public string PaymentName { get; set; }

        [DataMember]
        public string PaymentType { get; set; }

        [DataMember]
        public bool CountRequired { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public bool IsCountryNeed { get; set; }


        [DataMember]
        public string CountType { get; set; }

        [DataMember]
        public bool Refundable { get; set; }

        [DataMember]
        public bool RequiredManageApproval { get; set; }

        [DataMember]
        public bool OpenCashDraw { get; set; }

        [DataMember]
        public bool AllowOverTender { get; set; }

        [DataMember]
        public bool AllowPartialTender { get; set; }
        [DataMember]
        public decimal PaymemtValue { get; set; }
        [DataMember]
        public decimal TotalCardValue { get; set; }

        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public byte[] PaymentImage { get; set; }

        [DataMember]
        public List<PaymentTypeMasterType> PaymentImageList { get; set; }
        [DataMember]
        public bool IsPaymentProcesser { get; set; }
        [DataMember]
        public string SortOrder { get; set; }
        [DataMember]
        public string PaymentReceivedType { get; set; }
        [DataMember]
        public int CreateBy { get; set; }
        [DataMember]
        public int UpdateBy { get; set; }

        [DataMember]
        public int PaymentModeID { get; set; }
        [DataMember]
        public string PaymentModeCode { get; set; }
        [DataMember]
        public string TransactionType { get; set; }
    }
}
