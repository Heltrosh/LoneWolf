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
    /// Interaction logic for CombatLogWindow.xaml
    /// </summary>
    public partial class CombatLogWindow : Window
    {
        CombatLog combatLog;
        int currentCombat;
        public CombatLogWindow()
        {
            InitializeComponent();
            combatLog = CombatLog.Instance;
            currentCombat = combatLog.combats.Count()-1;
        }

        private void windowCBLog_loaded(object sender, RoutedEventArgs e)
        {
            if (currentCombat == -1)
                return;
            foreach (string line in combatLog.combats[currentCombat].lines)
            {
                txtCombatLog.Text += (line + "\n"); 
            }
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (currentCombat <= 0)
                return;
            txtCombatLog.Text = "";
            currentCombat--;
            foreach (string line in combatLog.combats[currentCombat].lines)
            {
                txtCombatLog.Text += (line + "\n");
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentCombat == combatLog.combats.Count - 1)
                return;
            txtCombatLog.Text = "";
            currentCombat++;
            foreach (string line in combatLog.combats[currentCombat].lines)
            {
                txtCombatLog.Text += (line + "\n");
            }
        }
    }
}
