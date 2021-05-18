using MusicStreamServiceApp.DAL.MongoDB.Interfaces;

namespace MusicStreamServiceApp.DAL.MongoDB
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
