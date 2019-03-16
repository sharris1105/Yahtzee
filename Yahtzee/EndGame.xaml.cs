using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;


namespace Yahtzee
{
    /// <summary>
    /// Interaction logic for EndGame.xaml
    /// </summary>
    public partial class EndGame : DXWindow
    {
        public EndGame(int totalScore)
        {
            InitializeComponent();
            EndText.Text = "Congratulations! Your score was " + totalScore  + "! Would you like to play again?";
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            var win2 = new OnePlayer();
            win2.Show();
            Close();
        }

        private void CloseGame_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
