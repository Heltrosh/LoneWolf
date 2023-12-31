using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

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
        private (bool, int) quiver;
        private int[] weaponMasteries;
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
        public void loadCharacter(string characterString)
        {
            for (int i = 0; i < 3; i++)
                disciplines[i] = int.Parse(characterString[i].ToString());
            combatScore = int.Parse(characterString.Substring(3, 2));
            endurance = int.Parse(characterString.Substring(5, 2));
            gold = int.Parse(characterString.Substring(7, 3));
            quiver = (Convert.ToBoolean(int.Parse(characterString[10].ToString())), int.Parse(characterString.Substring(11, 2)));
            if (disciplines.Contains(0))
                for (int i = 0; i < 3; i++)
                    weaponMasteries[i] = int.Parse(characterString[i + 13].ToString());
            else
                for (int i = 0; i < 3; i++)
                    weaponMasteries[i] = -1;

            for (int i = 0; i < 8; i++)
            {
                int itemStart = characterString.IndexOf("BAG" + i) + 4;
                int itemEnd = characterString.IndexOf("BAG" + (i + 1)) - 1;
                bag[i] = characterString.Substring(itemStart, itemEnd - itemStart + 1);
            }
            for (int i = 0; i < 10; i++)
            {
                int itemStart = characterString.IndexOf("SPEC" + i) + 5;
                int itemEnd = characterString.IndexOf("SPEC" + (i + 1)) - 1;
                specialItems[i] = characterString.Substring(itemStart, itemEnd - itemStart + 1);
            }
            for (int i = 0; i < 2; i++)
            {
                int itemStart = characterString.IndexOf("WEP" + i) + 4;
                int itemEnd = characterString.IndexOf("WEP" + (i + 1)) - 1;
                weapons[i] = characterString.Substring(itemStart, itemEnd - itemStart + 1);
            }
        }
        public void loadCharacter()
        {
            string? characterString;
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            using (StreamReader sr = new StreamReader(Path.Combine(appData, "LoneWolf\\character.txt")))
                characterString = sr.ReadLine();
            if (characterString != null)
                loadCharacter(characterString);
        }

        public void updateCharacter(string[] disciplines, int combatScore, int endurance, int gold, (bool, int) quiver,
                                        string[] weaponMasteries, string[] bag, string[] specialItems, string[] weapons)
        {
            Dictionary<string, int> magnakaiDisciplines = Consts.magnakaiDisciplines.ToDictionary((i) => i.Value, (i) => i.Key);
            for (int i = 0; i < 3; i++)
                this.disciplines[i] = magnakaiDisciplines[disciplines[i]];
            this.combatScore = combatScore;
            this.endurance = endurance;
            this.gold = gold;
            this.quiver = quiver;
            Dictionary<string, int> weaponMasteriesDict = Consts.weaponMasteries.ToDictionary((i) => i.Value, (i) => i.Key);
            weaponMasteriesDict.Add("", -1);
            for (int i = 0; i < 3; i++)
                this.weaponMasteries[i] = weaponMasteriesDict[weaponMasteries[i]];
            Array.Copy(bag, this.bag, bag.Length);
            for (int i = bag.Length; i < this.bag.Length; i++)
                this.bag[i] = "Empty";
            Array.Copy(specialItems, this.specialItems, specialItems.Length);
            for (int i = specialItems.Length; i < this.specialItems.Length; i++)
                this.specialItems[i] = "Empty";
            Array.Copy(weapons, this.weapons, weapons.Length);
            for (int i = weapons.Length; i < this.weapons.Length; i++)
                this.weapons[i] = "Empty";
        }
        public void saveCharacter()
        {
            string characterString = "";
            foreach (int discipline in disciplines)
                characterString += discipline.ToString();
            characterString += combatScore.ToString("00");
            characterString += endurance.ToString("00");
            characterString += gold.ToString("000");
            characterString += quiver.Item1 ? "1" : "0";
            characterString += quiver.Item2.ToString("00");
            foreach (int mastery in weaponMasteries)
                characterString += mastery == -1 ? "F" : mastery.ToString();
            for (int i = 0;  i < 8; i++)
            {
                characterString += "BAG" + i + bag[i];
                if (i == 7)
                    characterString += "BAG8";
            }
            for (int i = 0; i < 10; i++)
            {
                characterString += "SPEC" + i + specialItems[i];
                if (i == 9)
                    characterString += "SPEC10";
            }
            for (int i = 0; i < 2; i++)
            {
                characterString += "WEP" + i + weapons[i];
                if (i == 1)
                    characterString += "WEP2";
            }
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            System.IO.Directory.CreateDirectory(Path.Combine(appData, "LoneWolf"));
            using (StreamWriter sw = new StreamWriter(Path.Combine(appData, "LoneWolf\\character.txt"), false))
                sw.WriteLine(characterString);
        }

    }
}
