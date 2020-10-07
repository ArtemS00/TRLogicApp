using System.ComponentModel.DataAnnotations;
using TRLogicApp.Extensions;

namespace TRLogicApp.Models
{
    public class ImageModel
    {
        /// <summary>
        /// Image in base64 format. Max size ~ 1mb. Must be null if other data is setted.
        /// </summary>
        [OneOfTwo(nameof(Url))]
        [MaxLength(1400000)]
        public string Base64Data { get; set; }

        /// <summary>
        /// Image url. Must be null if other data is setted.
        /// </summary>
        [Url]
        [OneOfTwo(nameof(Base64Data))]
        [MaxLength(5000)]
        public string Url { get; set; }
    }
}
