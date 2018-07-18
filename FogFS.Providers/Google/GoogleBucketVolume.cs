using System.Text;
using FogFS.FileSystem;
using Google.Cloud.Storage.V1;
using System.IO;

namespace FogFS.Providers.AWS
{
    public class GoogleBucketVolume : VolumeBase
    {
        private string _bucketName;
        private StorageClient _storageClient;

        public GoogleBucketVolume(string name, string accountName, string accountKey, string bucketName) : base(name)
        {
            _bucketName = bucketName;

            _storageClient = StorageClient.Create();
        }

        public override string ReadAllText(string path)
        {
            using (var stream = new MemoryStream())
            {
                _storageClient.DownloadObject(_bucketName, path, stream);

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public override byte[] ReadAllBytes(string path)
        {
            using (var stream = new MemoryStream())
            {
                _storageClient.DownloadObject(_bucketName, path, stream);

                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        public override void WriteAllText(string path, string content)
        {
            var byteArray = Encoding.ASCII.GetBytes(content);
            var stream = new MemoryStream(byteArray);
            
            _storageClient.UploadObject(_bucketName, path, null, stream);
        }

        public override void WriteAllBytes(string path, byte[] bytes)
        {
            var stream = new MemoryStream(bytes);

            _storageClient.UploadObject(_bucketName, path, null, stream);
        }

        public override void Delete(string path)
        {
            _storageClient.DeleteObject(_bucketName, path);
        }
    }
}