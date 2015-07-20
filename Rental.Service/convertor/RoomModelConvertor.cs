using Rental.Model.Model;
using Rental.Service.domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service.convertor
{
    public static class RoomModelConvertor
    {
        public static List<RoomModel> ConvertToDomainModel(List<Room> rooms)
        {
            List<RoomModel> roomModels = new List<RoomModel>();
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            if (rooms != null && rooms.Count > 0)
            {
                roomModels = rooms.Select(s => new RoomModel
                {
                    RoomImageInfo = s.RoomImageInfo.Select(b => new RoomImageInfoModel
                    {
                        Id = b.Id,
                        ImgUrl = hostName + "Upload/RoomImg/" + b.ImgUrl,
                    }).ToList(),
                    RoomArea = s.RoomArea,
                    Smoke = s.Smoke.GetValueOrDefault(),
                    AirportTransfer = s.AirportTransfer.GetValueOrDefault(),
                    Bathtub = s.Bathtub.GetValueOrDefault(),
                    BedType1 = s.BedType1,
                    BedType2 = s.BedType2,
                    BedType3 = s.BedType3,
                    BedType4 = s.BedType4,
                    Child = s.Child.GetValueOrDefault(),
                    Computer = s.Computer.GetValueOrDefault(),
                    Elevator = s.Elevator.GetValueOrDefault(),
                    Fridge = s.Fridge.GetValueOrDefault(),
                    Guide = s.Guide.GetValueOrDefault(),
                    HotWater = s.HotWater.GetValueOrDefault(),
                    Id = s.Id,
                    Intercom = s.Intercom.GetValueOrDefault(),
                    IsBasement = s.IsBasement,
                    RoomBaseName = GetRoomBaseType(s.IsBasement),
                    LeftLuggage = s.LeftLuggage.GetValueOrDefault(),
                    Older = s.Older.GetValueOrDefault(),
                    Police = s.Police.GetValueOrDefault(),
                    PrivateBathroom = s.PrivateBathroom.GetValueOrDefault(),
                    PublicBathroom = s.PublicBathroom.GetValueOrDefault(),
                    RoomBalcony = s.RoomBalcony.GetValueOrDefault(),
                    RoomCity = s.RoomCity,
                    RoomCount = s.RoomCount,
                    RoomDetailedAddress = s.RoomDetailedAddress,
                    RoomKitchen = s.RoomKitchen.GetValueOrDefault(),
                    RoomLang = s.RoomLang,
                    RoomLong = s.RoomLong,
                    Shower = s.Shower,
                    RoomNumber = s.RoomNumber,
                    RoomShi = s.RoomShi,
                    RoomSpace = s.RoomSpace,
                    RoomTing = s.RoomTing,
                    RoomType = s.RoomType,
                    Slippers = s.Slippers,
                    Toiletries = s.Toiletries.GetValueOrDefault(),
                    Towel = s.Towel.GetValueOrDefault(),
                    WashingMechine = s.WashingMechine.GetValueOrDefault(),
                    Water = s.Water.GetValueOrDefault(),
                    RoomName = s.RoomName,
                    RoomNameEN = s.RoomNameEN,
                    RoomNameTW = s.RoomNameTW,
                    RoomTypeName = GetRoomType(s.RoomType),
                }).ToList();
            }
            return roomModels;

        }

        private static string GetRoomType(int roomTypeId)
        {
            string roomTypeName = string.Empty;
            switch (roomTypeId)
            {
                case 1:
                    roomTypeName = "民居";
                    break;
                case 2:
                    roomTypeName = "公寓";
                    break;
                case 3:
                    roomTypeName = "独栋别墅";
                    break;
                case 4:
                    roomTypeName = "客栈";
                    break;
                case 5:
                    roomTypeName = "阁楼";
                    break;
                case 6:
                    roomTypeName = "四合院";
                    break;
                case 7:
                    roomTypeName = "海边小屋";
                    break;
                case 8:
                    roomTypeName = "林间小屋";
                    break;
                case 9:
                    roomTypeName = "豪宅";
                    break;
                case 10:
                    roomTypeName = "城堡";
                    break;
                case 11:
                    roomTypeName = "树屋";
                    break;
                case 12:
                    roomTypeName = "船舱";
                    break;

                case 13:
                    roomTypeName = "房车";
                    break;
                case 14:
                    roomTypeName = "冰屋";
                    break;
                default:
                    roomTypeName = "其他";
                    break;
            }
            return roomTypeName;
        }

        private static string GetRoomBaseType(int roomBaseTypeId)
        {
            string roomBaseTypeName = string.Empty;
            switch (roomBaseTypeId)
            {
                case 0:
                    {
                        roomBaseTypeName = "否";
                        break;
                    }
                case 1:
                    {
                        roomBaseTypeName = "地下室";
                        break;
                    }
                case 2:
                    {
                        roomBaseTypeName = "半地下室";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return roomBaseTypeName;
        }

        public static Room ConvertToEFModel(RoomModel room)
        {
            Room model = new Room();
            if (room != null)
            {

                model.RoomImageInfo = room.RoomImageInfo.Select(b => new RoomImageInfo
                {
                    ImgUrl = b.ImgUrl,
                }).ToList();
                model.RoomArea = room.RoomArea;
                model.Smoke = room.Smoke;
                model.AirportTransfer = room.AirportTransfer;
                model.Bathtub = room.Bathtub;
                model.BedType1 = room.BedType1;
                model.BedType2 = room.BedType2;
                model.BedType3 = room.BedType3;
                model.BedType4 = room.BedType4;
                model.Child = room.Child;
                model.Computer = room.Computer;
                model.Elevator = room.Elevator;
                model.Fridge = room.Fridge;
                model.Guide = room.Guide;
                model.HotWater = room.HotWater;
                model.Intercom = room.Intercom;
                model.IsBasement = room.IsBasement;
                model.LeftLuggage = room.LeftLuggage;
                model.Older = room.Older;
                model.Police = room.Police;
                model.PrivateBathroom = room.PrivateBathroom;
                model.PublicBathroom = room.PublicBathroom;
                model.RoomBalcony = room.RoomBalcony;
                model.RoomCity = room.RoomCity;
                model.RoomCount = room.RoomCount;
                model.RoomDetailedAddress = room.RoomDetailedAddress;
                model.RoomKitchen = room.RoomKitchen;
                model.RoomLang = room.RoomLang;
                model.RoomLong = room.RoomLong;
                model.Shower = room.Shower;
                model.RoomNumber = room.RoomNumber;
                model.RoomShi = room.RoomShi;
                model.RoomSpace = room.RoomSpace;
                model.RoomTing = room.RoomTing;
                model.RoomType = room.RoomType;
                model.Slippers = room.Slippers;
                model.Toiletries = room.Toiletries;
                model.Towel = room.Towel;
                model.WashingMechine = room.WashingMechine;
                model.Water = room.Water;
                model.RoomName = room.RoomName;
                model.RoomNameEN = room.RoomNameEN;
                model.RoomNameTW = room.RoomNameTW;
            }
            return model;
        }

        public static RoomModel ConvertToViewModel(Room room)
        {
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            RoomModel model = new RoomModel();
            if (room != null)
            {
                model.RoomImageInfo = room.RoomImageInfo.Select(b => new RoomImageInfoModel
               {
                   ImgUrl = hostName + "Upload/RoomImg/" + b.ImgUrl,
                   ImgUrlThumb = hostName + "Upload/RoomImg/Thumb/" + b.ImgUrl,
                   OriginImg = b.ImgUrl

               }).ToList();
                model.RoomArea = room.RoomArea;
                model.RoomTypeName = GetRoomType(room.RoomType);
                model.RoomBaseName = GetRoomBaseType(room.IsBasement);
                model.Smoke = room.Smoke.GetValueOrDefault();
                model.AirportTransfer = room.AirportTransfer;
                model.Bathtub = room.Bathtub;
                model.BedType1 = room.BedType1;
                model.BedType2 = room.BedType2;
                model.BedType3 = room.BedType3;
                model.BedType4 = room.BedType4;
                model.Child = room.Child;
                model.Computer = room.Computer;
                model.Elevator = room.Elevator;
                model.Fridge = room.Fridge;
                model.Guide = room.Guide;
                model.HotWater = room.HotWater;
                model.Intercom = room.Intercom;
                model.IsBasement = room.IsBasement;
                model.LeftLuggage = room.LeftLuggage;
                model.Older = room.Older;
                model.Police = room.Police;
                model.PrivateBathroom = room.PrivateBathroom;
                model.PublicBathroom = room.PublicBathroom;
                model.RoomBalcony = room.RoomBalcony;
                model.RoomCity = room.RoomCity;
                model.RoomCount = room.RoomCount;
                model.RoomDetailedAddress = room.RoomDetailedAddress;
                model.RoomKitchen = room.RoomKitchen;
                model.RoomLang = room.RoomLang;
                model.RoomLong = room.RoomLong;
                model.Shower = room.Shower;
                model.RoomNumber = room.RoomNumber;
                model.RoomShi = room.RoomShi;
                model.RoomSpace = room.RoomSpace;
                model.RoomTing = room.RoomTing;
                model.RoomType = room.RoomType;
                model.Slippers = room.Slippers;
                model.Toiletries = room.Toiletries;
                model.Towel = room.Towel;
                model.WashingMechine = room.WashingMechine;
                model.Water = room.Water;
                model.RoomName = room.RoomName;
                model.RoomNameEN = room.RoomNameEN;
                model.RoomNameTW = room.RoomNameTW;
            }
            return model;
        }

    }
}
