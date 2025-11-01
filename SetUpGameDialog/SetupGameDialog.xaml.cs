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

namespace Othello.View
{
        
    public enum PlayerType { Human, Computer }

    public sealed class SetUpGameResult
    {
        public string Player1Name { get; init; } = "";
        public PlayerType Player1Type { get; init; }
        public string Player2Name { get; init; } = "";
        public PlayerType Player2Type { get; init; }
        public bool Player1Black { get; init; }
    }


    public partial class SetupGameDialog : Window
    {
        public SetUpGameResult? Result { get; private set; }

        public SetupGameDialog()
        {
            InitializeComponent();
            Player1NameBox.Text = "Black";
            Player2NameBox.Text = "White";
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            var p1 = (Player1NameBox.Text ?? String.Empty).Trim();
            var p2 = (Player2NameBox.Text ?? String.Empty).Trim();

            if (string.IsNullOrWhiteSpace(p1) || string.IsNullOrWhiteSpace(p2))
            {
                MessageBox.Show(this, "Enter the player names: "); 
                return;
            }
            if(string.Equals(p1, p2))
            {
                MessageBox.Show(this, "Player names have to be different");
                return;
            }

            PlayerType p1Type;
            if(Player1TypeBox.SelectedIndex == 1)
            {
                p1Type = PlayerType.Computer;
            }
            else
            {
                p1Type = PlayerType.Human;
            }

            PlayerType p2Type;
            if(Player2TypeBox.SelectedIndex == 1)
            {
                p2Type = PlayerType.Computer;
            }
            else
            {
                p2Type = PlayerType.Human;
            }

            Result = new SetUpGameResult
            {
                Player1Name = p1,
                Player1Type = p1Type,
                Player2Name = p2,
                Player2Type = p2Type,
                Player1Black = true
            };

            //DialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Result = null;
            DialogResult = false;
            Close();
        }

    }
}
