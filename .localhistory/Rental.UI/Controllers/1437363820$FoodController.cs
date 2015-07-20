using Rental.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rental.UI.Controllers
{
    public class FoodController : Controller
    {
        Rental.Service.FoodService foodSer = new Service.FoodService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _AjaxFoodList()
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            int pageCount = 0;
            int pageIndex = Convert.ToInt16(Request.Form["pageIndex"]);
            var sliderList = foodSer.GetServicePageList(pageIndex, pageSize, out pageCount);
            return Json(new GridData<object>(sliderList, pageCount));
        }

    }
}
