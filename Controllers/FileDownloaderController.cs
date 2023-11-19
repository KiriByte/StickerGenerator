using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StickerGenerator.Services;

namespace StickerGenerator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileDownloaderController : ControllerBase
    {
        private FileService _fileService;
        public FileDownloaderController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public List<string> GetFilesNames()
        {
            return _fileService.GetFilesNames();
        }

        [HttpGet]
        public IResult DownloadFile(string fileName)
        {
            FileStream fileStream = new FileStream("/data/"+fileName,FileMode.Open);
            string contentType = "application/pdf";
            string downloadName = fileName;
            return Results.File(fileStream,contentType, downloadName);
        }
    }
}
