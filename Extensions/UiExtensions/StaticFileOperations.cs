using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Extensions.Generators;
using Microsoft.AspNetCore.Http;

namespace Extensions.UiExtensions
{
    public static class StaticFileOperations
    {
        public enum StaticFileTypes
        {
            Logo,
            FooterLogo,
            Fav,
            Slider
        }

        private static string _rootBase = "wwwroot/files";

        private static string _savePath = Path.Combine(_rootBase, "Uploaded");

        public static bool DirectoryCreate(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool DirectoryDelete(string module, Guid itemId)
        {
            var path = Path.Combine(_savePath, module, itemId.ToString());
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch (Exception e)
                {
                    return false;
                }

            }

            return true;
        }

        public static bool DirectoryDelete(string module)
        {
            var path = Path.Combine(_savePath, module);
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch (Exception e)
                {
                    return false;
                }

            }

            return true;
        }

        public static async Task<bool> UploadFiles(string module, Guid itemId, IFormFileCollection fileCollection)
        {
            if (fileCollection.Count == 0)
                return true;
            var path = Path.Combine(_savePath, module, itemId.ToString());
            DirectoryCreate(path);
            var len = Directory.GetFiles(path).Length;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                if (fileCollection[i].Length > 0)
                {
                    if (!await SaveFile(fileCollection[i], len + i, path, module))
                        return false;

                }
            }


            return true;
        }



        public static async Task<bool> UploadFiles(string module, IFormFileCollection fileCollection)
        {

            if (fileCollection.Count == 0)
                return true;

            var path = Path.Combine(_savePath, module);
            DirectoryCreate(path);
            var len = Directory.GetFiles(path).Length;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                if (fileCollection[i].Length > 0)
                {
                    if (!await SaveFile(fileCollection[i], len + i, path, module))
                        return false;

                }
            }

            return true;
        }


        public static async Task<bool> SaveFile(IFormFile file, int index, string path, string module)
        {
            try
            {
                var ext = Path.GetExtension(file.FileName);
                var fName = $"{index}-{RandomGenerator.CreateNumber(3)}{ext}";
                path = Path.Combine(path, fName);
                var pos = PosWithModule(module);
                ImageResizer.ImageResizer.Resize(file, pos[0], pos[1], path, _savePath);
                //using (var fileStream = new FileStream(path, FileMode.Create))
                //{
                //    await file.CopyToAsync(fileStream);
                //}

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<string> SaveFileGetPath(IFormFile file, int index, string path, string module)
        {
            try
            {
                var ext = Path.GetExtension(file.FileName);
                var fName = $"{index}-{RandomGenerator.CreateNumber(3)}{ext}";
                path = Path.Combine(path, fName);
                var pos = PosWithModule(module);
                if (ext.Contains("pdf"))
                {
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    ImageResizer.ImageResizer.Resize(file, pos[0], pos[1], path, _savePath);
                }

                return path;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static async Task<string> UploadFilesGetPath(string module, Guid id, IFormFileCollection fileCollection)
        {
            if (fileCollection.Count == 0)
                return "";
            var path = Path.Combine(_savePath, module, id.ToString());
            DirectoryCreate(path);
            var len = Directory.GetFiles(path).Length;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                if (fileCollection[i].Length > 0)
                {
                    return await SaveFileGetPath(fileCollection[i], len + i, path, module);
                }
            }


            return "";
        }

        public static async Task<bool> SaveFileDeleteOther(IFormFileCollection fileCollection, string module)
        {
            try
            {
                var path = Path.Combine(_savePath, module);
                DirectoryDelete(module);
                DirectoryCreate(Path.Combine(_savePath, module));
                if (fileCollection.Count == 0)
                    return true;

                foreach (var file in fileCollection)
                {
                    if (file.Length > 0)
                    {
                        if (!await SaveFile(file, 1, path, module))
                            return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<string> FilePaths(Guid? itemId, string module)
        {
            var imageList = new List<string>();

            if (itemId == null || itemId == Guid.Empty)
                return imageList;

            var path = Path.Combine(_savePath, module, itemId.ToString());
            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                    imageList.Add($"/{file.Replace("wwwroot/", "").Replace('\\', '/')}");

            }

            if (imageList.IsNullOrEmpty())
                imageList.Add(FirstImagePath("Logo").Result);

            return imageList;
        }

        public static List<string> FilePaths(string module)
        {
            var imageList = new List<string>();

            var path = Path.Combine(_savePath, module);
            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                    imageList.Add($"/{file.Replace("wwwroot/", "").Replace('\\', '/')}");

            }

            return imageList;
        }

        public static async Task<string> FirstImagePath(Guid? itemId, string module)
        {
            if (itemId == null || itemId == Guid.Empty)
                return "/files/logo.png";

            var path = Path.Combine(_savePath, module, itemId.ToString());
            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                    return $"/{file.Replace("wwwroot/", "").Replace('\\', '/')}";
            }

            return await FirstImagePath("Logo");
        }

        public static async Task<string> FirstImagePath(string module)
        {
            var path = Path.Combine(_savePath, module);
            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                    return $"/{file.Replace("wwwroot/", "").Replace('\\', '/')}";
            }

            return "/files/logo.png";
        }


        public static bool DeleteFile(string path)
        {
            try
            {
                var virtualPath = Path.Combine("wwwroot", path.Substring(1));
                if (File.Exists(virtualPath))
                    File.Delete(virtualPath);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<string> ReOrderItems(List<string> paths, string pathBase)
        {
            var newPaths = new List<string>();
            if (paths.Count == 0)
                return newPaths;

            var splitFolder = paths[0].Split('/').ToList();
            if (splitFolder.Count == 0)
                return newPaths;
            splitFolder.RemoveAt(splitFolder.Count - 1);
            var orginFolder = string.Join('/', splitFolder);
            var folderBase = pathBase + Path.Combine("wwwroot", orginFolder);
            for (int i = 0; i < paths.Count; i++)
            {
                var ext = Path.GetExtension(paths[i]);
                var fName = $"{i}-{RandomGenerator.CreateNumber(3)}{ext}";
                newPaths.Add(Path.Combine(orginFolder, fName).Replace("\\", "/"));
                var path = Path.Combine(folderBase, fName);

                var oldpath = pathBase + Path.Combine("wwwroot", paths[i]);
                File.Move(oldpath, path);
            }
            return newPaths;
        }

        public static string PngToBase64(Guid? id, string module)
        {
            var first = FirstImagePath(id, module).Result;
            var basePath = BasePath();
            var p1 = Path.Combine(basePath, "wwwroot") + "/" + first;
            byte[] b = File.ReadAllBytes(p1);
            return "data:image/png;base64," + Convert.ToBase64String(b);
        }

        public static string BasePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("\\bin"));
        }

        public static int[] PosWithModule(string module)
        {
            return new[] { 0, 0 };
        }

    }
}
