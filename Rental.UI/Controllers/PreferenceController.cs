using Rental.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rental.UI.Controllers
{
    public class PreferenceController : Controller
    {
        Rental.Service.PreferenceService preferneceSer = new Service.PreferenceService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _AjaxFoodList()
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            int pageCount = 0;
            int pageIndex = Convert.ToInt16(Request.Form["pageIndex"]);
            var sliderList = preferneceSer.GetServicePageList(pageIndex, pageSize, out pageCount);
            return Json(new GridData<object>(sliderList, pageCount));
        }

        public ActionResult _AjaxSaveFoodImg()
        {
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            UploadResponeModel response = new UploadResponeModel();
            if (Directory.Exists(Server.MapPath("~/Upload/PreferenceImg")))
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
                    originUrl = Server.MapPath(string.Format("~/Upload/PreferenceImg/{0}.{1}", actualFileName, fileExtension));
                    destUrl = Server.MapPath(string.Format("~/Upload/PreferenceImg/thumb/{0}.{1}", actualFileName, fileExtension));

                    //先保存大图片
                    file.SaveAs(originUrl);
                    Utility.ImgHelper.GenerateThumbImg(originUrl, 54, 44, destUrl);

                    //response
                    response.files.Add(new UploadFileInfo
                    {
                        url = string.Format("{0}/Upoad/PreferenceImg/{1}.{2}", hostName, actualFileName, fileExtension),
                        name = string.Format("{0}.{1}", actualFileName, fileExtension),
                        type = file.ContentType,

                    });
                }
                return Json(response);
            }
            return null;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Rental.Service.domain.PreferenceModel model, string[] ImgUrl)
        {
            if (ImgUrl != null && ImgUrl.Count() > 0)
            {
                foreach (var item in ImgUrl)
                {
                    model.PreferenImageInfo.Add(new Rental.Service.domain.PreferenceImageInfo
                    {
                        ImgUrl = item
                    });
                }
            }
            preferneceSer.Save(model);

            return RedirectToAction("Index");
        }

        public ActionResult _AjaxDeleteFood(int[] ids)
        {
            AjaxResponseModel response = new AjaxResponseModel() { Status = false };
            bool result = preferneceSer.Delete(ids);
            if (result)
            {
                response.Status = true;
                response.Type = "Success";
            }
            return Json(response);
        }

        public ActionResult Edit(int id)
        {
            var viewModel = preferneceSer.GetServiceById(id);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Rental.Service.domain.PreferenceModel model, string[] ImgUrl)
        {
            if (ImgUrl != null && ImgUrl.Count() > 0)
            {
                foreach (var item in ImgUrl)
                {
                    model.PreferenImageInfo.Add(new Service.domain.PreferenceImageInfo { ImgUrl = item });
                }
            }
            preferneceSer.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult test()
        {
            return View();
        }

    }
}
