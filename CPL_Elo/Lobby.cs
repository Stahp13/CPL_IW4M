using System;
using System.Collections.Generic;
using System.Text;
using CPL_Elo.json;
using SharedLibraryCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CPL_Elo
{
    public class Lobby
    {
        public string map { get; }
        public string mode { get; }
        public Team axis { get; }
        public Team allies { get; }
        public Lobby(EFClientFactory factory, UserEloAccessor userEloAccesor, GameData gameData) {
            axis = new Team(factory, userEloAccesor, gameData.axis, "axis");
            allies = new Team(factory, userEloAccesor, gameData.allies, "allies");
            map = gameData.map;
            mode = gameData.mode;
        }

        public static Lobby create(EFClientFactory factory, UserEloAccessor userEloAccesor, string jsonText) {
            return new Lobby(factory, userEloAccesor, JsonSerializer.Deserialize<GameData>(jsonText));
        }
    }
}
