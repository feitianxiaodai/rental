using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rental.Service;
using Rental.UI.Utility;
using Rental.UI.Models;
namespace Rental.UI.Controllers
{

    public class SliderController : Controller
    {
        SliderService sliderService = new SliderService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _AjaxGetAllSlider()
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            int pageCount = 0;
            int pageIndex = Convert.ToInt16(Request.Form["pageIndex"]);
            var sliderList = sliderService.GetSliderPageList(pageIndex, pageSize, out pageCount);
            return Json(new GridData<object>(sliderList, pageCount));
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Save()
        {
            var file = Request.Files[0];
            file.SaveAs(Server.MapPath("~/Content/1.png"));
            return View();
        }

    }
}
