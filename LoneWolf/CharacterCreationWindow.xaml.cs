using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoneWolf
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CharacterCreationWindow : Window
    {
        Character character;
        public CharacterCreationWindow()
        {
            InitializeComponent();
            character = Character.Instance;
        }
        private ComboBox? currentWeaponMastery;

        private void disciplineChanged(ComboBox cmbDiscipline)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)cmbDiscipline.SelectedItem;
            string? selectedString = selectedItem.Content.ToString();
            if (selectedString is not null && selectedString == "Weaponmastery")
            {
                cmbWeaponMastery1.IsEnabled = true;
                cmbWeaponMastery2.IsEnabled = true;
                cmbWeaponMastery3.IsEnabled = true;
                currentWeaponMastery = cmbDiscipline;
            }
            else if (cmbDiscipline == currentWeaponMastery && selectedString != "WeaponMastery")
            {
                cmbWeaponMastery1.IsEnabled = false;
                cmbWeaponMastery1.SelectedIndex = -1;
                cmbWeaponMastery2.IsEnabled = false;
                cmbWeaponMastery2.SelectedIndex = -1;
                cmbWeaponMastery3.IsEnabled = false;
                cmbWeaponMastery3.SelectedIndex = -1;
                currentWeaponMastery = null;
            }
        }
        private void cmbDiscipline1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            disciplineChanged(cmbDiscipline1);
        }

        private void cmbDiscipline2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            disciplineChanged(cmbDiscipline2);
        }

        private void cmbDiscipline3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            disciplineChanged(cmbDiscipline3);
        }

        private void btnStartingRoll_Click(object sender, RoutedEventArgs e)
        {
            Roller roller = new();
            txtStartingCS.Text = (roller.getRoll() + 10).ToString();
            txtStartingEND.Text = (roller.getRoll() + 20).ToString();
            txtStartingGold.Text = (roller.getRoll() + 10).ToString();
        }
        
        private string[] getSelectedDisciplines()
        {
            string[] disciplines = new string[3];
            ComboBoxItem selectedDiscipline1 = (ComboBoxItem)cmbDiscipline1.SelectedItem;
            ComboBoxItem selectedDiscipline2 = (ComboBoxItem)cmbDiscipline2.SelectedItem;
            ComboBoxItem selectedDiscipline3 = (ComboBoxItem)cmbDiscipline3.SelectedItem;
            disciplines[0] = selectedDiscipline1.Content?.ToString() ?? "0";
            disciplines[1] = selectedDiscipline2.Content?.ToString() ?? "0";
            disciplines[2] = selectedDiscipline3.Content?.ToString() ?? "0";
            return disciplines;
        }
        private void btnCreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            string[] disciplines = getSelectedDisciplines();
            int combatScore = int.Parse(txtStartingCS.Text);
            int endurance = int.Parse(txtStartingEND.Text);
            int gold = int.Parse(txtStartingGold.Text);
            string[] startingItems = new string[5];
            for (int i = 1; i < 6; i++)
            {
                ComboBox comboBox = (ComboBox)this.FindName($"cmbStartingItem{i}");
                ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
                startingItems[i - 1] = selectedItem.Content?.ToString() ?? "";
            }
            (bool, int) quiver = startingItems.Contains("Quiver") ? (true, 6) : (false, 0);
            string[] weaponMasteries = {"", "", ""};
            if (disciplines.Contains("Weaponmastery"))
            {
                for (int i = 1; i < 4; i++)
                {
                    ComboBox comboBox = (ComboBox)this.FindName($"cmbWeaponMastery{i}");
                    ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
                    weaponMasteries[i - 1] = selectedItem.Content?.ToString() ?? "";
                }
            }
            string[] startingBagItems = {"Potion of Laumspur", "Tinderbox", "Rope", "4 Special Rations"};
            string[] bag = startingBagItems
            .Where(searchItem => startingItems.Contains(searchItem))
            .SelectMany(matchedItem => Enumerable.Repeat(matchedItem, matchedItem == "4 Special Rations" ? 4 : 1))
            .ToArray();
            string[] specialItems = startingItems.Contains("Padded Leather Waistcoat") ? new[] {"Padded Leather Waistcoat +2END"} : new[] {""};
            string[] startingWeapons = {"Quarterstaff", "Dagger", "Bow", "Sword", "Axe", "Warhammer"};
            string[] weapons = startingItems.Intersect(startingWeapons).ToArray();
            character.updateCharacter(disciplines, combatScore, endurance, gold, quiver, weaponMasteries, bag, specialItems, weapons);
            this.Close();
        }
    }
}
