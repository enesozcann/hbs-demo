using AutoMapper;
using HastaBilgiSistemi.App.Helpers.Abstract;
using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;

        public AccountController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, IImageHelper imageHelper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _imageHelper = imageHelper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var updateDto = _mapper.Map<UserUpdateDto>(user);
            return View(updateDto);
        }
        [HttpPost]
        public async Task<ViewResult> ChangeDetails(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.GetUserAsync(HttpContext.User);
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    var uploadedImageDtoResult = await _imageHelper.UploadUserImage(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    userUpdateDto.Picture = uploadedImageDtoResult.ResultStatus == ResultStatus.Success
                        ? uploadedImageDtoResult.Data.Name : "profile_images/default.png";
                    if (oldUserPicture != "profile_images/default.png")
                    {
                        isNewPictureUploaded = true;
                    }
                }
                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        _imageHelper.Delete(oldUserPicture);
                    }
                    TempData.Add("SuccessMessage", "Hesap bilgileriniz başarıyla güncellendi.");
                    return View(userUpdateDto);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(userUpdateDto);
                }
            }
            else
            {
                return View(userUpdateDto);
            }
        }
        [HttpGet]
        public ViewResult PasswordChange()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var isVerified = await _userManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword, userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false);
                        TempData.Add("SuccessMessage", "Parolanız başarıyla güncellendi.");
                        return View();
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(userPasswordChangeDto);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mevcut parolanız geçersiz, lütfen parolanızı giriniz");
                    return View(userPasswordChangeDto);
                }
            }
            else { return View(userPasswordChangeDto); }
        }
    }
}
