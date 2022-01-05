using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CouponMasterRequest
{
    [Serializable]
    [DataContract]
    public class SaveCouponMasterRequest : BaseRequestType
    {
        [DataMember]
        public CouponMaster CouponMasterData { get; set; }


        [DataMember]
        public List<StoreMaster> StoreMasterList { get; set; }

        [DataMember]
        public List<StoreGroupMaster> StoreGroupMasterList { get; set; }

        [DataMember]
        public List<CustomerMaster> CustomerMasterList { get; set; }

        [DataMember]
        public List<CustomerGroupMaster> CustomerGroupMasterList { get; set; }

        [DataMember]
        public List<AFSegamationMasterTypes> _SegamationList { get; set; }
        [DataMember]
        public List<YearMaster> _YearList { get; set; }
        [DataMember]
        public List<BrandMaster> _BrandList { get; set; }
        [DataMember]
        public List<SubBrandMaster> _SubBrandList { get; set; }
        [DataMember]
        public List<SeasonMaster> _SeasonList { get; set; }
        [DataMember]
        public List<ProductGroupMaster> _ProductGroupList { get; set; }
        [DataMember]
        public List<ProductSubGroupMaster> _ProductSubGroupList { get; set; }

        [DataMember]
        public List<CommonUtil> StoreCommonUtilData { get; set; }

        [DataMember]
        public List<CommonUtil> CustomerCommonUtilData { get; set; }


        [DataMember]
        public List<CommonUtil> TotalMasterCommonUtilData { get; set; }

        //[DataMember]
        //public List<CommonUtil> CouponDetailsList { get; set; }

        [DataMember]
        public List<CouponListDetails> CouponDetailsList { get; set; }

        [DataMember]
        public List<CommonUtil> StoreTypeList { get; set; }
        [DataMember]
        public List<CommonUtil> CustomerTypeList { get; set; }
        [DataMember]
        public List<CommonUtil> ProductTypeList { get; set; }

        [DataMember]
        public bool IsExpirable { get; set; }

        [DataMember]
        public decimal MinBillAmount { get; set; }

        [DataMember]
        public int MaxNoIssue { get; set; }

        [DataMember]
        public decimal MaxLimit { get; set; }

        [DataMember]
        public int RedeemType { get; set; }

        [DataMember]
        public int ExpiryDays { get; set; }

    }
}
