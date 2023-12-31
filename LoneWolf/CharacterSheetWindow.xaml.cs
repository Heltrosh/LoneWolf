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
            //add logic
            txtBag1.Text = "Hello World";
        }

        private void btnSaveCharacter_Click(object sender, RoutedEventArgs e)
        {
            character.saveCharacter();
        }

        private void btnLoadCharacter_Click(object sender, RoutedEventArgs e)
        {
            character.loadCharacter();
            //add logic
        }
    }
}
