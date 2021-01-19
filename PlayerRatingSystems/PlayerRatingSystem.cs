using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerRatingSystems
{
    public interface PlayerRatingSystem
    {
        double GetWinProbability(double EloOfTeamA, double EloOfTeamB);

        int CalculateEloChange(IEnumerable<int> team1Elo, IEnumerable<int> team2Elo, double resultOfTeamA);
    }
}
