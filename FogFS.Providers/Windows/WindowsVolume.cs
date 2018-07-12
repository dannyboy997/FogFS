using System.IO;
using FogFS.FileSystem;

namespace FogFS.Providers.Windows
{
    public class WindowsVolume : VolumeBase
    {
        private string _driveLetter;

        public WindowsVolume(string name, string driveLetter) : base(name)
        {
            _driveLetter = driveLetter;
        }

        public override string ReadAllText(string path)
        {
            return File.ReadAllText(ConstructPath(path));
        }

        public override byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(ConstructPath(path));
        }

        public override void WriteAllText(string path, string content)
        {
            File.WriteAllText(ConstructPath(path), content);
        }

        public override void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(ConstructPath(path), bytes);
        }

        public override void Delete(string path)
        {
            File.Delete(ConstructPath(path));
        }

        private string ConstructPath(string path)
        {
            return string.Format("{0}:/{1}", _driveLetter, path);
        }
    }
}
