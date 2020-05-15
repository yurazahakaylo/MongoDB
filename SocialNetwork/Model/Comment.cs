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
    public class Comment
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        [BsonIgnoreIfDefault]
        public ObjectId CommentOwner { get; set; }

        [BsonIgnoreIfNull]
        public string Text { get; set; }

        [BsonIgnoreIfNull]
        public DateTime Time { get; set; }
    }

}
