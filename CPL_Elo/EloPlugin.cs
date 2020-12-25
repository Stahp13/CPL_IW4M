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

namespace CPL_Elo
{
    public class EloPlugin : IPlugin
    {
        public string Name => "Elo";
        public float Version => (float)Utilities.GetVersionAsDouble();
        private readonly IMetaService metaService;
        public string Author => "me, myself, with copy & paste";
        EloConfig config;
        UserEloAccessor userEloAccessor;
        DatabaseContext databaseContext;

        public EloPlugin(IConfigurationHandlerFactory configurationHandlerFactory, IDatabaseContextFactory databaseContextFactory, ITranslationLookup translationLookup, IMetaService __metaService) {
            metaService = __metaService;
            config = configurationHandlerFactory.GetConfigurationHandler<EloConfig>("EloPluginSettings").Configuration();
            userEloAccessor = new UserEloAccessor(__metaService);
            databaseContext = databaseContextFactory.CreateContext();
        }

        public string getPlayerRank(int Elo) {
            if (Elo >= config.masterRankThreshold)
                return "menu_div_semipro_64";
            if (Elo >= config.platinumRankThreshold)
                return "menu_div_platinum_64";
            if (Elo >= config.goldRankThreshold)
                return "menu_div_gold_64";
            if (Elo >= config.silverRankThreshold)
                return "menu_div_silver_64";
            if (Elo >= config.bronzeRankThreshold)
                return "menu_div_bronze_64";
            return "menu_div_iron_64";
        }

        public async Task SetEloDvars(Server server) {
            // Sets dvar with information about connected players' Elo
            string EloDvar = "";
            for (int i = 0; i < server.Clients.Count; i++) {
                EFClient client = server.Clients[i];
                if (client == null) {
                    continue;
                }
                int clientElo = userEloAccessor.GetClientElo(client);
                EloDvar += (i > 0 ? "-" : "") + $"{client.NetworkId},{clientElo},{getPlayerRank(clientElo)}";
                Console.WriteLine($"{client.Name} [{client.NetworkId}]: {clientElo}");
            }
            Console.WriteLine("EloDvar: " + EloDvar);
            await server.RconParser.SetDvarAsync(server.RemoteConnection, "clients_Elo", EloDvar);
        }

        public double GetWinProbability(double EloOfTeamA, double EloOfTeamB) {
            return 1.0 / (1.0 + Math.Pow(10, (EloOfTeamB - EloOfTeamA) / 400.0));
        }

        public int CalculateEloChange(Player[] teamA, Player[] teamB, double result) {
            return (int)Math.Round(config.kFactor * (result - GetWinProbability(teamA.Average(player => player.elo), teamB.Average(player => player.elo))));
        }

        private EFClient GetClient(Server server, long player_id) {
            return server.GetClientsAsList().Find(c => c.NetworkId == player_id);
        }

        public async Task OnEventAsync(GameEvent gameEvent, Server server) {
            try {
                switch (gameEvent.Type) {
                    case (GameEvent.EventType.MapChange):
                    case (GameEvent.EventType.Join):
                    case (GameEvent.EventType.PreConnect):
                    case (GameEvent.EventType.Disconnect):
                        await SetEloDvars(server);
                        break;
                    case (GameEvent.EventType.Unknown):
                        Console.WriteLine("recieved: " + gameEvent.Data);
                        string prefix = "RankedGameResult:";
                        if (gameEvent.Data.StartsWith(prefix)) {
                            string results = gameEvent.Data.Substring(prefix.Length);

                            Lobby lobby = Lobby.create(new EFClientFactory(databaseContext), userEloAccessor, results);

                            int axisEloChange = CalculateEloChange(lobby.axis.players, lobby.allies.players, lobby.axis.result);
                            int alliesEloChange = -axisEloChange;

                            foreach (Player player in lobby.axis.players) {
                                player.elo = player.elo + axisEloChange;
                            }

                            foreach (Player player in lobby.allies.players) {
                                player.elo = player.elo + alliesEloChange;
                            }

                            await SetEloDvars(server);
                        }
                        break;
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