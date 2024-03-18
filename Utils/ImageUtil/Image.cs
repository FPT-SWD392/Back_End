using ImageMagick;
using Microsoft.Extensions.Configuration;

namespace Utils.ImageProcessing
{
    public class Image : IDisposable
    {
        private readonly MagickImage _image;
        private readonly int _maxLength;
        private readonly string _watermark;
        public Image(MagickImage image, IConfiguration configuration)
        {
            _image = image;
            string maxLength = configuration["ImageProcessor:PreviewLongDimensionSize"]
                ?? throw new Exception("Failed to read PreviewLongDimensionSize");
            _maxLength = int.Parse(maxLength);
            _watermark = configuration["ImageProcessor:WatermarkText"]
                ?? throw new Exception("Failed to read WatermarkText");
        }
        public Image Resize()
        {
            int newWidth;
            int newHeight;
            if (_image.Width < _image.Height)
            {
                newWidth = _maxLength * (_image.Width / _image.Height);
                newHeight = _maxLength;
            }
            else
            {
                newWidth = _maxLength;
                newHeight = _maxLength * (_image.Height / _image.Width);
            }
            _image.Resize(newWidth, newHeight);
            return this;
        }
        public Image Resize(int width, int height)
        {
            _image.Resize(width, height);
            return this;
        }
        public Image AddWatermark()
        {
            var watermarkText = new Drawables()
                .FontPointSize(25)
                .FillColor(new MagickColor("#FFFFFF22"))
                .Gravity(Gravity.Center)
                .Rotation(45)
                .Text(0, 0, _watermark);
            ITypeMetric typeMetric = watermarkText.FontTypeMetrics(_watermark);
            int canvasSize = (int)((typeMetric.TextWidth + typeMetric.TextHeight) / Math.Sqrt(2));
            using var copyright = new MagickImage("xc:none", canvasSize, canvasSize);
            copyright.Draw(watermarkText);
            _image.Tile(copyright, CompositeOperator.Over);
            return this;
        }
        public byte[] GetResult()
        {
            byte[] result = _image.ToByteArray();
            this.Dispose();
            return result;
        }

        public void Dispose()
        {
            _image?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}