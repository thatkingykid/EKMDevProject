using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EKMDevProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RedirectUser()
        {
            try
            {
                var userLogic = new UserLogic();
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                var user = userLogic.GetUserByID((int)Session["UserID"]);
                if(user.Auth.AuthType == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Customer");
                }
            }
            catch (RequestErrorException e)
            {
                Response.StatusCode = 400;
                return Json(new { e.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(new { e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}