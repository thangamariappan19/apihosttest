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
	public class PriceChange : BaseType
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public string DocumentNo { get; set; }
		[DataMember]
		public DateTime DocumentDate { get; set; }
		[DataMember]
		public DateTime PriceChangeDate { get; set; }
		[DataMember]
		public string PriceChangeType { get; set; }
		[DataMember]
		public bool MultipleCountry { get; set; }
		[DataMember]
		public int SourceCountryID { get; set; }
		[DataMember]
		public string SourceCountryCode { get; set; }
		[DataMember]
		public string Status { get; set; }
		[DataMember]
		public string Remarks { get; set; }
		[DataMember]
        public bool IsPriceUpdated { get; set; }
        [DataMember]
        public bool IsInProgress { get; set; }
        [DataMember]
        public bool IsNotApplicable { get; set; }
        [DataMember]
        public List<PriceChangeDetails> PriceChangeDetailsList { get; set; }
        [DataMember]
        public List<PriceChangeCountries> PriceChangeCountriesList { get; set; }
        
        
    }
}
