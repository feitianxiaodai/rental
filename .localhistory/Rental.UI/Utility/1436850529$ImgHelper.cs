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
            int left, top;          
            if (originalBmp.Width <= width && originalBmp.Height <= height)
            {
                // 原图像的宽度和高度都小于生成的图片大小             
                left = (int)Math.Round((decimal)(width - originalBmp.Width) / 2);
                top = (int)Math.Round((decimal)(height - originalBmp.Height) / 2);
                // 最终生成的图像          
                Bitmap bmpOut = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(bmpOut))
                {
                    // 设置高质量插值法              
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    // 清空画布并以白色背景色填充             
                    graphics.Clear(Color.White);
                    //加上边框                 
                    //Pen pen = new Pen(ColorTranslator.FromHtml("#cccccc"));        
                    // graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);          
                    // 把源图画到新的画布上            
                    graphics.DrawImage(originalBmp, left, top);
                }
                // bmpOut.Save(tPath);  
                //保存为文件，tpath 为要保存的路径         
                this.OutputImgToPage(bmpOut);//直接输出到页面      
                bmpOut.Dispose();
                return;
            }


        }
    }
}