using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rental.Service;
using System.Configuration;
using Rental.UI.Models;
using System.IO;
namespace Rental.UI.Controllers
{
    public class ServiceController : Controller
    {
        ServiceService serviceSer = new ServiceService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _AjaxServiceList()
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            int pageCount = 0;
            int pageIndex = Convert.ToInt16(Request.Form["pageIndex"]);
            var sliderList = serviceSer.GetServicePageList(pageIndex, pageSize, out pageCount);
            return Json(new GridData<object>(sliderList, pageCount));
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

        public ActionResult _AjaxSaveServiceImg()
        {
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            UploadResponeModel response = new UploadResponeModel();
            if (Directory.Exists(Server.MapPath("~/Upload/ServiceImg")))
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
                    originUrl = Server.MapPath(string.Format("~/Upload/ServiceImg/{0}.{1}", actualFileName, fileExtension));
                    destUrl = Server.MapPath(string.Format("~/Upload/ServiceImg/thumb/{0}.{1}", actualFileName, fileExtension));

                    //先保存大图片
                    file.SaveAs(originUrl);
                    Utility.ImgHelper.GenerateThumbImg(originUrl, 54, 44, destUrl);

                    //response
                    response.files.Add(new UploadFileInfo
                    {
                        url = string.Format("{0}/Upoad/ServiceImg/{1}.{2}", hostName, actualFileName, fileExtension),
                        name = string.Format("{0}.{1}", actualFileName, fileExtension),
                        type = file.ContentType,

                    });
                }
                return Json(response);
            }
            return null;
        }

        public ActionResult _AjaxDeleteService(int[] ids)
        {
            AjaxResponseModel response = new AjaxResponseModel() { Status = false };
            bool result = serviceSer.Delete(ids);
            if (result)
            {
                response.Status = true;
                response.Type = "Success";
            }
            return Json(response);
        }

        public ActionResult Edit(int id)
        {
            var viewModel = serviceSer.GetServiceById(id);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Rental.Service.domain.ServiceModel model)
        {
            serviceSer.Update(model);
            return RedirectToAction("Index");
        }


    }
}
