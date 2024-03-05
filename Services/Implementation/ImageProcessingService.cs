/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Services.Implementation
{
    public sealed class ImageProcessingService : IDisposable
    {
        private readonly string _watermarkText;
        private readonly int _previewLongDimensionSize;
        private MemoryStream _memoryStream;

        private bool _disposed;
        private bool _isPreview;


        public ImageProcessingService(IConfiguration configuration)
        {
            _watermarkText = configuration["ImageProcessor:WatermarkText"]
                ?? throw new Exception("Failed to get watermark text");
            _disposed = false;
            _addWatermark = false;
            _resize = false;
        }

        public void Dispose()
        {
            _memoryStream?.Dispose();
            _disposed = true;
        }

        public ImageProcessingService SetImage(IFormFile imageFile)
        {
            using Stream file = imageFile.OpenReadStream();
            file.CopyTo(_memoryStream);
            return this;
        }
        public ImageProcessingService AsPreview()
        {
            _isPreview = true;
            return this;
        }
        public async Task<string> ProcessAndUploadAsync()
        {
            using var image = new MagickImage(_memoryStream);
            if (_isPreview)
            {
                using MagickImage watermark = new(new MagickColor("#FFFFFF80"), 100, 100);
                Drawables watermark = new();
            }
        }
    }
}
*/