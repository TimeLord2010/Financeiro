using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Scripts.DB.Entities.MongoDB {

    public class Users {

        public Users(MongoDBHelper<Users> handler) {
            DbHandler = handler;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActivity { get; set; }

        MongoDBHelper<Users> DbHandler { get; }

        public async Task<bool> Login(string user_name, string password) {
            var filter_builder = Builders<Users>.Filter;
            var filter = filter_builder.And(Builders<Users>.Filter.Eq("Username", user_name), Builders<Users>.Filter.Eq("Password", password));
            var update_builder = Builders<Users>.Update;
            var update = update_builder.Set("LastActivity", DateTime.UtcNow);
            var user = await DbHandler.FindOneAndUpdateAsync(filter, update);
            return user != null;
        }

        /// <summary>
        /// Used in register.
        /// </summary>
        /// <param name="user_name"></param>
        /// <returns></returns>
        public async Task<bool> UserExists (string user_name) {
            var filter_builder = Builders<Users>.Filter;
            var filter = filter_builder.Eq("Username", user_name);
            var user = await DbHandler.FindOneAsync(filter);
            return user != null;
        }

    }
}
