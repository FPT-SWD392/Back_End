using ImageMagick;
using Microsoft.Extensions.Configuration;
using Utils.ImageUtil;

namespace Utils.ImageProcessing
{
    public class ImageUtil : IDisposable, IImageUtil
    {
        private readonly IConfiguration _configuration;
        private Image? _image;
        public ImageUtil(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Image SetImage(byte[] imageFile)
        {
            _image?.Dispose();
            using Stream file = new MemoryStream(imageFile);
            _image = new(new MagickImage(file), _configuration);
            return _image;
        }
        public void Dispose()
        {
            _image?.Dispose();
            _image = null;
            GC.SuppressFinalize(this);
        }
    }
}
