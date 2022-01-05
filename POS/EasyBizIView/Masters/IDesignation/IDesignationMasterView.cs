using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IDesignation
{
    public interface IDesignationMasterView : IBaseView
    {

        int ID { get; set; }

        string DesignationCode { get; set; }

        string DesignationName { get; set; }

        int RoleId { get; set; }

        string RoleCode { get; }

        string Description { get; set; }

        List<RoleMaster> RoleMasterLookup { set; }

        string Remarks { get; set; }

        bool Active { get; set; }

    }
}
