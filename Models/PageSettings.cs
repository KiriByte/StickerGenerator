namespace StickerGenerator.Models
{
    public class PageSettings
    {

        public int Id { get; set; }
        public int Cols { get; set; } = 1;
        public int Rows { get; set; } = 1;
        public float MarginLeft { get; set; } = 0;
        public float MarginRight { get; set; } = 0;
        public float MarginTop { get; set; } = 0;
        public float MarginBottom { get; set; } = 0;
    }
}
