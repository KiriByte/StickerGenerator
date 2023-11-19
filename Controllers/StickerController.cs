using Microsoft.AspNetCore.Mvc;
using StickerGenerator.Models;
using StickerGenerator.Services;

namespace StickerGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StickerController : ControllerBase
    {
        private readonly StickerService _stickerService;

        public StickerController(StickerService service)
        {
            _stickerService = service;
        }

        [HttpGet]
        public List<StickerAmount> GetStickers()
        {
            return _stickerService.Stickers;
        }
        [HttpPost]
        public void AddModel(StickerAmount sticker)
        {
            _stickerService.AddModel(sticker);
        }

        [HttpDelete]
        public void ClearAll()
        {
            _stickerService.ClearAll();
        }
        [HttpDelete]
        public void DeleteModel(int id)
        {
            _stickerService.DeleteSticker(id);
        }


    }
}
