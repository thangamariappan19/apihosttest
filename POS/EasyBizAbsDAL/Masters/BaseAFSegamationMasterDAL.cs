using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.AFSegamationMasterResponse;
using EasyBizResponse.Masters.AFSegamationMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseAFSegamationMasterDAL:BaseDAL
    {
        public abstract SelectSegmentationDetailsResponse SelectSegmentationDetails(SelectSegmentationDetailsRequest ObjRequest);
        public abstract SelectAFSegamationDetailsResponse SelectAFSegmentationDetails(SelectAFSegmationDetailsRequest ObjRequest);

        public abstract SelectAfSegmentationLookUpResponse SelectAfSegmentationLookUp(SelectAFsegmentationLookUpRequest ObjRequest);
        public abstract SelectSegamationDetailsLookUpResponse SelectSegamationDetailsLookUp(SelectSegamationDetailsLookUpRequest ObjRequest);
        public abstract SelectAllAFSegamationMasterResponse API_SelectALL(SelectAllAFSegamationMasterRequest requestData);
    }
}
