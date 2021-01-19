using SharedLibraryCore;
using SharedLibraryCore.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using SharedLibraryCore.Configuration;
using System.Xml;
using System.Collections.Generic;
using SharedLibraryCore.Helpers;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Linq;
using SharedLibraryCore.Database.Models;
using SharedLibraryCore.Database;
using CPL_Elo.database;
using PlayerRatingSystems;

namespace CPL_Elo
{
    public class EloPlugin : IPlugin
    {
        public class Prefix
        {
            public static string RankGameEnded = "RankedGameResult:";
            public static string RankGameAborted = "RankedGameAborted:";
        }

        public string Name => "Elo";
        public float Version => (float)Utilities.GetVersionAsDouble();
        public string Author => "me, myself, with copy & paste";
        public PlayerRatingSystem ratingSystem;
        

        public EloPlugin(IConfigurationHandlerFactory configurationHandlerFactory, IDatabaseContextFactory databaseContextFactory, ITranslationLookup translationLookup, IMetaService __metaService) {
            EloConfig config = configurationHandlerFactory.GetConfigurationHandler<EloConfig>("EloPluginSettings").Configuration();
            ratingSystem = new EloRatingSystem(config.kFactor);
        }

        public void OnRankedGameEnded(GameEvent gameEvent) {
            EloContext eloContext = new EloContext();
            eloContext.Database.EnsureCreated();
            string results = gameEvent.Data.Substring(Prefix.RankGameEnded.Length);

            Lobby lobby = Lobby.create(eloContext, results);

            int axisEloChange = ratingSystem.CalculateEloChange(lobby.axis.players.Select(p => p.elo), lobby.allies.players.Select(p => p.elo), lobby.axis.result);
            int alliesEloChange = -axisEloChange;

            foreach (Player player in lobby.axis.players) {
                player.elo = player.elo + axisEloChange;
                player.addResult(lobby.axis.result);
                player.setAvailable();
            }

            foreach (Player player in lobby.allies.players) {
                player.elo = player.elo + alliesEloChange;
                player.addResult(lobby.allies.result);
                player.setAvailable();
            }
            eloContext.SaveChanges();
        }

        public void OnRankedGameAborted(GameEvent gameEvent) {
            EloContext eloContext = new EloContext();
            string[] playerIds = gameEvent.Data.Substring(Prefix.RankGameAborted.Length).Split(";"); ;
            foreach (string playerId in playerIds) {
                User user = eloContext.Users.Find(Convert.ToInt64(playerId));
                if (user == null) {
                    throw new Exception($"user: [{playerId}] not found in database!");
                }
                user.available = true;
                eloContext.Update(user);
            }
            eloContext.SaveChanges();
        }

        public async Task OnEventAsync(GameEvent gameEvent, Server server) {
            try {
                switch (gameEvent.Type) {
                    case (GameEvent.EventType.Unknown):
                        Console.WriteLine("recieved: " + gameEvent.Data);
                        if (gameEvent.Data.StartsWith(Prefix.RankGameEnded)) {
                            OnRankedGameEnded(gameEvent);
                        } else if (gameEvent.Data.StartsWith(Prefix.RankGameAborted)) {
                            OnRankedGameAborted(gameEvent);
                        }
                        break;
                    case (GameEvent.EventType.MapChange):
                    case (GameEvent.EventType.Join):
                    case (GameEvent.EventType.PreConnect):
                    case (GameEvent.EventType.Disconnect):
                    case (GameEvent.EventType.MapEnd):
                        break;
                }
            } catch(Exception e) {
                Console.WriteLine("Elo plugin unhandled exception: " + e.ToString());
            }
        }

        public Task OnLoadAsync(IManager manager) {
            Console.WriteLine($"Elo loaded ({Author})");
            return Task.CompletedTask;
        }

        public Task OnTickAsync(Server server) => Task.CompletedTask;

        public Task OnUnloadAsync() => Task.CompletedTask;
    }
}