using System;
using System.Collections.Generic;
using System.Text;
using CPL_Elo.json;
using SharedLibraryCore;
using CPL_Elo.database;

namespace CPL_Elo
{
    public class Team
    {
        public string score { get; }
        public string name { get; }
        public double result { get; }
        public Player[] players { get; }
        public Team(EloContext eloContext, TeamData teamData, string _name) {
            name = _name;
            score = teamData.score;
            result = teamData.result;
            players = new Player[teamData.players.Length];
            for (int i = 0; i < teamData.players.Length; ++i) {
                players[i] = new Player(eloContext, teamData.players[i]);
            }
        }
    }
}
