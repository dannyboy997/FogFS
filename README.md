# FogFS
FogFS is a abstract filesystem that eases the use of multiple file system be it local or in the cloud. Because this filesystem is abstract, there are no code changes needed when switching between local Windows, Azure Blob, AWS S3, GCP and many more.

Free yourself and your team from cloud lock-in!

# Interface
```c#

public interface IVolume
{
        string ReadAllText(string path);

        byte[] ReadAllBytes(string path);

        void WriteAllText(string path, string content);

        void WriteAllBytes(string path, byte[] bytes);

        void Delete(string path);
}

```
