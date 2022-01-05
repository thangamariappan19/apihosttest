using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizFactory;
using EasyBizRequest.Masters.ArmadaCollectionsMasterRequest;
using EasyBizResponse.Masters.ArmadaCollectionsMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class ArmadaCollectionBLL
    {
        public SelectArmadaCollectionLookUpResponse ArmadaCollectionLookUp(SelectArmadaCollectionLookUpRequest objRequest)
        {
            SelectArmadaCollectionLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseArmadaCollectionMasterDAL = objFactory.GetDALRepository().GetBaseArmadaCollectionMasterDAL();
                objResponse = (SelectArmadaCollectionLookUpResponse)objBaseArmadaCollectionMasterDAL.SelectArmadaCollectionLookUp(objRequest);               
            
            }
            catch (Exception ex)
            {
                objResponse = new SelectArmadaCollectionLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Armada CollectionMaster");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
