using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.GiftvoucherDetailsResponse
{
    [DataContract]
    [Serializable]
   public class SelectGiftvoucherDetailByInvoiceNoResponse:BaseResponseType
    {
        [DataMember]
        public GiftvoucherDetail GiftvoucherDetailData { get; set; }
    }
}
