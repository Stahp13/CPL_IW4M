using System;
using System.Collections.Generic;
using System.Text;
using CPL_Elo.json;
using SharedLibraryCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using CPL_Elo.database;

namespace CPL_Elo
{
    public class Lobby
    {
        public string map { get; }
        public string mode { get; }
        public Team axis { get; }
        public Team allies { get; }
        public Lobby(EloContext eloContext, GameData gameData) {
            axis = new Team(eloContext, gameData.axis, "axis");
            allies = new Team(eloContext, gameData.allies, "allies");
            map = gameData.map;
            mode = gameData.mode;
        }

        public static Lobby create(EloContext eloContext, string jsonText) {
            return new Lobby(eloContext, JsonSerializer.Deserialize<GameData>(jsonText));
        }
    }
}
