using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBTweetsApp.Entities
{
    public class Tweets
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public int polarity { get; set; }
        public long id { get; set; }
        public DateTime date { get; set; }
        public string query { get; set; }
        public string user { get; set; }
        public string text { get; set; }
    }
}
