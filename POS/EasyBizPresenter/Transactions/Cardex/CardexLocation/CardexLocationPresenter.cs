
using EasyBizBLL.Transactions.Cardex.CardexLocation;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Cardex.CardexLocation;
using EasyBizIView.Transactions.Icardex.IcardexLocation;
using EasyBizRequest.Transactions.Cardex.CardexLocationRequest;


using EasyBizResponse.Transactions.Cardex.CardexLocationResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;


namespace EasyBizPresenter.Transactions.Cardex.CardexLocation
{
    public class CardexLocationPresenter
    {
        public class CardexLocationPresenterList
        {
            IcardexLocationView _IcardexLocationView;
            CardexLocationBLL _CardexLocationBLL = new CardexLocationBLL();
            public CardexLocationPresenterList(IcardexLocationView ViewObj)
            {
                _IcardexLocationView = ViewObj;
            }

            public bool IsValidForm()
            {
                bool objBool = false;
                if (_IcardexLocationView.SearchString == String.Empty)
                {
                    _IcardexLocationView.Message = "Please enter style/sku/Barcode/Supplier Barcode";
                }
                else if (_IcardexLocationView.FromDate.ToString() == "1/1/0001 12:00:00 AM")
                {
                    _IcardexLocationView.Message = "Please Enter From Date";
                }
                else if (_IcardexLocationView.ToDate.ToString() == "1/1/0001 12:00:00 AM")
                {
                    _IcardexLocationView.Message = "Please Enter To Date";
                }
                else
                {
                    objBool = true;
                }
                return objBool;
            }
            public void GetCardexLocation()
            {
                try
                {
                    if (IsValidForm())
                    {
                        var RequestData = new SelectAllCardexLocationRequest();
                        RequestData.ShowInActiveRecords = true;
                        RequestData.RequestFrom = _IcardexLocationView.RequestFrom;
                        RequestData.SearchString = _IcardexLocationView.SearchString;
                        RequestData.StoreID = _IcardexLocationView.StoreID;
                        RequestData.FromDate = _IcardexLocationView.FromDate;
                        RequestData.ToDate = _IcardexLocationView.ToDate;
                        var ResponseData = new SelectAllCardexLocationResponse();
                        ResponseData = _CardexLocationBLL.SelectAllCardexLocation(RequestData);
                        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                        {
                            _IcardexLocationView._CardexLocationViewList = ResponseData.CardexLOcationData;
                            _IcardexLocationView.TotalInQty = ResponseData.CardexLocationTotalData.TotalInQty;
                            _IcardexLocationView.TotalOutQty = ResponseData.CardexLocationTotalData.TotalOutQty;
                            _IcardexLocationView.TotalBalance = ResponseData.CardexLocationTotalData.TotalBalance;
                        }
                        else
                        {
                            _IcardexLocationView._CardexLocationViewList = ResponseData.CardexLOcationData;
                            _IcardexLocationView.Message = ResponseData.DisplayMessage;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
