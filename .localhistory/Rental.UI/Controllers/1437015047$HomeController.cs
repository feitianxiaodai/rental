using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Rental.UI.Controllers
{
    public class HomeController : Controller
    {
        string cultureName = Thread.CurrentThread.CurrentCulture.Name;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _AjaxGetHotelAlbumList()
        {
            string cultureTyle = string.Empty;
            if(cultureName=="zh-CN")
            {

            }
            return null;
        }

        

    }
}
