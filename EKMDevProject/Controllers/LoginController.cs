using BusinessLogic;
using EKMDevProject.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BusinessLogic.UserLogic;

namespace EKMDevProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 400;
                    return Content("Bad login");
                }
                var userLogic = new UserLogic();
                var userID = userLogic.Login(model.Username, model.Password);
                if(userID == 0)
                {
                    Response.StatusCode = 400;
                    return Content("bad login");
                }
                else
                {
                    Session["UserID"] = userID;
                    return Content("OK");
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Content("Bad login");
            }
        }

        public ActionResult Register()
        {
            var authOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = ((int)AuthType.Admin).ToString(), Text = AuthType.Admin.ToString() },
                new SelectListItem {Value = ((int)AuthType.Customer).ToString(), Text = AuthType.Customer.ToString() }
            };

            ViewBag.Auth = new SelectList(authOptions, "Value", "Text", null);
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    var errorMessage = "";
                    foreach (var value in ModelState.Values)
                    {
                        foreach (var error in value.Errors)
                        {
                            errorMessage += error.ErrorMessage + Environment.NewLine;
                        }
                    }
                    Response.StatusCode = 400;
                    return Json(new { Message = errorMessage });
                }

                var userLogic = new UserLogic();
                userLogic.CreateNewUser(model.Username, model.Password, (AuthType)model.AuthType);
                return Content("200 OK");
            }
            catch (RequestErrorException ree)
            {
                Response.StatusCode = 400;
                return Json(new { ree.Message });
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(new { e.Message });
            }
        }
    }
}
