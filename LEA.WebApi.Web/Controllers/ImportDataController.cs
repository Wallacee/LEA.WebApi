﻿using LEA.WebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace LEA.WebApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportDataController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUploadService uploadService;
        public IUploadService UploadService => uploadService;
        public IConfiguration Configuration => configuration;
        public ImportDataController(IUploadService _uploadService, IConfiguration _configuration)
        {
            this.uploadService = _uploadService;
            this.configuration = _configuration;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadCSV")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Configuration.GetValue<string>("ApiConstant:UploadFolderFile");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!System.IO.Directory.Exists(folderName))
                    Directory.CreateDirectory(folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(dbPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
