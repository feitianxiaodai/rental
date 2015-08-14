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
        RoomService roomSer = new RoomService();
        ServiceService serviceSer = new ServiceService();
        FoodService foodSer = new FoodService();
        PreferenceService prefenceSer = new PreferenceService();
        AboutService aboutSer = new AboutService();
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

        public ActionResult Room()
        {
            return View();
        }

        public ActionResult RoomPhone()
        {
            return View();
        }

        public ActionResult RoomList(int id)
        {
            //根据地区选择该地区所有的房间信息
            var roomViewModels = roomSer.GetRoomInfoList(id);
            return View(roomViewModels);
        }

        /// <summary>
        /// 得到房间的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RoomPreview(int id)
        {
            var room = roomSer.GetPreviewRoomInfo(id);
            return View(room);
        }

        public ActionResult Service()
        {
            var viewModel = serviceSer.GetServiceViewModel(cultureName);
            return View(viewModel);
        }

        public ActionResult ServicePhone()
        {
            var viewModel = serviceSer.GetServiceViewModel(cultureName);
            return View(viewModel);
        }


        public ActionResult ServiceInfo(int id)
        {
            var viewModel = serviceSer.GetServiceViewModelById(id, cultureName);
            return View(viewModel);
        }

        public ActionResult Food()
        {
            var viewModel = foodSer.GetServiceViewModel(cultureName);
            return View(viewModel);
        }

        public ActionResult FoodPhone()
        {
            var viewModel = foodSer.GetServiceViewModel(cultureName);
            return View(viewModel);
        }

        public ActionResult FoodInfo(int id)
        {
            var viewModel = foodSer.GetServiceViewModelById(id, cultureName);
            return View(viewModel);
        }

        public ActionResult Preference()
        {
            var viewModel = prefenceSer.GetServiceViewModel(cultureName);
            return View(viewModel);
        }

        public ActionResult PreferencePhone()
        {
            var viewModel = prefenceSer.GetServiceViewModel(cultureName);
            return View(viewModel);
        }

        public ActionResult PreferenceInfo(int id)
        {
            var viewModel = prefenceSer.GetServiceViewModelById(id, cultureName);
            return View(viewModel);
        }

        public ActionResult About()
        {
            var viewModel = aboutSer.GetAboutViewModel(cultureName);
            return View(viewModel);
        }

        public ActionResult AboutPhone()
        {
            var viewModel = aboutSer.GetAboutViewModel(cultureName);
            return View(viewModel);
        }

    }
}
