using System;
using System.Collections.Generic;
using System.Text;

namespace CPL_Elo.database
{
    public class User
    {
        public long ID { get; set; }
        public long DID { get; set; }
        public int elo { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public int draws { get; set; }
        public bool available { get; set; }
    }
}
