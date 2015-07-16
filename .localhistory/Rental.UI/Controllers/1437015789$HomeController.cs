using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Rental.Service;
using Rental.UI.Models;
namespace Rental.UI.Controllers
{
    public class HomeController : Controller
    {
        string cultureName = Thread.CurrentThread.CurrentCulture.Name;
        SliderService sliderSer = new SliderService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _AjaxGetHotelAlbumList()
        {
            string cultureType = string.Empty;
            if(cultureName=="zh-CN")
            {
                cultureType = "TitleCN";
            }
            else if(cultureName=="en-US")
            {
                cultureType = "TitleEN";
            }
            else
            {
                cultureType = "TitleTW";
            }
            var hotelList = sliderSer.GetHotelAlbumList();
            var result = new List<HotelAlbumModel>();
            if(hotelList !=null && hotelList.Count>0)
            {
                if (cultureName == "zh-CN")
                {
                    cultureType = "TitleCN";
                }
                else if (cultureName == "en-US")
                {
                    cultureType = "TitleEN";
                }
                else
                {
                    cultureType = "TitleTW";
                }
            }

            return null;
        }

        

    }
}
