using System;
using System.Collections.Generic;
using System.Text;
using SharedLibraryCore;
using SharedLibraryCore.Interfaces;
using SharedLibraryCore.Database.Models;
using CPL_Elo.json;
using System.Threading.Tasks;

namespace CPL_Elo
{
    public class Player
    {
        private PlayerData data;
        private UserEloAccessor userEloAccessor;
        public EFClient client { get; }
        public long userId { get { return data.userId; } }
        public int kills { get { return data.kills; }}
        public int deaths { get { return data.deaths; } }
        public int score { get { return data.score; } }
        public int captures { get { return data.captures; } }
        public int defends { get { return data.defends; } }
        public int plants { get { return data.plants; } }
        public int defuses { get { return data.defuses; } }
        private int cachedElo;
        virtual public int elo { 
            get { return cachedElo; } 
            set {
                Console.WriteLine($"{client.Name} [{client.NetworkId}]: setting Elo: {cachedElo} => {value}");
                cachedElo = value;
                userEloAccessor.SetElo(client, value);
            } 
        }
        public Player(EFClientFactory factory, UserEloAccessor _userEloAccesor, PlayerData playerData) {
            data = playerData;
            client = factory.getClient(playerData.userId);
            userEloAccessor = _userEloAccesor;
            cachedElo = userEloAccessor.GetClientElo(client);
        }

        public Player() { }
    }
}
