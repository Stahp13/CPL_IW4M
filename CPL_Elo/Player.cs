using System;
using System.Collections.Generic;
using System.Text;
using SharedLibraryCore;
using SharedLibraryCore.Interfaces;
using SharedLibraryCore.Database.Models;
using CPL_Elo.json;
using CPL_Elo.database;
using System.Threading.Tasks;

namespace CPL_Elo
{
    public class Player
    {
        private PlayerData data;
        public EloContext database;
        public long userId { get { return data.userId; } }
        public int kills { get { return data.kills; }}
        public int deaths { get { return data.deaths; } }
        public int score { get { return data.score; } }
        public int captures { get { return data.captures; } }
        public int defends { get { return data.defends; } }
        public int plants { get { return data.plants; } }
        public int defuses { get { return data.defuses; } }
        
        private User getUser() {
            var ret = database.Users.Find(userId);
            if (ret == null) {
                throw new Exception($"user: [{userId}] not found in database!");
            }
            return ret;
        }

        virtual public int elo { 
            get { return getUser().elo; } 
            set {
                User u = getUser();
                u.elo = value;
                database.Update(u);
            } 
        }

        virtual public int wins {
            get { return getUser().wins; }
            set {
                User u = getUser();
                u.wins = value;
                database.Update(u);
            }
        }

        virtual public int losses {
            get { return getUser().losses; }
            set {
                User u = getUser();
                u.losses = value;
                database.Update(u);
            }
        }

        virtual public int draws {
            get { return getUser().draws; }
            set {
                User u = getUser();
                u.draws = value;
                database.Update(u);
            }
        }

        public void setAvailable() {
            User u = getUser();
            u.available = true;
            database.Update(u);
        }

        public void addResult(double result) {
            if(result < 0.1) {
                losses = losses + 1;
            } else if (result < 0.6) {
                draws = draws + 1;
            } else {
                wins = wins + 1;
            }
        }

        public Player(EloContext database, PlayerData playerData) {
            data = playerData;
            this.database = database;
        }

        public Player() { }
    }
}
