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
        public static void GenerateThumbImg(string originImg, int width, int height, string desUrl)
        {
            string fileAbsoluteUrl = HttpContext.Current.Server.MapPath("~/Upload/SliderImg" + originImg);
            string newFileName = string.Empty;
            if (!File.Exists(fileAbsoluteUrl))
            {
                return;
            }
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
                //保存为文件，tpath 为要保存的路径         
                newFileName = HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}{2}", desUrl, DateTime.Now.Ticks, Path.GetExtension(fileAbsoluteUrl)));
                originImage.Save(newFileName);
                bmpOut.Dispose();
                return;
            }

            int newWidth, newHeight;        
67.        if (width * originalBmp.Height < height * originalBmp.Width)       
68.        {          
69.            newWidth = width;     
70.            newHeight = (int)Math.Round((decimal)originalBmp.Height * width / originalBmp.Width);     
71.            // 缩放成宽度跟预定义的宽度相同的，即left=0，计算top          
72.            left = 0;       
73.            top = (int)Math.Round((decimal)(height - newHeight) / 2);   
74.        }        
75.        else       
76.        {            
77.            newWidth = (int)Math.Round((decimal)originalBmp.Width * height / originalBmp.Height);     
78.            newHeight = height;     
79.            // 缩放成高度跟预定义的高度相同的，即top=0，计算left     
80.            left = (int)Math.Round((decimal)(width - newWidth) / 2);      
81.            top = 0;          
82.        }         
83.        // 生成按比例缩放的图，如：160*80的图         
84.        Bitmap bmpOut2 = new Bitmap(newWidth, newHeight);        
85.        using (Graphics graphics = Graphics.FromImage(bmpOut2))          
86.        {              
87.            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;         
88.            graphics.FillRectangle(Brushes.White, 0, 0, newWidth, newHeight);       
89.            graphics.DrawImage(originalBmp, 0, 0, newWidth, newHeight);        
90.        }           
91.        // 再把该图画到预先定义的宽高的画布上，如160*120          
92.        Bitmap lastbmp = new Bitmap(width, height);         
93.        using (Graphics graphics = Graphics.FromImage(lastbmp))      
94.        {               
95.            // 设置高质量插值法          
96.            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;    
97.            // 清空画布并以白色背景色填充      
98.            graphics.Clear(Color.White);             
99.            //加上边框              
100.            //Pen pen = new Pen(ColorTranslator.FromHtml("#cccccc"));          
101.            //graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);        
102.            // 把源图画到新的画布上            
103.            graphics.DrawImage(bmpOut2, left, top);      
104.        }           
105.        // lastbmp.Save(tPath);//保存为文件，tpath 为要保存的路径      
106.        this.OutputImgToPage(lastbmp);  
107.        //直接输出到页面          
108.        lastbmp.Dispose();  



        }
    }
}