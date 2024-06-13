using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace C_Utilities
{
    public class Imgoperation
    {
        //public readonly ApplicationDBcontext _context;

        IWebHostEnvironment webHostEnvironment;

        public Imgoperation(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        //public bool Delete(int id)
        //{
        //    var isDeleted = false;

        //    var game = _context.f;

        //    if (game is null)
        //        return isDeleted;

        //    _context.Remove(game);
        //    var effectedRows = _context.SaveChanges();

        //    if (effectedRows > 0)
        //    {
        //        isDeleted = true;

        //        var cover = Path.Combine(_imagesPath, game.Cover);
        //        File.Delete(cover);
        //    }

        //    return isDeleted;
        //}



        public List<string> UploadFiles(List<IFormFile> files, string folderName, string fileNamePrefix)
        {
            if (files == null || files.Count == 0 || string.IsNullOrEmpty(folderName) || string.IsNullOrEmpty(fileNamePrefix))
            {
                throw new ArgumentException("Invalid input parameters.");
            }

            var uploadedFiles = new List<string>();

            foreach (var file in files)
            {
                string relativePath = UploadFile(file, folderName, fileNamePrefix);
                if (relativePath != null)
                {
                    uploadedFiles.Add(relativePath);
                }
            }

            return uploadedFiles;
        }


        public string UploadFile(IFormFile file, string folderName, string fillName)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            //if (IsPDF(file))
            //{
                // Get the file extension
                string fileExtension = Path.GetExtension(file.FileName);
                if (string.IsNullOrEmpty(fileExtension))
                {
                    throw new InvalidDataException("File does not have an extension.");
                }

                string folderPath = Path.Combine(webHostEnvironment.WebRootPath, "Images", folderName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName;
                string path;

                do
                {
                    Guid rand = Guid.NewGuid();
                    fileName = $"{fillName}_{rand}{fileExtension}";
                    path = Path.Combine(folderPath, fileName);
                } while (File.Exists(path));

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Return the relative path
                    return Path.Combine("Images", folderName, fileName).Replace("\\", "/");
                }
                catch (Exception ex)
                {
                    // Log the exception (replace with your logging mechanism)
                    // e.g., _logger.LogError(ex, "An error occurred while uploading the file.");

                    throw new ApplicationException("An error occurred while uploading the file.", ex);
                }
            //}

            //return null;
        }

        private bool IsImage(IFormFile file)
        {
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return validExtensions.Contains(fileExtension);
        }

        private bool IsPDF(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return fileExtension == ".pdf";
        }















        public string SaveImage(IFormFile image)
        {
            if (image != null)
            {
                if (IsImageValid(image))
                {
                    // Generate a unique file name using a combination of timestamp and GUID
                    var fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    var newImagePath = Path.Combine(webHostEnvironment.WebRootPath, "Images", fileName);

                    try
                    {
                        using (var stream = new FileStream(newImagePath, FileMode.Create))
                        {
                            image.CopyTo(stream);
                        }

                        // Return the relative path to the image
                        return $"/Images/{fileName}";
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception (e.g., log it or provide user-friendly error message)
                        // You may want to return an error code or message to the user
                        return null;
                    }
                }
            }

            return null;
        }

        private bool IsImageValid(IFormFile image)
        {
            // Validate the file based on content type or extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" }; // Define your list of allowed extensions
            var fileExtension = Path.GetExtension(image.FileName).ToLower();

            return image.ContentType.StartsWith("image/") && allowedExtensions.Contains(fileExtension);
        }

    }

}
