namespace Intive.ConfR.Application.Interfaces.AzureStorage
{
    public interface INameGenerator
    {
        /// <summary>
        /// Generates name for container
        /// </summary>
        /// <param name="email">Room email</param>
        /// <returns>Well formatted container name</returns>
        string GenerateContainerName(string email);

        /// <summary>
        /// Generates random name for photo with given extension
        /// </summary>
        /// <param name="extension">Photo extension</param>
        /// <returns>Name of the photo</returns>
        string GeneratePhotoName(string extension);

        /// <summary>
        /// Generates random name for thumbnail with given extension
        /// </summary>
        /// <param name="originalFileName">Name of file for which you want to generate thumbnail</param>
        /// <returns>Name of the thumbnail</returns>
        string GenerateThumbnailName(string originalFileName);
    }
}
