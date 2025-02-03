using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample_Login_app.Models;

namespace Sample_Login_app.Models
{
    public class combinedModels
    {
        public string Username { get; set; }
        public User_credencials _Credencials { get; set; }
        public user_detailes _Detailes { get; set; }
    }
}