using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class CouponMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CouponCode { get; set; }
        [DataMember]
        public string CouponName { get; set; }

        [DataMember]
        public string Coupondescription { get; set; }

        [DataMember]
        public string BarCode { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string CouponType { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public string DiscountType { get; set; }

        [DataMember]
        public double DiscountValue { get; set; }

        [DataMember]
        public bool IssuableAtPOS { get; set; }

        [DataMember]
        public bool Serial { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public int CouponID { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public string StoreName { get; set; }

        [DataMember]
        public string StoreCode { get; set; }

        [DataMember]
        public string CouponStoreType { get; set; }

        [DataMember]
        public int StoreGroupID { get; set; }

        [DataMember]
        public string CouponCustomerType { get; set; }

        [DataMember]
        public string CouponSerialCode { get; set; }

        [DataMember]
        public string Issuedstatus { get; set; }

        [DataMember]
        public string PhysicalStore { get; set; }

        [DataMember]
        public double Remainingamount { get; set; }

        [DataMember]
        public string Redeemedstatus { get; set; }

    }
}
