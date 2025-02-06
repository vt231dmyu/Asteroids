using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidsLibrary
{
    public class GameLeaderboard
    {
        public List<LeaderboardEntry> Entries { get; private set; }

        public GameLeaderboard()
        {
            Entries = new List<LeaderboardEntry>();
        }

        public void LoadEntries()
        {
            var lines = File.ReadAllLines("leaderboard.txt");

            Entries = lines.Select(line => line.Split(','))
                          .Select(parts => new LeaderboardEntry
                          {
                              Name = parts[0],
                              Score = int.Parse(parts[1]),
                              Time = TimeSpan.Parse(parts[2]),
                              Wave = parts[3]
                          })
                          .OrderByDescending(entry => entry.Score)
                          .ToList();

            for (int i = 0; i < Entries.Count; i++)
            {
                Entries[i].Number = i + 1;
            }
        }

    }
}
