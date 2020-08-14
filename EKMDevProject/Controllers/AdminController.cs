using BusinessLogic;
using DataAccess;
using EKMDevProject.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EKMDevProject.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            var itemLogic = new ItemLogic();
            var items = itemLogic.GetAllItems().GroupBy(x => x.CommonCode);
            List<ItemTableViewModel> viewModels = new List<ItemTableViewModel>();
            foreach(var itemGroup in items)
            {
                foreach(var item in itemGroup)
                {
                    viewModels.Add(new ItemTableViewModel(item));
                }
            }

            return View(viewModels);
        }
    }
}