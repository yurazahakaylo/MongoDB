using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UserRepository
    {
        IMongoDatabase database;
        IMongoCollection<User> collection;
        public UserRepository()
        {
            database = Connectionstr.GetDefaultDatabase();
            collection = database.GetCollection<User>(GetTableName());

        }

        private string GetTableName()
        {
            return "User";
        }


        public void Add(User user) =>
           collection.InsertOne(user);

        public void Add(IEnumerable<User> entities) =>
            collection.InsertMany(entities);



        public void Update(string nickname, User user) =>
            collection.ReplaceOne(entity => entity.Nickname == nickname, user);

        public void Update(ObjectId id, User user) =>
            collection.ReplaceOne(entity => entity.Id == id, user);


        public void UpdateField(string nickname, string fieldToEdit, string value)
        {
            var filter = Builders<User>.Filter.Eq("Nickname", nickname);
            var update = Builders<User>.Update.Set(fieldToEdit, value);
            collection.UpdateOne(filter, update);
        }




        public void Delete(User user) =>
            collection.DeleteOne(u => u.Id == user.Id);

        public void Delete(ObjectId id) =>
            collection.DeleteOne(u => u.Id == id);

        public void Delete(string nickname) =>
            collection.DeleteOne(u => u.Nickname == nickname);



        public List<string> GetFollowers(string nickname)
        {
            var filter = Builders<User>.Filter.Eq("Nickname", nickname);
            var users = collection.Find(filter).Project(x => x.Followers).First();
            return users;
        }

        public List<string> GetFollowing(string nickname)
        {
            var filter = Builders<User>.Filter.Eq("Nickname", nickname);
            var users = collection.Find(filter).Project(x => x.Following).First();
            return users;
        }

        public ObjectId GetId(string nickname)
        {
            var user = collection.Find(entity => entity.Nickname == nickname).FirstOrDefault();
            return user.Id;
        }

        public List<User> GetUsers() =>
            collection.Find(entity => true).ToList();

        public User GetUser(string nickname) =>
          collection.Find(entity => entity.Nickname == nickname).FirstOrDefault();

        public User GetUser(ObjectId id) =>
         collection.Find(entity => entity.Id == id).FirstOrDefault();



        public void Follow(string nickname, string plus)
        {
            var filter = Builders<User>.Filter.Eq("Nickname", nickname);
            var update = Builders<User>.Update.Push("Followers", plus);
            collection.UpdateOne(filter, update);
        }

        public void Follow(ObjectId user, ObjectId plus)
        {
            var filter = Builders<User>.Filter.Eq("Nickname", user);
            var update = Builders<User>.Update.Push("Followers", plus);   /////////////////////////////////////////////////////
            collection.UpdateOne(filter, update);
        }

        public void Following(string nickname, string plus)
        {
            var filter = Builders<User>.Filter.Eq("Nickname", nickname);
            var update = Builders<User>.Update.Push("Following", plus);
            collection.UpdateOne(filter, update);
        }

        public void Following(ObjectId user, ObjectId plus)
        {
            var filter = Builders<User>.Filter.Eq("Nickname", user);        ///////////////////////////////////////////////////
            var update = Builders<User>.Update.Push("Following", plus);
            collection.UpdateOne(filter, update);
        }

        public void Unfollow(string nickname, string follower)
        {
            var filter = Builders<User>.Filter.Eq("Nickname", nickname);
            var update = Builders<User>.Update.Pull("Following", follower);
            collection.UpdateOne(filter, update);


            filter = Builders<User>.Filter.Eq("Nickname", follower);
            update = Builders<User>.Update.Pull("Followers", nickname);
            collection.UpdateOne(filter, update);
        }

    }
}
