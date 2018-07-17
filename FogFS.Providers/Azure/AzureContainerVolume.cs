using System;
using FogFS.FileSystem;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace FogFS.Providers.Azure
{
    public class AzureContainerVolume : VolumeBase
    {
        private string _containerNam;
        private CloudBlobContainer _cloudBlobContainer;

        public AzureContainerVolume(string name, string accountName, string accountKey, string containerName) : base(name)
        {
            _containerNam = containerName;

            var cloudBlobClient = new CloudBlobClient(new StorageUri(new Uri("")), new StorageCredentials("", ""));

            _cloudBlobContainer = cloudBlobClient.GetContainerReference(_containerNam);
        }

        public override string ReadAllText(string path)
        {
            var cloudBlob = _cloudBlobContainer.GetBlobReference(path);

            using (var stream = cloudBlob.OpenRead())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public override byte[] ReadAllBytes(string path)
        {
            var cloudBlob = _cloudBlobContainer.GetBlobReference(path);

            using (var stream = cloudBlob.OpenRead())
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        public override void WriteAllText(string path, string content)
        {
            var cloudBlob = _cloudBlobContainer.GetBlockBlobReference(path);

            cloudBlob.UploadText(content);
        }

        public override void WriteAllBytes(string path, byte[] bytes)
        {
            var cloudBlob = _cloudBlobContainer.GetBlockBlobReference(path);

            cloudBlob.UploadFromByteArray(bytes, 0, bytes.Length);
        }

        public override void Delete(string path)
        {
            var cloudBlob = _cloudBlobContainer.GetBlobReference(path);
            
            cloudBlob.Delete();
        }
    }
}
