using Newtonsoft.Json;
using StickerGenerator.Models;

namespace StickerGenerator.Services
{
    public class PageSettingsService
    {
        public string PathToFile = @"pagesettings.json";
        public List<PageSettings> PageSettings = new List<PageSettings>();

        public PageSettingsService()
        {
            string json = ReadFileToString();
            PageSettings = Deserialize(json);
        }

        public void AddModel(PageSettings pageSettings)
        {
            PageSettings.Add(pageSettings);
            Serialize();
        }

        public PageSettings GetModel(int id)
        {
            return PageSettings.Find(x => x.Id == id);
        }

        public void ChangeModel(int id, PageSettings pageSettings)
        {
            var model = PageSettings.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                PageSettings.Remove(model);
                pageSettings.Id = id;
                PageSettings.Add(pageSettings);
                Serialize();
            }

        }

        public void DeleteModelByID(int id)
        {
            var pageSetting = PageSettings.FirstOrDefault(x => x.Id == id);
            if (pageSetting != null)
            {
                PageSettings.Remove(pageSetting);
                Serialize();
            }
        }
        public void ClearAll()
        {
            PageSettings.Clear();
            Serialize();
        }

        private string ReadFileToString()
        {
            if (!File.Exists(PathToFile))
            {
                return "[]";
            }
            return File.ReadAllText(PathToFile);
        }

        private List<PageSettings>? Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<List<PageSettings>>(json);
        }

        private void Serialize()
        {
            string json = JsonConvert.SerializeObject(PageSettings, Formatting.Indented);
            File.WriteAllText(PathToFile, json);
        }
    }
}
