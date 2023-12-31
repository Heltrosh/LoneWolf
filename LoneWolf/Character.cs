using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoneWolf
{
    internal class Character
    {
        private static Character _instance = new Character();
        public static Character Instance { get { return _instance; } }
        private int[] disciplines;
        private int combatScore;
        private int endurance;
        private int gold;
        private int[] weaponMasteries;
        private (bool, int) quiver;
        private string[] bag;
        private string[] specialItems;
        private string[] weapons;

        private Character()
        {
            disciplines = new int[3];
            combatScore = 0;
            endurance = 0;
            gold = 0;
            quiver = (false, 0);
            weaponMasteries = new int[3];
            bag = new string[8];
            specialItems = new string[10];
            weapons = new string[2];
        }
        public void createCharacter(string characterString)
        {
            for (int i = 0; i < 3; i++)
                disciplines[i] = int.Parse(characterString[i].ToString());
            combatScore = int.Parse(characterString.Substring(3, 2));
            endurance = int.Parse(characterString.Substring(5, 2));
            gold = int.Parse(characterString.Substring(7, 3));
            quiver = (Convert.ToBoolean(int.Parse(characterString[10].ToString())), int.Parse(characterString[11].ToString()));
            for (int i = 0; i < 3; i++)
                weaponMasteries[i] = characterString[i + 12];
            for (int i = 0; i < 8; i++)
            {
                int itemStart = characterString.IndexOf("BAG" + i) + 4;
                int itemEnd = characterString.IndexOf("BAG" + (i + 1)) -1;
                bag[i] = characterString.Substring(itemStart, itemEnd-itemStart+1);
            }
            for (int i = 0; i < 10; i++)
            {
                int itemStart = characterString.IndexOf("SPEC" + i) + 5;
                int itemEnd = characterString.IndexOf("SPEC" + (i+1)) -1;
                specialItems[i] = characterString.Substring(itemStart, itemEnd - itemStart+1);
            }
            for (int i = 0; i < 2; i++)
            {
                int itemStart = characterString.IndexOf("WEP" + i) + 4;
                int itemEnd = characterString.IndexOf("WEP" + (i + 1)) - 1;
                weapons[i] = characterString.Substring(itemStart, itemEnd - itemStart + 1);
            }
        }

    }
}
