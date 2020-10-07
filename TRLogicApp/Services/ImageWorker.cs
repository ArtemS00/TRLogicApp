using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TRLogicApp.Services
{
    public static class ImageWorker
    {
        /// <summary>
        /// Get square preview with desired size
        /// </summary>
        /// <param name="image">Image to create preview for</param>
        /// <param name="size">Square side size</param>
        public static Image GetSquarePreview(Image image, int size)
        {
            if (image == null || size < 0)
                throw new ArgumentException();

            if (image.Width == image.Height)
                return ResizeImage(image, size, size);

            // Crop image if it's not square
            if (image.Width > image.Height)
            {
                var cropedImage = CropImage(image, new Rectangle((image.Width - image.Height) / 2, 0, image.Height, image.Height));
                return ResizeImage(cropedImage, size, size);
            }
            else
            {
                var cropedImage = CropImage(image, new Rectangle(0, (image.Height - image.Width) / 2, image.Width, image.Width));
                return ResizeImage(cropedImage, size, size);
            }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Image ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// <summary>
        /// Crop the image
        /// </summary>
        public static Image CropImage(Image source, Rectangle section)
        {
            Bitmap target = new Bitmap(section.Width, section.Height);
            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(source, new Rectangle(0, 0, target.Width, target.Height), section, GraphicsUnit.Pixel);
                return target;
            }
        }
    }
}
