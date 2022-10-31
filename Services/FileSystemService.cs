using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Net.Sockets;

namespace UserAuthBlazorApp.Services
{
    public class FileSystemService
    {
        static MongoClient client = new MongoClient("mongodb://localhost");
        static IMongoDatabase database = client.GetDatabase("Images320");
        static GridFSBucket gridFS = new GridFSBucket(database);
        public void UploadImageToDb(string name)
        {
            using (FileStream fs = new FileStream("./Temp/" + name, FileMode.Open))
            {
                gridFS.UploadFromStream(name, fs);
            }
        }
        public async Task UploadImageToDbAsync(string name, Stream fs)
        {
            await gridFS.UploadFromStreamAsync(name, fs);
        }

        public void DownloadToLocal(string name)
        {
            using (FileStream fs = new FileStream($"{Directory.CreateDirectory("./wwwroot/images/")}{name}", FileMode.Create))
            {
                gridFS.DownloadToStreamByName(name, fs);
            }
        }

        public string[] GetAllNames()
        {
            var filter = Builders<GridFSFileInfo>.Filter.Empty;
            string[] fileNames = new string[] { };
            using (var cursor = gridFS.Find(filter))
            {
                fileNames = cursor.ToList().Select(x => x.Filename).ToArray();
            }
            return fileNames;
        }

        public void DeleteImage(string name)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", name);
            using (var cursor = gridFS.Find(filter))
            {
                var id = cursor.ToList().Select(x => x.Id).FirstOrDefault();
                gridFS.Delete(id);
            }
        }
    }
}