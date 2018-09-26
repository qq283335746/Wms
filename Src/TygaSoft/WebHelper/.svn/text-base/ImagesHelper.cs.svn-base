using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TygaSoft.WebHelper
{
    public class ImagesHelper
    {
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param> 
        /// <param name="type">生成的文件类型</param> 
        public void CreateThumbnailImage(string originalImagePath, string thumbnailPath, int width, int height, string mode, string type)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;
            type = type.ToLower();

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形） 
                    break;
                case "W"://指定宽，高按比例 
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形） 
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                case "DB"://等比缩放（不变形，如果高大按高，宽大按宽缩放）
                    if (((double)originalImage.Width / (double)towidth) < ((double)originalImage.Height / (double)toheight))
                    {
                        toheight = height;
                        towidth = originalImage.Width * height / originalImage.Height;
                    }
                    else
                    {
                        towidth = width;
                        toheight = originalImage.Height * width / originalImage.Width;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
            new Rectangle(x, y, ow, oh),
            GraphicsUnit.Pixel);

            try
            {
                //保存缩略图
                switch (type)
                {
                    case ".jpg":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".bmp":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case ".gif":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case ".png":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        throw new ArgumentException("文件类型【" + type + "】不合法，请检查");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 创建规定大小的图像(源图像只能是JPG格式)  
        /// </summary>    
        /// <param name="fromPath">源图像绝对路径</param>    
        /// <param name="toPath">生成图像绝对路径</param>    
        /// <param name="width">生成图像的宽度</param>    
        /// <param name="height">生成图像的高度</param>    
        public void CreateThumbnailImage(string fromPath, string toPath, int width, int height)
        {
            Bitmap originalBmp = new Bitmap(fromPath);
            // 源图像在新图像中的位置    
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
                    // 把源图画到新的画布上    
                    graphics.DrawImage(originalBmp, left, top, originalBmp.Width, originalBmp.Height);
                }
                bmpOut.Save(toPath);
                bmpOut.Dispose();

                return;
            }

            // 新图片的宽度和高度，如400*200的图像，想要生成160*120的图且不变形，    
            // 那么生成的图像应该是160*80，然后再把160*80的图像画到160*120的画布上    
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
                // 把源图画到新的画布上    
                graphics.DrawImage(bmpOut2, left, top);
            }
            lastbmp.Save(toPath);
            lastbmp.Dispose();
        }   
    }
}
