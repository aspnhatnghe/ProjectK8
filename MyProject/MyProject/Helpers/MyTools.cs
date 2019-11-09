using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyProject.Helpers
{
    public class MyTools
    {
        public static string UploadFile(IFormFile fHinh, string folder)
        {
            if (fHinh == null) return string.Empty;
            try
            {
                var fileName = Path.GetFileNameWithoutExtension(fHinh.FileName).ToLower();
                var fileNameCleaned = Regex.Replace(fileName, @"[^a-z0-9]", "") + $"{DateTime.Now.Ticks}{Path.GetExtension(fHinh.FileName)}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folder, fileNameCleaned);
                using (var file = new FileStream(filePath, FileMode.Create))
                {
                    fHinh.CopyTo(file);
                }

                return fileNameCleaned;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
