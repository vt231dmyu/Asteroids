using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsLibrary
{
    public class LeaderboardEntry
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public TimeSpan Time { get; set; }
        public string Wave { get; set; }
    }
}
