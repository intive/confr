using Intive.ConfR.Application.Interfaces.AzureStorage;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Intive.ConfR.Infrastructure.AzureStorage.Helpers
{
    public class StorageConnectionHelper : IStorageConnectionHelper
    {
        private readonly StorageConnectionString _connectionString;

        public StorageConnectionHelper(IOptions<StorageConnectionString> connectionOptions)
        {
            _connectionString = connectionOptions.Value;
        }

        public CloudBlobClient GetCloudBlobClient()
        {
            var credentials = new StorageCredentials(_connectionString.AccountName, _connectionString.AccountKey);

            if (!CloudStorageAccount.TryParse(_connectionString.ConnectionString, out var storageAccount))
            {
                storageAccount = new CloudStorageAccount(credentials, false);
            }

            return storageAccount.CreateCloudBlobClient();
        }
    }
}
