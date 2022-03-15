using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace TaskManagerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
        public class FileUploadApiController : ControllerBase
        {
            private readonly IFileProvider _IFileProvider;
            public FileUploadApiController(IFileProvider IFileProvider)
            {
                _IFileProvider = IFileProvider;
            }
            [HttpPost("")]
            [DisableRequestSizeLimit]

            public IActionResult UploadFile()
            {
                try
                {
                    // 1. get the file form the request
                    var postedFile = Request.Form.Files[0];
                    // 2. set the file uploaded folder
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Attachments");
                    // 3. check for the file length, if it is more than 0 the save it
                    if (postedFile.Length > 0)
                    {
                        // 3a. read the file name of the received file
                        var fileName = ContentDispositionHeaderValue.Parse(postedFile.ContentDisposition)
                            .FileName.Trim('"');
                        // 3b. save the file on Path
                        var finalPath = Path.Combine(uploadFolder, fileName);
                        using (var fileStream = new FileStream(finalPath, FileMode.Create))
                        {
                            postedFile.CopyTo(fileStream);
                        }
                        return Ok($"File is uploaded Successfully");
                    }
                    else
                    {
                        return BadRequest("The File is not received.");
                    }


                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Some Error Occcured while uploading File {ex.Message}");
                }
            }
            [HttpGet("")]
            public FileContentResult Get(string name)
            {

                return File(System.IO.File.ReadAllBytes(_IFileProvider.GetFileInfo(name).PhysicalPath), "application/octet-stream", "picture.jpg");
            }
        }
    }

