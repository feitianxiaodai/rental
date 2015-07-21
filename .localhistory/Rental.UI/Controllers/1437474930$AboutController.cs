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
    public class AboutController : Controller
    {
        //
        // GET: /About/

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult _AjaxSaveAboutImg()
        {
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            UploadResponeModel response = new UploadResponeModel();
            if (Directory.Exists(Server.MapPath("~/Upload/AboutImg")))
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
                    originUrl = Server.MapPath(string.Format("~/Upload/AboutImg/{0}.{1}", actualFileName, fileExtension));
                    destUrl = Server.MapPath(string.Format("~/Upload/AboutImg/thumb/{0}.{1}", actualFileName, fileExtension));

                    //先保存大图片
                    file.SaveAs(originUrl);
                    //response
                    response.files.Add(new UploadFileInfo
                    {
                        url = string.Format("{0}/Upoad/AboutImg/{1}.{2}", hostName, actualFileName, fileExtension),
                        name = string.Format("{0}.{1}", actualFileName, fileExtension),
                        type = file.ContentType,

                    });
                }
                return Json(response);
            }
            return null;
        }

    }
}
