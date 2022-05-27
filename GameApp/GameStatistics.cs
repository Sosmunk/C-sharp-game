using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Text
{
    public class GameStatistics
    {
        public int KillCount { get; private set; }

        public GameStatistics()
        {
            KillCount = 0;
        }
        public void AddKill()
        {
            KillCount++;
        }
        public void ResetKillCount()
        {
            KillCount = 0;
        }
    }
}
