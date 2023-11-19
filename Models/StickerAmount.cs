namespace StickerGenerator.Models
{
    public class StickerAmount
    {
        public int Id {  get; set; }
        public int Amount { get; set; }
        public Sticker Sticker { get; set; }
    }
}
