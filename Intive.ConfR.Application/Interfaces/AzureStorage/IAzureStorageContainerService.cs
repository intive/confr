using System.Collections.Generic;
using System.Threading.Tasks;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Interfaces.AzureStorage
{
    public interface IAzureStorageContainerService
    {
        /// <summary>
        /// Creates container in storage with given name
        /// </summary>
        /// <param name="name">Container name</param>
        /// <param name="makePublic">Set container to be public</param>
        /// <returns>Nothing</returns>
        Task CreateContainerAsync(string name, bool makePublic = false);

        /// <summary>
        /// Returns whole content of the container
        /// </summary>
        /// <param name="name">Container name</param>
        /// <param name="requireSas">Apply container scoped SAS</param>
        /// <returns>Collection of RoomPhotos</returns>
        Task<IEnumerable<RoomPhoto>> GetContainerContentsAsync(string name, bool requireSas);

        /// <summary>
        /// Deletes container with given name
        /// </summary>
        /// <param name="name">Container name</param>
        /// <returns>Nothing</returns>
        Task DeleteContainerAsync(string name);

        /// <summary>
        /// Checks whether container exists
        /// </summary>
        /// <param name="name">Name of container</param>
        /// <returns>Boolean</returns>
        Task<bool> ContainerExists(string name);
    }
}
