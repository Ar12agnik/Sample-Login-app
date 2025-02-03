using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sample_Login_app.DAL;
using Sample_Login_app.Models;

namespace Sample_Login_app.Controllers
{
    public class dashboardController : Controller
    {
        public bool loggedin =false;
        Login_DAL _DAL = new Login_DAL();
        // GET: dashboard
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("view_dashboard");
            }
            else
            {

                return View();
            }
        }
        public ActionResult login()

        {
            //Session.Add("message", "message");
            if (Session["user"] != null)
            {
                return RedirectToAction("view_dashboard");
            }
            else
            {

                return View();
            }
        }
        public ActionResult register()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("view_dashboard");
            }
            else
            {

                return View();
            }
        }
        [System.Web.Mvc.HttpPost]
        public JsonResult createuser(combinedModels _combined)
        {
            bool usr=_DAL.RegisterUser(_combined);
            if (usr)
            {
                return Json(new
                {
                    messgae = "success",
                    code = 200,
                    redirect_url="login"
                });
            }
            else
            {
                return Json(new
                {
                    messgae = "an Error Occured!",
                    code = 500
                },JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult removesession()
        {
            Session.Remove("message");
            return RedirectToAction("register");
        }
        [System.Web.Mvc.HttpPost]
        public JsonResult Auth()
        {
            try
            {
                string jsonData = new StreamReader(Request.InputStream).ReadToEnd();

                // Deserialize the JSON dynamically.
                dynamic data = JsonConvert.DeserializeObject(jsonData);
                string username = data.username;
                string password = data.password;
                bool temp = _DAL.ValidateUser(username, password);
                if (temp)
                {
                    loggedin = true;
                    Session["user"] = username;
                    return Json(new { message = "Logged in Successfully!!", code = 200,redirect = "view_dashboard" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Invalid Username or password", code = 401 }, JsonRequestBehavior.AllowGet);

                }



            }
            catch (Exception e)
            {
                return Json(new { message = "Error occurred", details = e.Message, Loggedin = loggedin }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult view_dashboard()
        {
            if (Session["user"]!= null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        [System.Web.Mvc.HttpGet]
        public JsonResult getdetails()
        {
            string user=Session["user"].ToString();
            List<user_detailes> _user = _DAL.get_user_detailes(user);
            return Json(_user, JsonRequestBehavior.AllowGet);


        }
        public ActionResult logout()
        {
            Session.Remove("user");
            return RedirectToAction("index");
        }
    }
}