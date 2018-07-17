using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogFS.FileSystem;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.IO;

namespace FogFS.Providers.AWS
{
    public class S3BucketVolume : VolumeBase
    {
        private string _bucketName;
        
        private IAmazonS3 _s3Client;

        public S3BucketVolume(string name, string accountName, string accountKey, string bucketName) : base(name)
        {
            _bucketName = bucketName;
        }

        public override string ReadAllText(string path)
        {
            using (var stream = _s3Client.GetObjectStream(_bucketName, path, new Dictionary<string, object>()))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public override byte[] ReadAllBytes(string path)
        {
            using (var stream = _s3Client.GetObjectStream(_bucketName, path, new Dictionary<string, object>()))
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
            var byteArray = Encoding.ASCII.GetBytes(content);
            var stream = new MemoryStream(byteArray);

            _s3Client.UploadObjectFromStream(_bucketName, path, stream, new Dictionary<string, object>());
        }

        public override void WriteAllBytes(string path, byte[] bytes)
        {
            var stream = new MemoryStream(bytes);

            _s3Client.UploadObjectFromStream(_bucketName, path, stream, new Dictionary<string, object>());
        }

        public override void Delete(string path)
        {
            _s3Client.Delete(_bucketName, path, new Dictionary<string, object>());
        }
    }
}