using System;
using System.Collections.Generic;
using System.Text;

namespace CPL_Elo.json
{
    public class TeamData
    {
        public string score { get; set; }
        public double result { get; set; }
        public PlayerData[] players { get; set; }
    }
}
