using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThiHocKy.Models
{
    public class MongoDBConnect
    {
        private IMongoDatabase db;
        public IMongoDatabase Getdb()
        {
            return db;
        }
        public MongoDBConnect(string database_name)
        {
            var connect = new MongoClient();
            db = connect.GetDatabase(database_name);
        }    
        public void InsertDB<T>(string table, T value)
        {
            IMongoCollection<T> collection = db.GetCollection<T>(table);
            collection.InsertOne(value);    
        }

        public List<T> DisplayAll<T>(string table, string sort)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).Sort(new BsonDocument(sort, 1)).ToList();
        }
        public T DisplayOne<T,K>(string table, K id, string nameColumn)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq(nameColumn, id);
            return collection.Find(filter).FirstOrDefault();
        }   
        public void DeleteDB<T>(string table, int id, string nameColumn)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq(nameColumn, id);
            collection.DeleteOne(filter);
        }    
    }
}