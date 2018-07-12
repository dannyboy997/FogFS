using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogFS.FileSystem.Interfaces;

namespace FogFS.FileSystem
{
    public abstract class VolumeBase : IVolume
    {
        public VolumeBase(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public abstract string ReadAllText(string path);

        public abstract byte[] ReadAllBytes(string path);

        public abstract void WriteAllText(string path, string text);

        public abstract void WriteAllBytes(string path, byte[] bytes);

        public abstract void Delete(string path);
    }
}
