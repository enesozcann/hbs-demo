using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Shared.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadDto>> UploadUserImage(string userName, IFormFile pictureFile, string folderName = "profile_images");
        IDataResult<ImageDeleteDto> Delete(string pictureName);
    }
}
