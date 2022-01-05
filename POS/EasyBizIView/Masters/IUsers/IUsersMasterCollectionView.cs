using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IUsers
{
    public interface IUsersMasterCollectionView : IBaseView
    {
        List<UsersSettings> UsersList { get; set; }
    }
}
