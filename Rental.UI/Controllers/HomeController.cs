using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Rental.Service;
using Rental.UI.Models;
using System.Configuration;
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
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            var hotelList = sliderSer.GetHotelAlbumList();
            var result = new List<HotelAlbumModel>();
            if (hotelList != null && hotelList.Count > 0)
            {
                if (cultureName == "zh-CN")
                {
                    result = hotelList.Select(s => new HotelAlbumModel
                    {
                        ImageAlt = s.TitleCN,
                        ImageSrc = hostName + "Upload/SliderImg/" + s.ImgUrl,
                        ImageType = 0,
                        ThumbSrc = hostName + "Upload/SliderImg/thumb/" + s.ImgUrl,
                    }).ToList();
                }
                else if (cultureName == "en-US")
                {
                    result = hotelList.Select(s => new HotelAlbumModel
                    {
                        ImageAlt = s.TitleEN,
                        ImageSrc = hostName + "Upload/SliderImg/" + s.ImgUrl,
                        ImageType = 0,
                        ThumbSrc = hostName + "Upload/SliderImg/thumb/" + s.ImgUrl,
                    }).ToList();

                }
                else
                {
                    result = hotelList.Select(s => new HotelAlbumModel
                    {
                        ImageAlt = s.TitleTW,
                        ImageSrc = hostName + "Upload/SliderImg/" + s.ImgUrl,
                        ImageType = 0,
                        ThumbSrc = hostName + "Upload/SliderImg/thumb/" + s.ImgUrl,
                    }).ToList();


                }
            }

            return Json(result);
        }



    }
}
