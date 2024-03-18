using Utils.ImageProcessing;

namespace Utils.ImageUtil
{
    public interface IImageUtil : IDisposable
    {
        public Image SetImage(byte[] imageFile);
        public void Dispose();
    }
}
