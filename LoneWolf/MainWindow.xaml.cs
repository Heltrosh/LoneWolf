using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoneWolf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CombatTable combatTable;
        CombatLog combatLog;
        public MainWindow()
        {
            InitializeComponent();
            combatTable = new();
            combatLog = CombatLog.Instance;
        }

        private int getRoll()
        {
            int[] values = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
            int[] weights = [10, 10, 11, 11, 12, 12, 11, 11, 10, 10];

            int totalWeight = weights.Sum();
            int row=0;
            int col=0;
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
        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            lblRoll.Content = getRoll().ToString();
        }
        private void playRound(int combatRatio, CombatTable combatTable, ref int enemyEndurance, ref int loneWolfEndurance)
        {
            int roll = getRoll();
            Tuple<int, int> damage = combatTable.getCombatTableValue(roll, combatRatio);
            enemyEndurance -= damage.Item1 == -1 ? enemyEndurance : damage.Item1;
            loneWolfEndurance -= damage.Item2 == -1 ? loneWolfEndurance : damage.Item2;
            combatLog.addLine(roll, damage);
        }
        private void btnSimulateFight_Click(object sender, RoutedEventArgs e)
        {
            int enemyEndurance = int.Parse(txtEnemyEndurance.Text);
            int loneWolfEndurance = int.Parse(txtLoneEndurance.Text);
            int combatRatio = int.Parse(txtLoneCS.Text) - int.Parse(txtEnemyCS.Text);
            combatLog.addCombat(combatRatio, enemyEndurance, loneWolfEndurance);
            while (loneWolfEndurance > 0 && enemyEndurance > 0)
            {
                playRound(combatRatio, combatTable, ref enemyEndurance, ref loneWolfEndurance);
            }
            string winner = loneWolfEndurance > 0 ? "Lone Wolf" : "Enemy";
            combatLog.addLine( winner + " wins the combat with " + (winner=="Lone Wolf" ? loneWolfEndurance : enemyEndurance) + " Endurance remaining." );
            lblCombatResult.Content = "LW: " + loneWolfEndurance + ", E: " + enemyEndurance;
        }

        private void btnCombatLog_Click(object sender, RoutedEventArgs e)
        {
            CombatLogWindow cbLogWindow = new CombatLogWindow();
            cbLogWindow.Show();
        }

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterSheetWindow charWindow = new CharacterSheetWindow();
            charWindow.Show();
        }
    }
}