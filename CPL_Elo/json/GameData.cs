using System;
using System.Collections.Generic;
using System.Text;

namespace CPL_Elo.json
{
    public class GameData
    {
        public string map { get; set; }
        public string mode { get; set; }
        public TeamData axis { get; set; }
        public TeamData allies { get; set; }
    }
}
