using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using Logeecom.FileManager;

namespace Logeecom.DemoProjekat.BL.Services
{
    public class ImageService
    {
        public const string DEFAULT_IMAGE_PATH = "images";
        public const int MINIMUM_WIDTH = 600;
        public const double MINIMUM_RATIO = 4.0 / 3;
        public const double MAXIMUM_RATIO = 16.0 / 9;
        private readonly Dictionary<string, string> mimeTypeToExtension;
        private readonly IFileManager fileManager;
        
        public ImageService(IFileManager fileManager)
        {
            this.fileManager = fileManager; 
            this.mimeTypeToExtension = new Dictionary<string, string>()
            {
                { "image/png" , ".png"},
                {"image/jpeg", ".jpg" },
            };
        }

        public static string GetMinimumRatio()
        {
            return "4 by 3";
        }

        public static string GetMaximumRatio()
        {
            return "16 by 9";
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            using (var stream = image.OpenReadStream())
            {
                return await this.fileManager.Save(stream, DEFAULT_IMAGE_PATH, mimeTypeToExtension[image.ContentType]);
            }
        }

        public async Task<bool> ImageExists(string imageURI, string hostName)
        {
            return await this.fileManager.Exists(imageURI, hostName);
        }

        public bool ImageExtensionValid(IFormFile image)
        {
            return this.mimeTypeToExtension.ContainsKey(image.ContentType);
        }

        public bool ImageSizeValid(IFormFile file)
        {
            using (Image image = Image.FromStream(file.OpenReadStream()))
            {
                return image.Width >= MINIMUM_WIDTH &&
                    (double)image.Width / image.Height >= MINIMUM_RATIO &&
                    (double)image.Width / image.Height <= MAXIMUM_RATIO;
            }
        }
    }
}
