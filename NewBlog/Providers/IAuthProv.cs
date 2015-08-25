using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBlog.Providers
{
  public interface IAuthProv
    {
      bool IsLogged { get; }
      bool Login(string username, string password);

      void Logout();

    }
}
