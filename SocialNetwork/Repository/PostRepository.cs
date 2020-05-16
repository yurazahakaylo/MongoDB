using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PostRepository
    {
        IMongoDatabase database;
        IMongoCollection<Post> collection;
        public PostRepository()
        {
            database = Connectionstr.GetDefaultDatabase();
            collection = database.GetCollection<Post>(GetTableName());
         }

        private string GetTableName()
        {
            return "Post";
        }



        public void Add(Post post) =>
          collection.InsertOne(post);

        public void Add(IEnumerable<Post> entities) =>
            collection.InsertMany(entities);




        public void Update(ObjectId id, Post post) =>
            collection.ReplaceOne(entity => entity.Id == id, post);

        public void UpdatePost(ObjectId id, string text)
        {
            var filter = Builders<Post>.Filter.Eq("_id", id);
            var update = Builders<Post>.Update.Set("Text", text);
            collection.UpdateOne(filter, update);
        }




        public void Delete(Post post) =>
            collection.DeleteOne(entity => entity.Id == post.Id);

        public void Delete(ObjectId postId) =>
           collection.DeleteOne(entity => entity.Id == postId);





        public List<Comment> GetComments(ObjectId id)
        {
            var filter = Builders<Post>.Filter.Eq("_id", id);
            var comments = collection.Find(filter).Project(x => x.PostComments).First();
            return comments;
        }


        public List<Post> GetPosts(ObjectId postWriter) =>
            collection.Find(entity => entity.PostOwner == postWriter).ToList();

        public Post GetPost(ObjectId id) =>
          collection.Find(entity => entity.Id == id).FirstOrDefault();


        public List<Post> GetNewPosts(DateTime lastLogin, List<ObjectId> following)
        {
            var filter = Builders<Post>.Filter.Gte("Time", lastLogin);
            filter = filter & Builders<Post>.Filter.In("PostOwner", following);  
            var posts = collection.Find(filter).ToList();
            return posts;
        }




        public void AddComment(Comment comment, ObjectId id)
        {
            var filter = Builders<Post>.Filter.Eq("_id", id);
            var update = Builders<Post>.Update.Push("Comments", comment);   
            collection.UpdateOne(filter, update);
        }


        public void DeleteComment(Comment comment, ObjectId id)
        {
            var filter = Builders<Post>.Filter.Eq("_id", id);
            var update = Builders<Post>.Update.Pull("Comments", comment);
            collection.UpdateOne(filter, update);
        }

    }
}
