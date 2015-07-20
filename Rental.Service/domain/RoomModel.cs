using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service.domain
{
    public class RoomModel
    {
        public RoomModel()
        {
            RoomImageInfo = new List<RoomImageInfoModel>();
        }
        public int Id { get; set; }
        public int RoomType { get; set; }

        public string RoomTypeName { get; set; }
        public int IsBasement { get; set; }

        public string RoomBaseName { get; set; }
        public double RoomSpace { get; set; }
        public int RoomShi { get; set; }
        public Nullable<int> RoomTing { get; set; }
        public Nullable<int> RoomKitchen { get; set; }
        public Nullable<int> RoomBalcony { get; set; }
        public Nullable<int> PrivateBathroom { get; set; }
        public Nullable<int> PublicBathroom { get; set; }

        [Required(ErrorMessage = "请填写房屋面积")]
        public int RoomCount { get; set; }
        public int BedType1 { get; set; }
        public Nullable<int> BedType2 { get; set; }
        public Nullable<int> BedType3 { get; set; }
        public Nullable<int> BedType4 { get; set; }
        public Nullable<bool> Fridge { get; set; }
        public Nullable<bool> WashingMechine { get; set; }
        public Nullable<bool> Water { get; set; }
        public Nullable<bool> Intercom { get; set; }
        public Nullable<bool> Smoke { get; set; }
        public Nullable<bool> Computer { get; set; }
        public Nullable<bool> Shower { get; set; }
        public Nullable<bool> Bathtub { get; set; }
        public Nullable<bool> Toiletries { get; set; }
        public Nullable<bool> Towel { get; set; }
        public Nullable<bool> Slippers { get; set; }
        public Nullable<bool> HotWater { get; set; }
        public Nullable<bool> Elevator { get; set; }
        public Nullable<bool> Police { get; set; }
        public Nullable<bool> Child { get; set; }
        public Nullable<bool> Older { get; set; }
        public Nullable<bool> AirportTransfer { get; set; }
        public Nullable<bool> LeftLuggage { get; set; }
        public Nullable<bool> Guide { get; set; }

        [Required(ErrorMessage = "请填写同类房源数量")]
        public int RoomNumber { get; set; }
        public int RoomCity { get; set; }
        public int RoomArea { get; set; }
        public string RoomDetailedAddress { get; set; }
        public string RoomLang { get; set; }
        public string RoomLong { get; set; }
        public string RoomName { get; set; }
        public string RoomNameTW { get; set; }
        public string RoomNameEN { get; set; }
        public List<RoomImageInfoModel> RoomImageInfo { get; set; }
    }

    public class RoomImageInfoModel
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }

        public string OriginImg { get; set; }

        //for front show
        public string ImgUrlThumb { get; set; }
    }
}
