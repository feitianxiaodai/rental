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
    
    public partial class PreferenImageInfo
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public int PreferenId { get; set; }
    
        public virtual Preferential Preferential { get; set; }
    }
}
