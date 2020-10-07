using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;

namespace TRLogicApp.Services
{
    public static class ImageConverter
    {
        /// <summary>
        /// Converts an base64 encrypted image to a byte array
        /// </summary>
        /// <returns>Byte array if data is correct, null if it's not</returns>
        public static byte[] FromBase64(string base64image)
        {
            if (String.IsNullOrEmpty(base64image))
                return null;

            try
            {
                return Convert.FromBase64String(base64image);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a form file image to a byte array
        /// </summary>
        /// <returns>Byte array if data is correct, null if it's not</returns>
        public static byte[] FromFormFile(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return null;

            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Converts an base64 encrypted images to a byte arrays
        /// </summary>
        /// <returns>IEnumerable of byte arrays. If the data is incorrect, the array is null</returns>
        public static IEnumerable<byte[]> FromBase64(IEnumerable<string> base64images)
        {
            foreach (var image in base64images)
            {
                byte[] result = FromBase64(image);
                yield return result;
            }
            yield break;
        }

        /// <summary>
        /// Converts an form file images to a byte arrays
        /// </summary>
        /// <returns>IEnumerable of byte arrays. If the data is incorrect, the array is null</returns>
        public static IEnumerable<byte[]> FromFormFile(IEnumerable<IFormFile> base64images)
        {
            foreach (var image in base64images)
            {
                if (image == null || image.Length == 0)
                    yield return null;

                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    yield return ms.ToArray();
                }
            }
            yield break;
        }

        /// <summary>
        /// Converts an image to a byte array
        /// </summary>
        public static byte[] FromImage(Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Downloads an image from url
        /// </summary>
        /// <returns>Byte array if image downloaded successful, null if it's not</returns>
        public static byte[] FromUrl(string url)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(url);
                    return data;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a byte array to an image
        /// </summary>
        public static Image ToImage(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
