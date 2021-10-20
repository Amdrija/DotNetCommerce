using System;
using System.IO;
using System.Threading.Tasks;

namespace Logeecom.DemoProjekat.FileManager
{
    public class WebRootFileManager : IFileManager
    {
        private readonly string hostName;
        private readonly string webRootPath;

        public WebRootFileManager(string hostName, string webRootPath)
        {
            this.hostName = hostName;
            this.webRootPath = webRootPath;
        }

        public async Task<string> Save(FileStream fileStream, string destinationPath)
        {
            string imageName = Guid.NewGuid().ToString();
            string filePath = Path.Combine(this.webRootPath, destinationPath, imageName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(stream);
                return imageName;
            }
        }

        public Task<string> Move(string oldLocationPath, string newLocationPath)
        {
            string oldPath = Path.Combine(this.webRootPath, oldLocationPath);
            string newPath = Path.Combine(this.webRootPath, oldLocationPath);

            File.Move(oldPath, newPath);

            return Task.FromResult(newPath);
        }

        public Task Delete(string path)
        {
            string file = Path.Combine(this.webRootPath, path);

            File.Delete(file);

            return Task.CompletedTask;
        }

        public Task<string> Rename(string locationPath, string newName)
        {
            return this.Move(locationPath, Path.Combine(Path.GetDirectoryName(locationPath), newName));
        }

        public Task<bool> Exists(string URI)
        {
            if (!URI.Contains(this.hostName))
            {
                return Task.FromResult(false);
            }

            string filePath = URI.Replace(this.hostName, this.webRootPath);

            return Task.FromResult(File.Exists(filePath));
        }
    }
}
