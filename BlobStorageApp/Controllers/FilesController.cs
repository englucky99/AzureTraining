using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FileUploader.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FileUploader.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private const int MaxFilenameLength = 50;
        private static readonly Regex filenameRegex = new Regex("[^a-zA-Z0-9._]");

        private readonly IStorage storage;

        public FilesController(IStorage storage)
        {
            this.storage = storage;
        }

        
        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var names = await storage.GetNames();

            var baseUrl = Request.Path.Value;

            var urls = names.Select(n => $"{baseUrl}/{n}");

            return Ok(urls);            
        }

        
        [HttpPost()]
        public async Task<IActionResult> Upload(IFormFile file)
        {
           
            var name = SanitizeFilename(file.FileName);

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException();
            }

            using (Stream stream = file.OpenReadStream())
            {
                await storage.Save(stream, name);
            }
            
            return Accepted();
        }

        // GET /api/Files/{filename}
      
        [HttpGet("{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            var stream = await storage.Load(filename);
            return File(stream, "application/octet-stream", filename);
        }

        private static string SanitizeFilename(string filename)
        {
            var sanitizedFilename = filenameRegex.Replace(filename, "").TrimEnd('.');

            if (sanitizedFilename.Length > MaxFilenameLength)
            {
                sanitizedFilename = sanitizedFilename.Substring(0, MaxFilenameLength);
            }

            return sanitizedFilename;
        }
    }
}