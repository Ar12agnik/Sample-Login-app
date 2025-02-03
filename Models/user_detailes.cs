using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sample_Login_app.Models
{
    public class user_detailes
    {

        public string First_Name { get; set; }
        public string Middle_name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }
}