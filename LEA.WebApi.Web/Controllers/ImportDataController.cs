using LEA.WebApi.Service.Interfaces;
using LEA.WebApi.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace LEA.WebApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportDataController : Controller
    {
        private readonly IUploadService uploadService;
        public IUploadService UploadService => uploadService;
        public ImportDataController(IUploadService uploadService)
        {
            this.uploadService = uploadService;
        }

        [HttpPost]
        public string UploadData([FromForm] IFormFile formFile)
        {
            if (formFile != null)
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                FileStream fileStream = System.IO.File.Create(formFile.FileName);
                fileStream.Close();
                FileStream _fileStream = System.IO.File.O(formFile.FileName, FileMode.Open, FileAccess.Read);
                uploadService.Upload(_fileStream);
                fileStream.Close();
                
            }
            return "show";

        }

    }
}
