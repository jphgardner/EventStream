using System;
using System.Collections.Generic;
using System.Linq;
using EventStream.Domain;
using EventStream.Domain.Models;
using EventStream.Server.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace EventStream.Server.Repositories
{
    public class StreamDatabase
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        public readonly IMongoCollection<Client> Clients;
        public readonly IMongoCollection<User> Users;
        public readonly List<StreamCollection> Streams = new();

        public StreamDatabase(IOptions<MongoOptions> dbOptions)
        {
            _mongoClient = new MongoClient(dbOptions.Value.ConnectionString);
            _database = _mongoClient.GetDatabase(dbOptions.Value.Database);

            Clients = _database.GetCollection<Client>("Clients");
            Users = _database.GetCollection<User>("Users");
            
            Setup();
        }

        private async void Setup()
        {
            //Create Admin User
            var adminUser = await Users.Find(user => user.Username == "admin").FirstOrDefaultAsync();
            if (adminUser == null)
            {
                adminUser = new User("admin", "admin");
                await Users.InsertOneAsync(adminUser);
                Console.WriteLine($"'admin' user created");
            }
            
            //Load StreamCollections into server memory 
            var streamCollections = await _database.ListCollectionNamesAsync();
            var collections = streamCollections.ToList().Where(s => s != "Clients" && s != "Users" && s != "Metrics");

            foreach (var collection in collections)
            {
                Console.WriteLine($"collection: {collection}");
            }
            
            Console.WriteLine($"collection: {collections.Count()}");
        }

        public StreamCollection GetStream(string name)
        {
            var findStream = Streams.Find(collection => collection.Name == name);
            if (findStream == null)
            {
                findStream = new StreamCollection(name, _database.GetCollection<Event>($"Stream_{name}"));
                Streams.Add(findStream);
            }
            return findStream;
        }
    }
}