using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rental.Service;
namespace Rental.UI.Controllers
{
    public class RoomController : Controller
    {
        public AreaService areaSer = new AreaService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateStep1()
        {
            return View();
        }

        public ActionResult CreateStep2()
        {
            return View();
        }

        public ActionResult CreateStep3()
        {
            return View();
        }

        public ActionResult _AjaxGetArea(int Id = 0)
        {
            var areaList = areaSer.GetArea(Id);
            return Json(areaList);
        }

    }
}
