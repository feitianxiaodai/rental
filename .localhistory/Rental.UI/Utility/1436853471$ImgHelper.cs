using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Rental.UI.Utility
{
    public static class ImgHelper
    {
        public static void GenerateThumbImg(string originImg, int width, int height, string desImg)
        {
            string newFileName = string.Empty;
            if (!File.Exists(originImg))
            {
                return;
            }
            Bitmap originalBmp = null;
            var originImage = Image.FromFile(originImg);
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
                //保存为文件，tpath 为要保存的路径         
                newFileName = HttpContext.Current.Server.MapPath();
                originImage.Save(newFileName);
                bmpOut.Dispose();
                return;
            }

            int newWidth, newHeight;
            if (width * originalBmp.Height < height * originalBmp.Width)
            {
                newWidth = width;
                newHeight = (int)Math.Round((decimal)originalBmp.Height * width / originalBmp.Width);
                // 缩放成宽度跟预定义的宽度相同的，即left=0，计算top          
                left = 0;
                top = (int)Math.Round((decimal)(height - newHeight) / 2);
            }
            else
            {
                newWidth = (int)Math.Round((decimal)originalBmp.Width * height / originalBmp.Height);
                newHeight = height;
                // 缩放成高度跟预定义的高度相同的，即top=0，计算left     
                left = (int)Math.Round((decimal)(width - newWidth) / 2);
                top = 0;
            }
            // 生成按比例缩放的图，如：160*80的图         
            Bitmap bmpOut2 = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(bmpOut2))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.FillRectangle(Brushes.White, 0, 0, newWidth, newHeight);
                graphics.DrawImage(originalBmp, 0, 0, newWidth, newHeight);
            }
            // 再把该图画到预先定义的宽高的画布上，如160*120          
            Bitmap lastbmp = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(lastbmp))
            {
                // 设置高质量插值法          
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // 清空画布并以白色背景色填充      
                graphics.Clear(Color.White);
                //加上边框              
                //Pen pen = new Pen(ColorTranslator.FromHtml("#cccccc"));          
                //graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);        
                // 把源图画到新的画布上            
                graphics.DrawImage(bmpOut2, left, top);
            }
            bmpOut2.Save(HttpContext.Current.Server.MapPath("~/Upload/1.png"));
            //直接输出到页面          
            lastbmp.Dispose();



        }
    }
}