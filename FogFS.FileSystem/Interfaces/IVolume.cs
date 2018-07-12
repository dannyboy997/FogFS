using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogFS.FileSystem.Interfaces
{
    public interface IVolume
    {
        string ReadAllText(string path);

        byte[] ReadAllBytes(string path);

        void WriteAllText(string path, string content);

        void WriteAllBytes(string path, byte[] bytes);

        void Delete(string path);
    }
}
