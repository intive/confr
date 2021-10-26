using System;
using System.IO;
using System.Threading.Tasks;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Interfaces.AzureStorage
{
    public interface IAzureStorageImageService
    {
        /// <summary>
        /// Finds photo in storage
        /// </summary>
        /// <param name="name">Photo name</param>
        /// <param name="containerName">Container name</param>
        /// <param name="requireSas">Require Shared Access Signature</param>
        /// <returns>RoomPhoto model</returns>
        Task<RoomPhoto> FindPhoto(string name, string containerName, bool requireSas);

        /// <summary>
        /// Checks whether photo exists in storage
        /// </summary>
        /// <param name="name">Photo name</param>
        /// <param name="containerName">Container name</param>
        /// <returns>Boolean</returns>
        Task<bool> PhotoExists(string name, string containerName);

        /// <summary>
        /// Uploads photo from given stream to storage
        /// </summary>
        /// <param name="stream">Stream with photo</param>
        /// <param name="containerName">Container name</param>
        /// <param name="name">>Name of photo. If left empty name is automatically generated.</param>
        /// <returns>Name of photo created in storage.</returns>
        Task<string> UploadPhotoFromStream(Stream stream, string containerName, string name = "", int width = 0, int height = 0);

        /// <summary>
        /// Deletes photo from storage
        /// </summary>
        /// <param name="photoName">Photo name</param>
        /// <param name="containerName">Container name</param>
        /// <returns>Boolean</returns>
        Task<bool> DeletePhotoAsync(string photoName, string containerName);
    }
}
