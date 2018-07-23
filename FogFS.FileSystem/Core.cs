using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogFS.FileSystem.Interfaces;

namespace FogFS.FileSystem
{
    public class Core
    {
        private Dictionary<string, VolumeBase> _volumes = new Dictionary<string, VolumeBase>();

        public void AddVolume(VolumeBase volume)
        {
            if (_volumes.ContainsKey(volume.Name))
            {
                throw new InvalidOperationException("Unable to add volume. Name is already in use!");
            }

            _volumes.Add(volume.Name, volume);
        }

        public void RemoveVolume(string name)
        {
            if (!_volumes.ContainsKey(name))
            {
                throw new InvalidOperationException("Unable to remove volume. Volume name not found!");
            }
            
            _volumes.Remove(name);
        }

        public IVolume LookupVolumeByPath(string path)
        {
            return _volumes.First(e => e.Key == ParseVolumeName(path)).Value;
        }

        public string LookupLocalPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var index = path.IndexOf('/');

            return path.Substring(index, path.Length - index);
        }

        private string ParseVolumeName(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var index = path.IndexOf('/');

            return path.Substring(0, index);
        }

        public IReadOnlyDictionary<string, VolumeBase> ListVolumes()
        {
            return _volumes;
        }

        public string ReadAllText(string path)
        {
            return LookupVolumeByPath(path).ReadAllText(LookupLocalPath(path));
        }
        
        public byte[] ReadAllBytes(string path)
        {
            return LookupVolumeByPath(path).ReadAllBytes(LookupLocalPath(path));
        }

        public void WriteAllText(string path, string content)
        {
            LookupVolumeByPath(path).WriteAllText(LookupLocalPath(path), content);
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            LookupVolumeByPath(path).WriteAllBytes(LookupLocalPath(path), bytes);
        }

        public void Delete(string path)
        {
            LookupVolumeByPath(path).Delete(LookupLocalPath(path));
        }
    }
}
