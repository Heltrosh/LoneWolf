using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for CharacterSheetWindow.xaml
    /// </summary>
    public partial class CharacterSheetWindow : Window
    {
        Character character;
        public CharacterSheetWindow()
        {
            InitializeComponent();
            character = Character.Instance;
        }

        private void btnNewCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterCreationWindow characterCreationWindow = new CharacterCreationWindow();
            characterCreationWindow.Closed += characterCreationWindow_Closed;
            characterCreationWindow.Show();
        }
        private void characterCreationWindow_Closed(object? sender, EventArgs e)
        {
            cleanCharacterSheet();
            displayCharacter();
        }

        private void btnSaveCharacter_Click(object sender, RoutedEventArgs e)
        {
            character.saveCharacter();
        }

        private void btnLoadCharacter_Click(object sender, RoutedEventArgs e)
        {
            cleanCharacterSheet();
            character.loadCharacter();
            displayCharacter();
        }
        private void cleanCharacterSheet()
        {
            foreach (Control ctrl in gridMain.Children)
            {
                if (ctrl.GetType() == typeof(TextBox))
                    ((TextBox)ctrl).Text = String.Empty;
                if (ctrl.GetType() == typeof(CheckBox))
                    ((CheckBox)ctrl).IsChecked = false;
            }
        }

        private void displayCharacter()
        {
            for (int i = 0; i < 3; i++)
            {
                TextBox disciplineTextBox = (TextBox)this.FindName($"txtDiscipline{i + 1}");
                disciplineTextBox.Text = Consts.magnakaiDisciplines[character.disciplines[i]];
            }
            if (character.disciplines.Contains(0))
            {
                for (int i = 0; i < 3; i++)
                {
                    TextBox weaponMasteryTextBox = (TextBox)this.FindName($"txtWeaponMastery{i + 1}");
                    weaponMasteryTextBox.Text = Consts.weaponMasteries[character.weaponMasteries[i]];
                }
            }
            txtBonusCS.Text = character.bonuses.Item1.ToString();
            txtBonusEND.Text = character.bonuses.Item2.ToString();
            txtCS.Text = (character.combatScore + character.bonuses.Item1).ToString();
            txtEND.Text = (character.endurance + character.bonuses.Item2).ToString();
            txtWeapon1.Text = character.weapons[0] != "Empty" ? character.weapons[0] : "";
            txtWeapon2.Text = character.weapons[1] != "Empty" ? character.weapons[1] : "";
            chckQuiverAvail.IsChecked = character.quiver.Item1;
            txtQuiverArrows.Text = character.quiver.Item2.ToString();
            for (int i = 0; i < 8; i++)
            {
                TextBox bagTextBox = (TextBox)this.FindName($"txtBag{i + 1}");
                bagTextBox.Text = character.bag[i] != "Empty" ? character.bag[i] : "";
            }
            txtGold.Text = character.gold.ToString();
            for (int i = 0; i < 10; i++)
            {
                txtSpecialItems.Text += character.specialItems[i] != "Empty" ? character.specialItems[i] + "\n" : "";
                if (character.specialItems[i].Contains('+'))
                {
                    int bonusIndex = character.specialItems[i].IndexOf('+') + 1;
                    if (character.specialItems[i].Contains("END"))
                        txtEND.Text = (int.Parse(txtEND.Text) + int.Parse(character.specialItems[i][bonusIndex].ToString())).ToString();
                    else
                        txtCS.Text = (int.Parse(txtCS.Text) + int.Parse(character.specialItems[i][bonusIndex].ToString())).ToString();
                }
            }
        }
    }
}
