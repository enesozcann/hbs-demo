using HastaBilgiSistemi.App.Helpers.Abstract;
using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Shared.Utilites.Extensions;
using HastaBilgiSistemi.Shared.Utilities.Results.Abstract;
using HastaBilgiSistemi.Shared.Utilities.Results.ComplexTypes;
using HastaBilgiSistemi.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string _imgFolder = "images";
        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }

        public async Task<IDataResult<ImageUploadDto>> UploadUserImage(string userName, IFormFile pictureFile, string folderName = "profile_images")
        {
            if (!Directory.Exists(path: $"{_wwwroot}/{_imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory(path: $"{_wwwroot}/{_imgFolder}/{folderName}");
            }
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            // resmin formatı
            string fileExtension = Path.GetExtension(pictureFile.FileName);

            DateTime dateTime = DateTime.Now;
            // resmin tam adı
            string fileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderScore()}{fileExtension}";
            // resmin kayıt olacağı yol
            var path = Path.Combine($"{_wwwroot}/{_imgFolder}/{folderName}", fileName);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            return new DataResult<ImageUploadDto>(ResultStatus.Success, "Resim başarıyla yüklendi.", new ImageUploadDto
            {
                Name = $"{folderName}/{fileName}",
                OldName = oldFileName,
                Extension = fileExtension,
                FolderName = folderName,
                Path = path,
                Size = pictureFile.Length
            });
        }

        public IDataResult<ImageDeleteDto> Delete(string pictureName)
        {
            var fileToDelete = Path.Combine($"{_wwwroot}/{_imgFolder}/", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);
                var imageDeletedDto = new ImageDeleteDto
                {
                    Name = pictureName,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                System.IO.File.Delete(fileToDelete);
                return new DataResult<ImageDeleteDto>(ResultStatus.Success, imageDeletedDto);
            }
            else
            {
                return new DataResult<ImageDeleteDto>(ResultStatus.Error, message: "Resim bulunamadı.", data: null);
            }
        }
    }
}
