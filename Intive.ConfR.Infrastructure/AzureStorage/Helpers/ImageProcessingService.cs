using Intive.ConfR.Application.Interfaces.AzureStorage;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Intive.ConfR.Infrastructure.AzureStorage.Helpers
{
    public class ImageProcessingService : IImageProcessingService
    {
        private readonly ThumbnailDimensions _dimensions;

        public ImageProcessingService(IOptions<ThumbnailDimensions> dimensionsOptions)
        {
            _dimensions = dimensionsOptions.Value;
        }

        public Image<Rgba32> CreatePhotoThumbnail(Image<Rgba32> image, int width, int height)
        {
           var thumbnail = image.Clone();
           thumbnail.Mutate(ctx => ctx.Resize(width != 0 ? width : _dimensions.Width, height != 0 ? height : _dimensions.Height));

           return thumbnail;
        }
    }
}
