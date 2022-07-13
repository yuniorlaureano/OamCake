using Microsoft.AspNetCore.Http;

namespace OamCake.Common.Helpers
{
    public static class FileUpload
    {
        public static void UploadPhoto(this IFormFile photo, string fileName, string rootDirectory, Action<string> photoCallback)
        {
            if (photo != null)
            {
                var extension = Path.GetExtension(photo.FileName);
                var newFileName = Guid.NewGuid() + extension;
                var newPath = Path.Combine(rootDirectory, "Media", "Photos", newFileName);

                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    var oldPath = Path.Combine(rootDirectory, "Media", "Photos", fileName);
                    File.Delete(oldPath);
                }

                using var fs = File.Create(newPath);
                photo.CopyTo(fs);
                fs.Flush();

                photoCallback(newFileName);
            }
            else
            {
                photoCallback(null);
            }
        }
    }
}
