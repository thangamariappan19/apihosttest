using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ISeason
{
  public interface ISeasonview:IBaseView
    {
        int ID { get; set; }
        string SeasonCode { get; set; }
        string SeasonName { get; set; }
        int SeasonDrop{ get; set; }
        Boolean IsSelected { get; set; }
        List<SeasonMaster> SeasonMasterList { get; set; }
        DateTime SeasonStartDate { get; set; }
        DateTime SeasonendDate { get; set; }
        int NoOfWeeks { get; set; }
        int NoOfDays { get; set; }
    }
}
