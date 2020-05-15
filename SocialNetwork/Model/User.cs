using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Model
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        [BsonIgnoreIfNull]
        public string Nickname { get; set; }

        [BsonIgnoreIfNull]
        public string Email { get; set; }

        [BsonIgnoreIfNull]
        public string Password { get; set; }

        [BsonIgnoreIfNull]
        public List<string> Following { get; set; }

        [BsonIgnoreIfNull]
        public List<string> Followers { get; set; }

    }
}
