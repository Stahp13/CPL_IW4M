using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerRatingCharterApp
{
    class Player
    {
        public int gamesPlayed { get; set; }
        public double skill { get; set; }
        public int rating { get; set; }
        public int gamesWon { get; set; }
        public int gamesLost { get; set; }
        public int unlikelyVictories { get; set; }
        public int unlikelyLosses{ get; set; }

        public Player() { }
        public Player(double skill) {
            gamesPlayed = 0;
            this.skill = skill;
            rating = 1000;
            gamesWon = 0;
            gamesLost = 0;
            unlikelyLosses = 0;
            unlikelyVictories = 0;
        }

        public object this[string propertyName] {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { }
        }
    }
}
