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
    
    public partial class Food
    {
        public Food()
        {
            this.FoodImageInfo = new HashSet<FoodImageInfo>();
        }
    
        public int Id { get; set; }
        public string TitleCN { get; set; }
        public string TitleTW { get; set; }
        public string TtitleEN { get; set; }
        public string ContentCN { get; set; }
        public string ContentTW { get; set; }
        public string ContentEN { get; set; }
    
        public virtual ICollection<FoodImageInfo> FoodImageInfo { get; set; }
    }
}
