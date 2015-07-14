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

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <returns></returns>
        public ActionResult _AjaxSaveSliderImg()
        {
            if (System.IO.File.Exists(Server.MapPath("~/Upload/SliderImg")))
            {
                var uploadFiles = Request.Files;
                if (uploadFiles != null && uploadFiles.Count > 0)
                {
                    var file = uploadFiles[0];
                }
            }
            //Msg tmp = new Msg();
            ////tmp.files.Add(new MsgInfo()
            ////{
            ////    url = "http://url.to/file/or/page",
            ////    name = "thumb2.jpg",
            ////    type = "image/jpeg",
            ////    size = 46353,
            ////});
            //return Json(tmp);
            return null;
        }

        public ActionResult _AjaxDeleteSlider(int id)
        {
            AjaxResponseModel response = new AjaxResponseModel() { Status = false };
            bool result = sliderService.Delete(id);
            if(result)
            {
                response.Status = "ok";
            }
        }

    }

}
