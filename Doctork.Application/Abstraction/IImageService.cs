﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctork.Application.Abstraction;
public interface IImageService
{
    string SetImage(IFormFile imgFile, string? oldImgUrl = null);
    public void DeleteImage(string imgUrl);
}
