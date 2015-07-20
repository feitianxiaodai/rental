using Rental.Model.Model;
using Rental.Repository;
using Rental.Service.convertor;
using Rental.Service.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service
{
    public class RoomService
    {
        private UnitOfWork _unitOfWork = null;
        public RoomService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<RoomModel> GetRoomPageList(int pageIndex, int pageSize, out int totalCount)
        {
            int count = 0;
            var roomList = _unitOfWork.RoomRepository.Query().OrderBy(s => s.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var roomDomainList = RoomModelConvertor.ConvertToDomainModel(roomList);
            count = _unitOfWork.RoomRepository.Query().Where(s => true).Count();
            totalCount = count;
            return roomDomainList;
        }

        public bool SaveRoomInfo(RoomModel roomModel)
        {
            var room = RoomModelConvertor.ConvertToEFModel(roomModel);
            if (room != null)
            {
                _unitOfWork.RoomRepository.Insert(room);
                return _unitOfWork.Commit() > 0;
            }
            return false;
        }

        public List<RoomModel> GetRoomInfoList(int city)
        {
            var rooms = _unitOfWork.RoomRepository.Query().Where(s => s.RoomCity == city).ToList();
            List<RoomModel> roomViewModels = RoomModelConvertor.ConvertToDomainModel(rooms);
            return roomViewModels;
        }

        /// <summary>
        /// 根据id获得房间的详细信息
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public RoomModel GetPreviewRoomInfo(int roomId)
        {
            var room = _unitOfWork.RoomRepository.Query().FirstOrDefault(s => s.Id == roomId);
            RoomModel roomPreview = new RoomModel();
            if (room != null)
            {
                roomPreview = RoomModelConvertor.ConvertToViewModel(room);
            }
            return roomPreview;
        }

        public bool Delete(int[] ids)
        {
            try
            {
                int id = ids.First();
                var model = _unitOfWork.RoomRepository.Query().FirstOrDefault(s => s.Id == id);
                _unitOfWork.RoomRepository.Delete(model);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateRoomInfo(RoomModel room)
        {
            int id = room.Id;
            if (id != 0)
            {
                var model = _unitOfWork.RoomRepository.Query().FirstOrDefault(s => s.Id == id);

                //model.RoomImageInfo.Clear();
                //model.RoomImageInfo = room.RoomImageInfo.Select(b => new RoomImageInfo
                //{
                //    ImgUrl = b.ImgUrl,
                //}).ToList();
                foreach(var item in room.RoomImageInfo)
                {
                    model.RoomImageInfo.Add(new RoomImageInfo
                    {
                        ImgUrl = item.ImgUrl,
                        Id= room.Id,
                    });
                }
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
                return _unitOfWork.Commit() > 0;
            }
            return false;
        }
    }
}
