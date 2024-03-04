using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{
   private readonly IFirebaseStorageService _firebaseStorageService;

   public FileController(IFirebaseStorageService firebaseStorageService)
   {
      _firebaseStorageService = firebaseStorageService;
   }

   [HttpPost("upload")]
   public async Task<IActionResult> Upload(IFormFile file)
   {
      var filePath = Path.GetTempFileName();
      using (var stream = new FileStream(filePath, FileMode.Create))
      {
         await file.CopyToAsync(stream);
      }

      var objectName = $"{Guid.NewGuid()}_{file.FileName}";
      var gcsUri = await _firebaseStorageService.UploadFileAsync(filePath, objectName, contentType: file.ContentType);
      
      System.IO.File.Delete(filePath);

      return Ok(new { GcsUri = gcsUri });
   }

   [HttpGet("download/{objectName}")]
   public async Task<IActionResult> Download([FromRoute] string objectName)
   {
      var stream = await _firebaseStorageService.DownloadFileAsync(objectName);
      return File(stream, "application/octet-stream",objectName);
   }
}