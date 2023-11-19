using System.Drawing;

namespace StickerGenerator.Services
{
    public class FileService
    {

        const string path = "/data/";

        public FileService() { }

        public List<string> GetFilesNames()
        {
            var list = new List<string>();
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                list.Add(Path.GetFileName(file));
            }
            list.Sort();
            return list;
        }

   



    }
}
