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
            string characterString = "";

            string[] disciplines = getSelectedDisciplines();
            Dictionary<string, int> magnakaiDisciplines = Consts.magnakaiDisciplines.ToDictionary((i) => i.Value, (i) => i.Key);
            foreach (string discipline in disciplines)
                characterString += (magnakaiDisciplines[discipline]);
        }
    }
}
