using Newtonsoft.Json;
using StickerGenerator.Models;

namespace StickerGenerator.Services
{
    public class StickerService
    {
        public string PathToFile = @"stickers.json";
        public List<StickerAmount> Stickers = new List<StickerAmount>();

        public StickerService()
        {
            string json = ReadFileToString();
            Stickers = Deserialize(json);
        }

        private string ReadFileToString()
        {
            if (!File.Exists(PathToFile))
            {
                return "[]";
            }
            return File.ReadAllText(PathToFile);
        }

        private List<StickerAmount>? Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<List<StickerAmount>>(json);
        }

        private void Serialize()
        {
            string json = JsonConvert.SerializeObject(Stickers, Formatting.Indented);
            File.WriteAllText(PathToFile, json);
        }

        private int FirstFreeId()
        {
            var listId = new List<int>();
            foreach (var sticker in Stickers)
            {
                listId.Add(sticker.Id);
            }
            if (!listId.Any())
            {
                return 0;
            }
            return Enumerable.Range(0, int.MaxValue).Except(listId).First();

        }

        public void AddModel(StickerAmount sticker)
        {
            sticker.Id = FirstFreeId();
            Stickers.Add(sticker);
            Serialize();
        }

        public void ClearAll()
        {
            Stickers.Clear();
            Serialize();
        }

        public void DeleteSticker(int id)
        {
            var sticker = Stickers.FirstOrDefault(x => x.Id == id);
            if (sticker != null)
            {
                Stickers.Remove(sticker);
                Serialize();
            }
        }

    }
}
