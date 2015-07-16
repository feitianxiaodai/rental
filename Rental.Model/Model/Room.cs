//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rental.Model.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Room
    {
        public Room()
        {
            this.RoomImageInfo = new HashSet<RoomImageInfo>();
        }
    
        public int Id { get; set; }
        public int RoomType { get; set; }
        public int IsBasement { get; set; }
        public double RoomSpace { get; set; }
        public int RoomShi { get; set; }
        public Nullable<int> RoomTing { get; set; }
        public Nullable<int> RoomKitchen { get; set; }
        public Nullable<int> RoomBalcony { get; set; }
        public Nullable<int> PrivateBathroom { get; set; }
        public Nullable<int> PublicBathroom { get; set; }
        public int RoomCount { get; set; }
        public int BedType1 { get; set; }
        public Nullable<int> BedType2 { get; set; }
        public Nullable<int> BedType3 { get; set; }
        public Nullable<int> BedType4 { get; set; }
        public Nullable<bool> Fridge { get; set; }
        public Nullable<bool> WashingMechine { get; set; }
        public Nullable<bool> Water { get; set; }
        public Nullable<bool> Intercom { get; set; }
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
        public int RoomNumber { get; set; }
        public int RoomCity { get; set; }
        public int RoomArea { get; set; }
        public string RoomDetailedAddress { get; set; }
        public string RoomLang { get; set; }
        public string RoomLong { get; set; }
    
        public virtual ICollection<RoomImageInfo> RoomImageInfo { get; set; }
    }
}
