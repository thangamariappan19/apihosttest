using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.StoreMasterRequest;
//using EasyBizRequest.Transactions.Promotions.FamilyDiscount;
using EasyBizResponse.Masters.StoreMasterResponse;
//using EasyBizResponse.Transactions.Promotions.FamilyDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseStoreMasterDAL: BaseDAL
    {
        public abstract SelectStoreMasterLookUpResponse SelectStoreMasterLookUp(SelectStoreMasterLookUpRequest RequestObj);
        public abstract SelectByIDStorebrandmappingrespons SelectByIDStoreBrandMappingDetails(SelectByIDStorebrandMappingRequest RequestObj);
        public abstract SelectAllStoreBrandMappingResponse SelectStoreBrandMappingDetails(SelectAllStoreBrandMappingRequest RequestObj);
        public abstract SelectStoreMasterLookUpResponse SelectStoreNameRecord(SelectStoreMasterLookUpRequest RequestObj);

        public abstract SelectStoreGradeLookUpResponse SelectStoreGradeLookUp(SelectStoreGradeLookUpRequest ObjRequest);
        public abstract UpdateUniqueIDResponse UpdateUniqueID(UpdateUniqueIDRequest ObjRequest);
        public abstract SelectByIDStoreMasterResponse SelectedStoreId(SelectByIDStoreMasterRequest ObjRequest);

       // public abstract GetStoreBrandMappingResponse GetStoreBrandMapping(GetStoreBrandMappingRequest RequestData);

        public abstract StoreBrandMapResponse GetStoreBrandMapping(StoreBrandMapRequest objRequest);
        public abstract SelectAllStoreMasterResponse API_SelectALL(SelectAllStoreMasterRequest requestData);
    }
}
