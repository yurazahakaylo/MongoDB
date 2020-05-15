using MongoDB.Driver;

namespace Repository
{
    public class Connectionstr
    {
        private Connectionstr()
        {

        }
        public static IMongoDatabase GetDefaultDatabase()
        {
            var connectionString = GetDefaultConnectionString();
            var client = new MongoClient(connectionString);
            return client.GetDatabase(GetDefaultDatabaseName());
        }
        private static string GetDefaultConnectionString()
        {
            return "mongodb://localhost:27017";
        }
        private static string GetDefaultDatabaseName()
        {
            return "Insta";
        }
    }
}