using Financeiro.Scripts.DB.Entities.MongoDB;
using Financeiro.Scripts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Scripts.DB.MongoDB {

    public class FinanceiroMongoDB {

        public FinanceiroMongoDB () {
            var users_handler = new MongoDBHelper<Users>(cluster,user,password,database,"Users");
            Users = new Users(users_handler);
            var feedback_handler = new MongoDBHelper<Feedback>(cluster, user, password, database, "Feedback");
            Feedback = new Feedback(feedback_handler);
        }

        DateTime LastActivity { get; set; }

        Users users;
        public Users Users {
            get {
                ValidateSession();
                return users;
            }
            private set => users = value;
        }

        public Feedback Feedback { get; }

        void ValidateSession () {
            if (LastActivity == DateTime.MinValue) 
                throw new NotLoggedIn("Usuário não logado.");
            if (DateTime.Now - LastActivity > TimeSpan.FromMinutes(20))
                throw new SessionTimeout("Sessão expirada.");
        }

    }
}
