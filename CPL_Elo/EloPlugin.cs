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

namespace CPL_Elo
{
    public class EloPlugin : IPlugin
    {
        public string Name => "Elo";

        public float Version => (float)Utilities.GetVersionAsDouble();

        private readonly IMetaService metaService;

        public string Author => "me, myself, with copy & paste";

        double k_factor;

        public EloPlugin(IConfigurationHandlerFactory configurationHandlerFactory, IDatabaseContextFactory databaseContextFactory, ITranslationLookup translationLookup, IMetaService __metaService) {
            metaService = __metaService;
            k_factor = configurationHandlerFactory.GetConfigurationHandler<EloConfig>("EloPluginSettings").Configuration().kFactor;
        }

        public async Task<int> GetClientElo(EFClient C) {
            EFMeta Elo_meta = await metaService.GetPersistentMeta("Elo", C);
            if (Elo_meta == null) {
                await metaService.AddPersistentMeta("Elo", "1000", C);
                return 1000;
            }
            return Convert.ToInt32(Elo_meta.Value);
        }

        public async Task SetElo(EFClient C, int Elo) {
            await metaService.AddPersistentMeta("Elo", Elo.ToString(), C);
        }

        public async Task SetEloDvars(Server S) {
            // Sets dvar with information about connected players' Elo
            string EloDvar = "";
            for (int i = 0; i < S.Clients.Count; i++) {
                if (S.Clients[i] == null) {
                    continue;
                }
                EloDvar += (i > 0 ? "-" : "") + $"{S.Clients[i].NetworkId},{await GetClientElo(S.Clients[i])}";
            }
            Console.WriteLine("EloDvar: " + EloDvar);
            await S.RconParser.SetDvarAsync(S.RemoteConnection, "clients_Elo", EloDvar);
        }

        public double GetWinProbability(int EloOfTeamA, int EloOfTeamB) {
            return 1.0 / (1.0 + Math.Pow(10, (double)(EloOfTeamB - EloOfTeamA) / 400.0));
        }

        public int CalculateEloChange(List<int> winners, List<int> losers) {
            return (int)Math.Round(k_factor * (1.0 - GetWinProbability(winners.Sum() / 4, losers.Sum() / 4)));
        }

        private async Task ApplyEloChange(Server server, EFClient client, int client_elo, int Elo_change) {
            await SetElo(client, client_elo + Elo_change);
        }

        private EFClient GetClient(Server server, long player_id) {
            return server.GetClientsAsList().Find(c => c.NetworkId == player_id);
        }

        public async Task OnEvent(GameEvent E, Server S) {
            switch (E.Type) {
                case (GameEvent.EventType.PreConnect):
                case (GameEvent.EventType.Join):
                case (GameEvent.EventType.MapChange):
                case (GameEvent.EventType.Disconnect):
                    _ = SetEloDvars(S);
                    break;
                case (GameEvent.EventType.Unknown):
                    Console.WriteLine("recieved: " + E.Data);
                    string prefix = "RankedGameResult:";
                    if (E.Data.StartsWith(prefix)) {
                        string results = E.Data.Substring(prefix.Length);
                        results = results.Substring(0, results.Length - 1); // removing last semicolon
                        string[] player_list = results.Split(";");
                        foreach (string player in player_list) {
                            Console.WriteLine("player: " + player);
                        }
                        int player_count = player_list.Length;
                        if (player_count % 2 == 1) {
                            Console.WriteLine("odd number of players");
                            break;
                        }
                        int team_size = player_count / 2;

                        List<EFClient> winners = new List<EFClient>();
                        List<int> winners_Elo = new List<int>();
                        List<EFClient> losers = new List<EFClient>();
                        List<int> losers_elo = new List<int>();
                        for (int i = 0; i < team_size; i++) {
                            long winning_player_id = Int64.Parse(player_list[i]);
                            EFClient winner = GetClient(S, winning_player_id);
                            winners.Add(winner);
                            winners_Elo.Add(await GetClientElo(winner));
                            long losing_player_id = Int64.Parse(player_list[i + team_size]);
                            EFClient loser = GetClient(S, losing_player_id);
                            losers.Add(loser);
                            losers_elo.Add(await GetClientElo(loser));
                        }

                        int Elo_change = CalculateEloChange(winners_Elo, losers_elo);
                        for (int i = 0; i < team_size; i++) {
                            EFClient winner = winners[i];
                            int winner_Elo = winners_Elo[i];
                            await ApplyEloChange(S, winner, winner_Elo, Elo_change);
                            EFClient loser = losers[i];
                            int loser_Elo = losers_elo[i];
                            await ApplyEloChange(S, loser, loser_Elo, -1 * Elo_change);
                        }
                        _ = SetEloDvars(S);
                    }
                    break;
                case (GameEvent.EventType.MapEnd):
                    break;
            }
        }

        public Task OnEventAsync(GameEvent E, Server S) {
            _ = OnEvent(E, S);
            return Task.CompletedTask;
        }

        public async Task OnLoadAsync(IManager manager) {
            Console.WriteLine($"Elo loaded ({Author})");
        }

        public Task OnTickAsync(Server S) {
            throw new NotImplementedException();
        }

        public Task OnUnloadAsync() {
            return Task.CompletedTask;
        }
    }
}