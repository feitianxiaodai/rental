﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rental.Service;
using Rental.UI.Utility;
using Rental.UI.Models;
using System.IO;
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
            if (Directory.Exists(Server.MapPath("~/Upload/SliderImg")))
            {
                string actualFileName = string.Empty;
                string fileExtension = string.Empty;
                string originUrl = string.Empty;
                string destUrl = string.Empty;
                var uploadFiles = Request.Files;
                if (uploadFiles != null && uploadFiles.Count > 0)
                {
                    var file = uploadFiles[0];

                    string fileName = file.FileName;
                    string[] fs = fileName.Split('.');
                    //获得后缀名
                    fileExtension = fs[fs.Length - 1];
                    actualFileName = DateTime.Now.Ticks.ToString();
                    originUrl = Server.MapPath(string.Format("~/Upload/SliderImg/{0}{1}", actualFileName,fileExtension));
                    
                    //Utility.ImgHelper.GenerateThumbImg("test.png", 54, 44, null);
                }
            }
            return null;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _AjaxDeleteSlider(int id)
        {
            AjaxResponseModel response = new AjaxResponseModel() { Status = false };
            bool result = sliderService.Delete(id);
            if (result)
            {
                response.Status = true;
            }
            return Json(response);
        }

    }

}
