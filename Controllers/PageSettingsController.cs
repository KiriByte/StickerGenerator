using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StickerGenerator.Models;
using StickerGenerator.Services;

namespace StickerGenerator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PageSettingsController : ControllerBase
    {
        private readonly PageSettingsService _pageSettingsService;

        public PageSettingsController(PageSettingsService pageSettingsService)
        {
            _pageSettingsService = pageSettingsService;
        }

        [HttpGet]
        public List<PageSettings> GetPageSettings()
        {
            return _pageSettingsService.PageSettings;
        }

        [HttpPost]
        public IActionResult AddPageSettings(PageSettings pageSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _pageSettingsService.AddModel(pageSettings);
            return Ok();
        }

        [HttpPut]
        public IActionResult ChangePageSettings(int id, PageSettings pageSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _pageSettingsService.ChangeModel(id, pageSettings);
            return Ok();
        }

        [HttpDelete]
        public void ClearAll()
        {
            _pageSettingsService.ClearAll();
        }

        [HttpDelete]
        public void DeleteByID(int id)
        {
            _pageSettingsService.DeleteModelByID(id);
        }
    }
}
