using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.PriceChange
{
	[DataContract]
	[Serializable]
	public class PriceChangeDetails : BaseType
	{
        [DataMember]
        public int style_serialNo { get; set; }
        [DataMember]
		public int ID { get; set; }
		[DataMember]
		public int HeaderID{ get; set; }
		[DataMember]
		public int StyleID{ get; set; }
		[DataMember]
		public string StyleCode { get; set; }
		[DataMember]
		public int BrandID{ get; set; }
		[DataMember]
		public string BrandCode { get; set; }
		[DataMember]
		public string BrandName { get; set; }
		[DataMember]
		public int CountryID{ get; set; }
		[DataMember]
		public string CountryCode { get; set; }
		[DataMember]
		public int CurrencyID { get; set; }
		[DataMember]
		public int PriceListID { get; set; }
		[DataMember]
		public string CurrencyCode { get; set; }
		[DataMember]
		public string PriceListCode{ get; set; }
		[DataMember]
		public bool PricePointApplicable{ get; set; }
		[DataMember]
		public decimal OldPrice{ get; set; }
		[DataMember]
		public decimal NewPrice { get; set; }
		[DataMember]
		public string Status { get; set; }
		[DataMember]
		public string Remarks { get; set; }
        [DataMember]
        public int BaseCurrencyID { get; set; }
        [DataMember]
        public string PriceType { get; set; }
	}
}
