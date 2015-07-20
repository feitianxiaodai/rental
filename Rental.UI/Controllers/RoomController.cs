using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rental.Service;
using Rental.Service.domain;
using System.Configuration;
using Rental.UI.Models;
using System.IO;
namespace Rental.UI.Controllers
{
    public class RoomController : Controller
    {
        public AreaService areaSer = new AreaService();
        public RoomService roomSer = new RoomService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _AjaxRoomList()
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            int pageCount = 0;
            int pageIndex = Convert.ToInt16(Request.Form["pageIndex"]);
            var roomList = roomSer.GetRoomPageList(pageIndex, pageSize, out pageCount);
            return Json(new GridData<object>(roomList, pageCount));
        }

        public ActionResult CreateStep1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStep1(RoomModel roomModel)
        {
            Session["rental.room"] = roomModel;
            return RedirectToAction("CreateStep2");
        }

        public ActionResult CreateStep2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStep2(string[] roomImg)
        {
            if (Session["rental.room"] != null)
            {
                var room = (RoomModel)Session["rental.room"];
                if (roomImg != null && roomImg.Count() > 0)
                {
                    foreach (var img in roomImg)
                    {
                        room.RoomImageInfo.Add(new RoomImageInfoModel
                        {
                            ImgUrl = img
                        });
                    }
                    bool result = roomSer.SaveRoomInfo(room);
                    if (result)
                    {
                        return RedirectToAction("CreateStep3");
                    }
                }
            }
            return View();
        }

        public ActionResult CreateStep3()
        {
            return View();
        }

        public ActionResult _AjaxGetArea(int Id = 0)
        {
            var areaList = areaSer.GetArea(Id);
            return Json(areaList);
        }

