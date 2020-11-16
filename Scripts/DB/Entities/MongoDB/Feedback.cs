using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Scripts.DB.Entities.MongoDB {

    public class Feedback {

        public Feedback (MongoDBHelper<Feedback> handler) {
            Handler = handler;
        }

        public string Type { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        MongoDBHelper<Feedback> Handler { get; }

        public async Task Insert (string type, string email, string title, string message) {
            var feedback = new Feedback(Handler) { 
                Type = type,
                Email = email,
                Title = title,
                Message = message
            };
            await Handler.InsertOneAsync(feedback);
        }

    }
}
