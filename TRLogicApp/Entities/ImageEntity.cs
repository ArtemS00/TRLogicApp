using System;
using TRLogicApp.Services;

namespace TRLogicApp.Entities
{
    public class ImageEntity : IEntity
    {
        public ImageEntity(byte[] data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));

            if (data.Length > 1024 * 1024)
                throw new ArgumentException(nameof(data), "The image is too large! The max size is 1 MB.");

            Preview = ImageConverter.FromImage(
                ImageWorker.GetSquarePreview(ImageConverter.ToImage(data), 100));
        }

        public int Id { get; set; }
        public byte[] Data { get; set; }
        public byte[] Preview { get; set; }
    }
}