        /// <summary>
        /// 生成图片和缩略图
        /// </summary>
        /// <returns></returns>
        public ActionResult _AjaxSaveRoomImg()
        {
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            UploadResponeModel response = new UploadResponeModel();
            if (Directory.Exists(Server.MapPath("~/Upload/RoomImg")))
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
                    originUrl = Server.MapPath(string.Format("~/Upload/RoomImg/{0}.{1}", actualFileName, fileExtension));
                    destUrl = Server.MapPath(string.Format("~/Upload/RoomImg/thumb/{0}.{1}", actualFileName, fileExtension));

                    //先保存大图片
                    file.SaveAs(originUrl);
                    Utility.ImgHelper.GenerateThumbImg(originUrl, 54, 44, destUrl);

                    //response
                    response.files.Add(new UploadFileInfo
                    {
                        url = string.Format("{0}/Upoad/RoomImg/{1}.{2}", hostName, actualFileName, fileExtension),
                        name = string.Format("{0}.{1}", actualFileName, fileExtension),
                        type = file.ContentType,

                    });
                }
                return Json(response);
            }
            return null;
        }

        /// <summary>
        /// 删除房间
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult _AjaxDeleteRoom(int[] ids)
        {
            AjaxResponseModel response = new AjaxResponseModel() { Status = false };
            bool result = roomSer.Delete(ids);
            if (result)
            {
                response.Status = true;
                response.Type = "Success";
            }
            return Json(response);
        }

        public ActionResult EditStep1(int id)
        {
            List<SelectListItem> roomType = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="1", Text="民居"},
                new SelectListItem(){ Value="2", Text="公寓",Selected=true},
                new SelectListItem(){ Value="3", Text="独栋别墅"},
                new SelectListItem(){ Value="4", Text="客栈"},
                new SelectListItem(){ Value="5", Text="阁楼"},
                new SelectListItem(){ Value="6", Text="四合院"},
                new SelectListItem(){ Value="7", Text="海边小屋"},
                new SelectListItem(){ Value="8", Text="林间小屋"},
                new SelectListItem(){ Value="9", Text="豪宅"},
                new SelectListItem(){ Value="10", Text="城堡"},
                new SelectListItem(){ Value="11", Text="树屋"},
                new SelectListItem(){ Value="12", Text="船舱"},
                new SelectListItem(){ Value="13", Text="房车"},
                new SelectListItem(){ Value="14", Text="冰屋"},
            };
            List<SelectListItem> basicInfo1 = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="1",Text="1"},
                new SelectListItem(){ Value="2",Text="2"},
                new SelectListItem(){ Value="3",Text="3"},
                new SelectListItem(){ Value="4",Text="4"},
                new SelectListItem(){ Value="5",Text="5"},
                new SelectListItem(){ Value="6",Text="6"},
                new SelectListItem(){ Value="7",Text="7"},
                new SelectListItem(){ Value="8",Text="8"},
                new SelectListItem(){ Value="9",Text="9"},
                new SelectListItem(){ Value="10",Text="9+"},
            };
            List<SelectListItem> basicInfo2 = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="1",Text="1"},
                new SelectListItem(){ Value="2",Text="2"},
                new SelectListItem(){ Value="3",Text="3"},
                new SelectListItem(){ Value="4",Text="4"},
                new SelectListItem(){ Value="5",Text="5"},
                new SelectListItem(){ Value="6",Text="6"},
                new SelectListItem(){ Value="7",Text="7"},
                new SelectListItem(){ Value="8",Text="8"},
                new SelectListItem(){ Value="9",Text="9"},
                new SelectListItem(){ Value="10",Text="9+"},
            };
            List<SelectListItem> basicInfo3 = new List<SelectListItem>()
            {
                 new SelectListItem(){ Value="0",Text="0"},
                new SelectListItem(){ Value="1",Text="1"},
                new SelectListItem(){ Value="2",Text="2"},
                new SelectListItem(){ Value="3",Text="3"},
                new SelectListItem(){ Value="4",Text="4"},
                new SelectListItem(){ Value="5",Text="5"},
                new SelectListItem(){ Value="6",Text="6"},
                new SelectListItem(){ Value="7",Text="7"},
                new SelectListItem(){ Value="8",Text="8"},
                new SelectListItem(){ Value="9",Text="9"},
                new SelectListItem(){ Value="10",Text="9+"},
            };
            List<SelectListItem> basicInfo4 = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="0",Text="0"},
                new SelectListItem(){ Value="1",Text="1"},
                new SelectListItem(){ Value="2",Text="2"},
                new SelectListItem(){ Value="3",Text="3"},
                new SelectListItem(){ Value="4",Text="4"},
                new SelectListItem(){ Value="5",Text="5"},
                new SelectListItem(){ Value="6",Text="6"},
                new SelectListItem(){ Value="7",Text="7"},
                new SelectListItem(){ Value="8",Text="8"},
                new SelectListItem(){ Value="9",Text="9"},
                new SelectListItem(){ Value="10",Text="9+"},
            };
            List<SelectListItem> basicInfo5 = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="0",Text="0"},
                new SelectListItem(){ Value="1",Text="1"},
                new SelectListItem(){ Value="2",Text="2"},
                new SelectListItem(){ Value="3",Text="3"},
                new SelectListItem(){ Value="4",Text="4"},
                new SelectListItem(){ Value="5",Text="5"},
                new SelectListItem(){ Value="6",Text="6"},
                new SelectListItem(){ Value="7",Text="7"},
                new SelectListItem(){ Value="8",Text="8"},
                new SelectListItem(){ Value="9",Text="9"},
                new SelectListItem(){ Value="10",Text="9+"},
            };
            List<SelectListItem> basicInfo6 = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="0",Text="0"},
                new SelectListItem(){ Value="1",Text="1"},
                new SelectListItem(){ Value="2",Text="2"},
                new SelectListItem(){ Value="3",Text="3"},
                new SelectListItem(){ Value="4",Text="4"},
                new SelectListItem(){ Value="5",Text="5"},
                new SelectListItem(){ Value="6",Text="6"},
                new SelectListItem(){ Value="7",Text="7"},
                new SelectListItem(){ Value="8",Text="8"},
                new SelectListItem(){ Value="9",Text="9"},
                new SelectListItem(){ Value="10",Text="9+"},
            };
            List<SelectListItem> basicInfo7 = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="1",Text="1"},
                new SelectListItem(){ Value="2",Text="2"},
                new SelectListItem(){ Value="3",Text="3"},
                new SelectListItem(){ Value="4",Text="4"},
                new SelectListItem(){ Value="5",Text="5"},
                new SelectListItem(){ Value="6",Text="6"},
                new SelectListItem(){ Value="7",Text="7"},
                new SelectListItem(){ Value="8",Text="8"},
                new SelectListItem(){ Value="9",Text="9"},
                new SelectListItem(){ Value="10",Text="9+"},
            };
            List<SelectListItem> basicInfo8 = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="0",Text="0"},
                new SelectListItem(){ Value="1",Text="1"},
                new SelectListItem(){ Value="2",Text="2"},
                new SelectListItem(){ Value="3",Text="3"},
                new SelectListItem(){ Value="4",Text="4"},
                new SelectListItem(){ Value="5",Text="5"},
                new SelectListItem(){ Value="6",Text="6"},
                new SelectListItem(){ Value="7",Text="7"},
                new SelectListItem(){ Value="8",Text="8"},
                new SelectListItem(){ Value="9",Text="9"},
                new SelectListItem(){ Value="10",Text="9+"},
            };
            List<SelectListItem> basicInfo9 = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="0",Text="0"},
                new SelectListItem(){ Value="1",Text="1"},
                new SelectListItem(){ Value="2",Text="2"},
                new SelectListItem(){ Value="3",Text="3"},
                new SelectListItem(){ Value="4",Text="4"},
                new SelectListItem(){ Value="5",Text="5"},
                new SelectListItem(){ Value="6",Text="6"},
                new SelectListItem(){ Value="7",Text="7"},
                new SelectListItem(){ Value="8",Text="8"},
                new SelectListItem(){ Value="9",Text="9"},
                new SelectListItem(){ Value="10",Text="9+"},

            };
            List<SelectListItem> basicInfo10 = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="0",Text="0"},
                new SelectListItem(){ Value="1",Text="1"},
                new SelectListItem(){ Value="2",Text="2"},
                new SelectListItem(){ Value="3",Text="3"},
                new SelectListItem(){ Value="4",Text="4"},
                new SelectListItem(){ Value="5",Text="5"},
                new SelectListItem(){ Value="6",Text="6"},
                new SelectListItem(){ Value="7",Text="7"},
                new SelectListItem(){ Value="8",Text="8"},
                new SelectListItem(){ Value="9",Text="9"},
                new SelectListItem(){ Value="10",Text="9+"},

            };
            List<SelectListItem> basicInfo11 = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="0",Text="0"},
                new SelectListItem(){ Value="1",Text="1"},
                new SelectListItem(){ Value="2",Text="2"},
                new SelectListItem(){ Value="3",Text="3"},
                new SelectListItem(){ Value="4",Text="4"},
                new SelectListItem(){ Value="5",Text="5"},
                new SelectListItem(){ Value="6",Text="6"},
                new SelectListItem(){ Value="7",Text="7"},
                new SelectListItem(){ Value="8",Text="8"},
                new SelectListItem(){ Value="9",Text="9"},
                new SelectListItem(){ Value="10",Text="9+"},

            };


            var room = roomSer.GetPreviewRoomInfo(id);
            roomType.FirstOrDefault(s => s.Value == room.RoomType.ToString()).Selected = true;
            basicInfo1.FirstOrDefault(s => s.Value == room.RoomShi.ToString()).Selected = true;
            basicInfo2.FirstOrDefault(s => s.Value == room.RoomTing.ToString()).Selected = true;
            basicInfo3.FirstOrDefault(s => s.Value == room.RoomKitchen.ToString()).Selected = true;
            basicInfo4.FirstOrDefault(s => s.Value == room.RoomBalcony.ToString()).Selected = true;
            basicInfo5.FirstOrDefault(s => s.Value == room.PublicBathroom.ToString()).Selected = true;
            basicInfo6.FirstOrDefault(s => s.Value == room.PrivateBathroom.ToString()).Selected = true;
            basicInfo7.FirstOrDefault(s => s.Value == room.RoomCount.ToString()).Selected = true;
            basicInfo8.FirstOrDefault(s => s.Value == room.BedType1.ToString()).Selected = true;
            basicInfo9.FirstOrDefault(s => s.Value == room.BedType2.ToString()).Selected = true;
            basicInfo10.FirstOrDefault(s => s.Value == room.BedType3.ToString()).Selected = true;
            basicInfo11.FirstOrDefault(s => s.Value == room.BedType4.ToString()).Selected = true;

            ViewBag.roomTypeSel = roomType;
            ViewBag.ShiSel = basicInfo1;
            ViewBag.TingSel = basicInfo2;
            ViewBag.kitchen = basicInfo3;
            ViewBag.balcony = basicInfo4;
            ViewBag.publicB = basicInfo5;
            ViewBag.privateB = basicInfo6;
            ViewBag.roomCountSel = basicInfo7;
            ViewBag.bed1 = basicInfo8;
            ViewBag.bed2 = basicInfo9;
            ViewBag.bed3 = basicInfo10;
            ViewBag.bed4 = basicInfo11;

            var cityList = areaSer.GetArea(room.RoomCity);
            var areaList = areaSer.GetArea(room.RoomArea);

            List<SelectListItem> citySel = new List<SelectListItem>();
            List<SelectListItem> areaSel = new List<SelectListItem>();

            if (cityList != null && cityList.Count > 0)
            {
                foreach (var item in cityList)
                {
                    var city = new SelectListItem() { Text = item.AreaName, Value = item.Id.ToString() };
                    if (item.Id == room.RoomCity)
                    {
                        city.Selected = true;
                    }
                    citySel.Add(city);
                }
            }

            if (areaList != null && areaList.Count > 0)
            {
                foreach (var item in areaList)
                {
                    var area = new SelectListItem() { Text = item.AreaName, Value = item.Id.ToString() };
                    if (item.Id == room.RoomArea)
                    {
                        area.Selected = true;
                    }
                    areaSel.Add(area);
                }
            }
            ViewBag.roomCitySel = citySel;
            ViewBag.roomAreaSel = areaSel;
            return View(room);
        }

        [HttpPost]
        public ActionResult EditStep1(RoomModel model)
        {
            Session["rental.room"] = model;
            return RedirectToAction("EditStep2");
        }

        public ActionResult EditStep2()
        {
            if (Session["rental.room"] != null)
            {
                var room = (RoomModel)Session["rental.room"];
                int id = room.Id;
                var viewRoom = roomSer.GetPreviewRoomInfo(id);
                return View(viewRoom);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditStep2(string[] roomImg)
        {
            if (Session["rental.room"] != null)
            {
                var room = (RoomModel)Session["rental.room"];
                if (roomImg != null && roomImg.Count() > 0)
                {
                    foreach (var img in roomImg)
                    {
                        room.RoomImageInfo.Add(new RoomImageInfoModel
                        {
                            ImgUrl = img
                        });
                    }
                    bool result = roomSer.UpdateRoomInfo(room);
                    if (result)
                    {
                        return RedirectToAction("EditStep3");
                    }
                }
            }
            return View();
        }

        public ActionResult EditStep3()
        {
            return View();
        }


    }
}
