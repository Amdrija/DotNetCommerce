using System.IO;
using System.Threading.Tasks;

namespace Logeecom.FileManager
{
    public interface IFileManager
    {
        public Task<string> Save(Stream fileStream, string destination, string extension);
        public Task<string> Move(string oldLocation, string newLocation);
        public Task Delete(string location);
        public Task<string> Rename(string location, string newName);
        public Task<bool> Exists(string location, string host);
    }
}
