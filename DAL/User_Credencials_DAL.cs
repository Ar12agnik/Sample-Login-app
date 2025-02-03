using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Sample_Login_app.Models;

namespace Sample_Login_app.DAL
{
    public class User_Credencials_DAL
    {
        string constring = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        public bool Create_user(User_credencials user_Credencials)
        {
            return true;
        }
    }
}