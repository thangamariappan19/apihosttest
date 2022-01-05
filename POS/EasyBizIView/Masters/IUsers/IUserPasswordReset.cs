using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IUsers
{
    public interface IUserPasswordReset : IBaseView
    {
        int ID { get; set; }

       // string UserCode { get; }
        string UserName { get; set; }
        string CurrentPassword { get; set; }
        string NewPassword { get; set; }
        string ConfirmPassword { get; set; }
    }
}
