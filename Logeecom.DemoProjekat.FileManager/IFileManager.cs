using System.IO;
using System.Threading.Tasks;

namespace Logeecom.DemoProjekat.FileManager
{
    public interface IFileManager
    {
        public Task<string> Save(FileStream fileStream, string destinationPath);
        public Task<string> Move(string oldLocationPath, string newLocationPath);
        public Task Delete(string path);
        public Task<string> Rename(string locationPath, string newName);
        public Task<bool> Exists(string path);
    }
}
