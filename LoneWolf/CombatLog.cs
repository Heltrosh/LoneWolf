using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoneWolf
{
    internal class CombatLog
    {
        static CombatLog _instance = new CombatLog();
        public List<Combat> combats { get; } = new();
        
        public static CombatLog Instance { get { return _instance; } }
        public void addCombat(int combatRatio, int enemyEndurance, int loneWolfEndurance)
        {
            combats.Add(new Combat(combatRatio, enemyEndurance, loneWolfEndurance, combats.Count()+1));
        }
        public void addLine(int roll, Tuple<int, int> damage)
        {
            Combat currentCombat = combats.Last();
            currentCombat.enemyEndurance -= damage.Item1;
            currentCombat.loneWolfEndurance -= damage.Item2;
            string line = "Roll: " + roll + ", Enemy Endurance: " + currentCombat.enemyEndurance + ", Lone Wolf Endurance: " + currentCombat.loneWolfEndurance;
            currentCombat.addLine(line);
        }
        public void addLine(string line)
        {
            Combat currentCombat = combats.Last();
            currentCombat.addLine(line);
        }
    }
    internal class Combat
    {
        public List<string> lines { get; } = new();
        public int combatRatio, enemyEndurance, loneWolfEndurance;
        public Combat(int combatRatio, int enemyEndurance, int loneWolfEndurance, int id)
        {
            this.combatRatio = combatRatio;
            this.enemyEndurance = enemyEndurance;
            this.loneWolfEndurance = loneWolfEndurance;
            lines.Add("Combat #" + id);
            lines.Add("Combat Ratio: " + combatRatio + ", Enemy Endurance: " + enemyEndurance + ", Lone Wolf Endurance: " + loneWolfEndurance);
        }
        public void addLine(string line) 
        {  
            lines.Add(line);
        }
    }
}
