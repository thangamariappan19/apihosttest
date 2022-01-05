using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.TailoringMasterRequest
{
	[DataContract]
	[Serializable]
	public class SelectAllTailoringMasterByStoreRequest : BaseRequestType
	{
		public int StoreID { get; set; }
	}
}
