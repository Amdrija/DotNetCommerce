using System;
using System.IO;
using System.Threading.Tasks;

namespace Logeecom.FileManager
{
    public class WebRootFileManager : IFileManager
    {
        private readonly string webRootPath;

        public WebRootFileManager(string webRootPath)
        {
            this.webRootPath = webRootPath;
        }

        public async Task<string> Save(Stream stream, string destinationPath, string extension)
        {
            string imageName = Guid.NewGuid().ToString() + extension;
            string filePath = Path.Combine(this.webRootPath, destinationPath, imageName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
                return imageName;
            }
        }

        public Task<string> Move(string oldLocationPath, string newLocationPath)
        {
            string oldPath = Path.Combine(this.webRootPath, oldLocationPath);
            string newPath = Path.Combine(this.webRootPath, newLocationPath);

            File.Move(oldPath, newPath);

            return Task.FromResult(newLocationPath);
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

        public Task<bool> Exists(string URI, string hostName)
        {
            if (!URI.Contains(hostName))
            {
                return Task.FromResult(false);
            }

            string filePath = URI.Replace(hostName, this.webRootPath);

            return Task.FromResult(File.Exists(filePath));
        }
    }
}
