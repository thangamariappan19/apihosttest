using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Coupons;
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
        public Decimal DiscountValue { get; set; }

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
        public Decimal Remainingamount { get; set; }

        [DataMember]
        public string Redeemedstatus { get; set; }
        [DataMember]
        public List<CommonUtil> StoreCommonUtilData { get; set; }

        [DataMember]
        public List<CommonUtil> CustomerCommonUtilData { get; set; }

        [DataMember]
        public List<CommonUtil> TotalMasterCommonUtilData { get; set; }

        [DataMember]
        public int MaxNumberOfUsagePerCoupon { get; set; }
        [DataMember]
        public bool ISCouponMulitpleApply { get; set; }
        [DataMember]
        public bool IsPartialRedeemAllowed { get; set; }
        [DataMember]
        public bool IsCouponManual { get; set; }
        [DataMember]
        public bool IsLimitedPeriodOffer { get; set; }
        [DataMember]
        public bool IsCouponExpirable { get; set; }
        [DataMember]
        public List<CouponListDetails> ObjCouponListDetails { get; set; }
        [DataMember]
        public CouponReceiptHeader ObjCouponReceiptHeader { get; set; }
        [DataMember]
        public CouponTransferMaster ObjCouponTransferHeader { get; set; }

        [DataMember]
        public int LineNo { get; set; }

        [DataMember]
        public Decimal MinAmount { get; set; }

        [DataMember]
        public int MaxCouponIssuePerDay { get; set; }


        [DataMember]
        public int MaxLimitOfCoupon { get; set; }

        //DateTime CouponIssueDate { get; set; }


        [DataMember]
        public string RedeemType { get; set; }
        [DataMember]
        public int CouponExpiresInNoOfDays { get; set; }
        [DataMember]
        public bool Active { get; set; }
    }
}

