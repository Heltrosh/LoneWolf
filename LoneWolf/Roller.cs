using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoneWolf
{
    class Roller
    {
        public int getRoll()
        {
            int[] values = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
            int[] weights = [10, 10, 11, 11, 12, 12, 11, 11, 10, 10];

            int totalWeight = weights.Sum();
            int row = 0;
            int col = 0;
            for (int i = 0; i < 2; i++)
            {
                int randomNumber = new Random().Next(1, totalWeight + 1);
                int cumulativeWeight = 0;
                for (int j = 0; j < values.Length; j++)
                {
                    cumulativeWeight += weights[j];
                    if (randomNumber <= cumulativeWeight)
                    {
                        if (i == 0)
                        { row = values[j]; }
                        else
                        { col = values[j]; }
                        break;
                    }
                }
            }
            return Consts.rollTable[row, col];
        }
    }
}
