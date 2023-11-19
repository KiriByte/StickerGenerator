using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using StickerGenerator.Services;

namespace StickerGenerator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PDFDocumentController : ControllerBase
    {
        private readonly StickerService _stickerService;
        private readonly PageSettingsService _pageSettingsService;
        private readonly PDFGenerator _pdfGenerator;

        public PDFDocumentController(StickerService stickerService, PageSettingsService pageSettingsService, PDFGenerator pdfGenerator)
        {
            _stickerService = stickerService;
            _pageSettingsService = pageSettingsService;
            _pdfGenerator = pdfGenerator;
        }

        [HttpGet]
        public IActionResult Generate(int pageSettingsId)
        {
            var settings = _pageSettingsService.PageSettings.Find(x => x.Id == pageSettingsId);
            if (settings != null)
            {
                _pdfGenerator.GenerateDocument(settings);
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(int id)
        {
            var filePath = "/tables/stickers_20231113143047.pdf";
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            string contentType = "application/pdf";
            string downloadName = "stickers_20231113143047.pdf";
            return File(fileStream, contentType, downloadName);
        }
    }
}
