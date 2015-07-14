using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
namespace Rental.UI.Utility
{
    public static class ImgHelper
    {
        public static void GenerateThumbImg(string originImg, int width, int height, string desUrl)
        {
            Bitmap originalBmp = null;
            var originImage = Image.FromFile(HttpContext.Current.Server.MapPath("~/Upload/SliderImg" + originImg));
            originalBmp = new Bitmap(originImage);         // 源图像在新图像中的位置 

        }
    }
}