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

        public ActionResult _AjaxSaveFoodImg()
        {
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            UploadResponeModel response = new UploadResponeModel();
            if (Directory.Exists(Server.MapPath("~/Upload/FoodImg")))
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
                    originUrl = Server.MapPath(string.Format("~/Upload/FoodImg/{0}.{1}", actualFileName, fileExtension));
                    destUrl = Server.MapPath(string.Format("~/Upload/FoodImg/thumb/{0}.{1}", actualFileName, fileExtension));

                    //先保存大图片
                    file.SaveAs(originUrl);
                    Utility.ImgHelper.GenerateThumbImg(originUrl, 54, 44, destUrl);

                    //response
                    response.files.Add(new UploadFileInfo
                    {
                        url = string.Format("{0}/Upoad/FoodImg/{1}.{2}", hostName, actualFileName, fileExtension),
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
        public ActionResult Create(Rental.Service.domain.ServiceModel model, string[] ImgUrl)
        {
            if (ImgUrl != null && ImgUrl.Count() > 0)
            {
                foreach (var item in ImgUrl)
                {
                    model.ServiceImageInfo.Add(new Rental.Service.domain.ServiceImageInfoModel
                    {
                        ImgUrl = item
                    });
                }
                model.CreateTime = DateTime.Now;
            }
            serviceSer.Save(model);

            return RedirectToAction("Index");
        }
    }
}
