using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Intive.ConfR.Application.Interfaces.AzureStorage
{
    public interface IImageProcessingService
    {
        Image<Rgba32> CreatePhotoThumbnail(Image<Rgba32> image, int width, int height);
    }
}
