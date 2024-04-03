using Doctork.Application.Abstraction;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Services;
public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private const string IMAGE_FOLDER_PATH = "\\images\\";
    private const string DEFAULT_USER_IMAGE = IMAGE_FOLDER_PATH + "Default_User_Image.png";
    public ImageService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public string SetImage(IFormFile imgFile, string? oldImgUrl = null)
    {
        if (oldImgUrl != null)
            DeleteImage(oldImgUrl);

        var imgGuid = Guid.NewGuid();
        var imgExtension = Path.GetExtension(imgFile.FileName);
        var imgName = imgGuid + imgExtension;
        var imgUrl = IMAGE_FOLDER_PATH + imgName;

        var imgPath = _webHostEnvironment.WebRootPath + imgUrl;
        using var imgStream = new FileStream(imgPath, FileMode.Create);
        imgFile.CopyTo(imgStream);
        return imgUrl;
    }
    public void DeleteImage(string imgUrl)
    {
        if (!string.IsNullOrEmpty(imgUrl) && imgUrl != DEFAULT_USER_IMAGE)
        {
            var imgOldPath = _webHostEnvironment.WebRootPath + imgUrl;

            if (File.Exists(imgOldPath))
            {
                File.Delete(imgOldPath);
            }
        }
    }
}

