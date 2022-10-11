using ImageMagick;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace Extensions.ImageResizer
{

    public class ImageResizer
    {
        public async static void Resize(IFormFile file, int width, int heigth, string savePath, string tempPath)
        {
            savePath = savePath.Replace("\\", "/");
            tempPath = tempPath.Replace("\\", "/");
            MagickNET.SetTempDirectory(tempPath);
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();
                using (var image = new MagickImage(fileBytes))
                {
                    if (heigth != 0)
                        image.Resize(new MagickGeometry() { IgnoreAspectRatio = true, Height = heigth, Width = width});
                    image.Strip();
                    image.Format = MagickFormat.WebP;
                    //image.Quality = quality;
                    
                    image.Write(savePath);
                }
            }
        }
    }
}