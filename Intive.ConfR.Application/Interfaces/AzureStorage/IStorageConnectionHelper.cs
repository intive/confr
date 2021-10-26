using Microsoft.WindowsAzure.Storage.Blob;

namespace Intive.ConfR.Application.Interfaces.AzureStorage
{
    public interface IStorageConnectionHelper
    {
        /// <summary>
        /// Returns cloud blob client.
        /// Object lets you use storage functionality.
        /// </summary>
        /// <returns>Cloud blob client</returns>
        CloudBlobClient GetCloudBlobClient();
    }
}
