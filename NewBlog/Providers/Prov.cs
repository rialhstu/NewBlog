using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using NewBlog.Models;
namespace NewBlog.Providers
{
    public class Prov:IAuthProv
    {

        public bool IsLogged
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public bool Login(string username, string password)
        {
            bool reut = FormsAuthentication.Authenticate(username, password);

            if (reut)
                FormsAuthentication.SetAuthCookie(username, false);
            return reut;

        }

        public void Logout()
        {
            FormsAuthentication.SignOut();


        }



    }
}