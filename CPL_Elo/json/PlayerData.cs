using System;
using System.Collections.Generic;
using System.Text;

namespace CPL_Elo.json
{
    public class PlayerData
    {
        public long userId { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int score { get; set; }
        public int captures { get; set; }
        public int defends { get; set; }
        public int plants { get; set; }
        public int defuses { get; set; }
    }
}
