using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDBTweetsApp.Entities;

namespace MongoDBTweetsApp.Services
{
    public class Mongo
    {
        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<Tweets> coll;
        
        public Mongo()
        {
            client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("social_net");
            coll = db.GetCollection<Tweets>("tweets");
        }

        public int getNumberOfUsers()
        {
            var users = coll.Aggregate()
                .Group(new BsonDocument { { "_id", "$user" } })
                .ToList();
            var count = users.Count;
            return count;
        }

        //public List<String> getUsersWithMostLinksToOtherUsers()
        //{
        //    var filter = Builders<BsonDocument>.Filter.Regex("text", new BsonRegularExpression("/@w +/", "i"));
        //    var users = coll.Aggregate()
        //        .Match(new BsonDocument { { "text", "/@w +/" } })
        //        .Group(new BsonDocument { { "id", null }, { "text", } });
        //    return null;
        //}

        public List<string> mostGrumpyUsers()
        {
            var collection = db.GetCollection<BsonDocument>("tweets");
            List<String> users = new List<string>();
            var grumpyUsers = collection.Aggregate()
                .Match(new BsonDocument { { "polarity", 0 } })
                .Group(new BsonDocument { { "_id", "$user" } })
                .Limit(5)
                .ToList();

            foreach (BsonDocument item in grumpyUsers)
            {
                users.Add(item["_id"].ToString());
            }

            return users;
        }

        public List<string> mostHappyUsers()
        {
            var collection = db.GetCollection<BsonDocument>("tweets");
            List<String> users = new List<string>();
            var happyUsers = collection.Aggregate()
                .Match(new BsonDocument { { "polarity", 4 } })
                .Group(new BsonDocument { { "_id", "$user" } })
                .Limit(5)
                .ToList();

            foreach (BsonDocument item in happyUsers)
            {
                users.Add(item["_id"].ToString());
            }

            return users;
        }

        public List<string> mostActiveUsers()
        {
            var collection = db.GetCollection<BsonDocument>("tweets");
            List<string> users = new List<string>();
            var sort = Builders<BsonDocument>.Sort.Descending("noOfTweets");
            var mostActive = collection.Aggregate(new AggregateOptions { AllowDiskUse = true })
                .Group(new BsonDocument {
                    { "_id", new BsonDocument {
                        { "user", "$user" },
                        { "tweet", "$id" }
                    } }
                })
                .Group(new BsonDocument {
                    { "_id", "$_id.user" },
                    { "noOfTweets", new BsonDocument {
                        { "$sum", 1}
                    } }
                })
                .Limit(10)
                .Sort(sort)
                .ToList();

            foreach (BsonDocument item in mostActive)
            {
                users.Add(item["_id"].ToString());
            }

            return users;
        }
    }
}
