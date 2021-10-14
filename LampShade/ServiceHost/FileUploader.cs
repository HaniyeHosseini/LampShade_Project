﻿using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file, string path)
        {
            if (file == null) return "";

            var directoryPath = $"{_webHostEnvironment.WebRootPath}//ProductPictures//{path}";
            if (! Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileName = DateTime.Now.ToFileName() + "-" + file.FileName;
            var filepath = $"{directoryPath}//{fileName}";

            using var output = File.Create(filepath);
            file.CopyTo(output);
            return $"{path}/{fileName}";

        }
    }
}