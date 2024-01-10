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
            string[] disciplines = { txtDiscipline1.Text, txtDiscipline2.Text, txtDiscipline3.Text };
            int combatScore = int.Parse(txtCS.Text); // TBD
            int endurance = int.Parse(txtEND.Text); //TBD
            int gold = int.Parse(txtGold.Text);
            (bool, int) quiver = (chckQuiverAvail.IsChecked ?? false , int.Parse(txtQuiverArrows.Text));
            string[] weaponMasteries = { "", "", "" };
            if (disciplines.Contains("Weaponmastery"))
                weaponMasteries = [txtWeaponMastery1.Text, txtWeaponMastery2.Text, txtWeaponMastery3.Text];
            TextBox[] bagTextBoxes = { txtBag1, txtBag2, txtBag3, txtBag4, txtBag5, txtBag6, txtBag7, txtBag8 };
            string[] bag = bagTextBoxes
                .Where(textBox => !string.IsNullOrWhiteSpace(textBox.Text))
                .Select(textBox => textBox.Text)
                .ToArray();
            string[] specialItems = txtSpecialItems.Text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();
            TextBox[] weaponTextBoxes = { txtWeapon1, txtWeapon2 };
            string[] weapons = weaponTextBoxes
                .Where(textBox => !string.IsNullOrWhiteSpace(textBox.Text))
                .Select(textBox => textBox.Text)
                .ToArray();
            character.updateCharacter(disciplines, combatScore, endurance, gold, quiver, weaponMasteries, bag, specialItems, weapons);
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
            txtCS.Text = character.combatScore.ToString();
            txtEND.Text = character.endurance.ToString();
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
            }
        }
    }
}
