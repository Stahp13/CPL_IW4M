using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PlayerRatingSystems
{
    public class EloRatingSystem : PlayerRatingSystem
    {
        int kFactor { get; set; }
        public EloRatingSystem(int kFactor) {
            this.kFactor = kFactor;
        }
        public double GetWinProbability(double team1Elo, double team2Elo) {
            return 1.0 / (1.0 + Math.Pow(10, (team2Elo - team1Elo) / 400.0));
        }

        public int CalculateEloChange(IEnumerable<int> team1Elo, IEnumerable<int> team2Elo, double resultOfTeam1) {
            return (int)Math.Round(kFactor * (resultOfTeam1 - GetWinProbability(team1Elo.Average(), team2Elo.Average())));
        }
    }
}
