using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [BsonIgnoreExtraElements]
    public class Post
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        [BsonIgnoreIfDefault]
        public ObjectId PostOwner { get; set; }

        [BsonIgnoreIfNull]
        public string Text { get; set; }

        [BsonIgnoreIfNull]
        public DateTime Time { get; set; }

        [BsonIgnoreIfNull]
        public List<Comment> PostComments { get; set; }
    }
}
