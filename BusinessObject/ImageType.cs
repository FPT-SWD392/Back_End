using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ImageType
    {
        public const string JPG = "image/jpeg";
        public const string PNG = "image/png";
        public const string GIF = "image/gif";
        public const string BMP = "image/bmp";
        public const string TIFF = "image/tiff";
        public const string WEBP = "image/webp";
        public const string SVG = "image/svg+xml";

        public static bool IsSupportedImageType(string contentType)
        {
            return contentType switch
            {
                JPG => true,
                PNG => true,
                GIF => true,
                BMP => true,
                TIFF => true,
                WEBP => true,
                SVG => true,
                _ => false
            };
        }
    }
}
