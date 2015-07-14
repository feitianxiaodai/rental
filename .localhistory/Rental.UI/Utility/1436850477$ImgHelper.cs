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
            if (originalBmp.Width <= width && originalBmp.Height <= height)           
40.        {           
41.            // 原图像的宽度和高度都小于生成的图片大小             
42.            left = (int)Math.Round((decimal)(width - originalBmp.Width) / 2);          
43.            top = (int)Math.Round((decimal)(height - originalBmp.Height) / 2);  
44.            // 最终生成的图像          
45.            Bitmap bmpOut = new Bitmap(width, height);           
46.            using (Graphics graphics = Graphics.FromImage(bmpOut))              
47.            {                  
48.                // 设置高质量插值法              
49.                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;        
50.                // 清空画布并以白色背景色填充             
51.                graphics.Clear(Color.White);                  
52.                //加上边框                 
53.                //Pen pen = new Pen(ColorTranslator.FromHtml("#cccccc"));        
54.                // graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);          
55.                // 把源图画到新的画布上            
56.                graphics.DrawImage(originalBmp, left, top);            
57.            }            
58.            // bmpOut.Save(tPath);  
59.            //保存为文件，tpath 为要保存的路径         
60.            this.OutputImgToPage(bmpOut);//直接输出到页面      
61.            bmpOut.Dispose();               
62.            return;           
63.        }            


        }
    }
}