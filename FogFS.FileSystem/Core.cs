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

        public IReadOnlyDictionary<string, VolumeBase> ListVolumes()
        {
            return _volumes;
        }

        public string ReadAllText(string path)
        {
            return "";
        }
    }
}
