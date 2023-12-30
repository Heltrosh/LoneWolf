using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoneWolf
{
    internal class CombatTable
    {
        private Dictionary<int, Dictionary<int, Tuple<int, int>>> combatTable;
        public CombatTable()
        {
            combatTable = new Dictionary<int, Dictionary<int, Tuple<int, int>>>();
            for (int i = 0; i < 10; i++)
            {
                combatTable.Add(i, []);
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = -11; j < 12; j++)
                {
                    int[] colmap = [0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12];
                    (int, int) combatTableValue = Consts.combatTableValues[13 * i + colmap[j + 11]];
                    combatTable[i][j] = Tuple.Create(combatTableValue.Item1, combatTableValue.Item2);
                }
            }
        }
        public Tuple<int, int> getCombatTableValue(int row, int col)
        {
            if (col < -11)
                col = -11;
            else if (col > 11)
                col = 11;
            return combatTable[row][col];
        }
    }
}
