using Othello.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Othello.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // private GameManager _gameManager;
        public ICommand SquareClickCommand { get; private init; }
        public ICommand NewGameCommand { get; private init; }
        public ICommand ExitCommand { get; private init; }
        public MainViewModel()
        {
            Action<object?> _local_HandleSquareClick = (something) =>
            {
                // convert something to Square
                // depend on View implementation
                HandleSquareClick();
            };
            SquareClickCommand = new RelayCommand(_local_HandleSquareClick);
            Action<object?> _local_NewGameCommand = (something) =>
            {
                // convert something to Player1 and Player2
                // depend on View implementation
                StartNewGameWithPlayers();
            };
            NewGameCommand = new RelayCommand(_local_NewGameCommand);
            Action<object?> _local_ExitCommand = (something) =>
            {
                if (MessageBox.Show("Are you sure you want to exit?", "Exit Game", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    System.Windows.Application.Current.Shutdown();
            };
            ExitCommand = new RelayCommand(_local_ExitCommand);
            StartNewGame();
        }
        public void StartNewGame()
        {
            TryComputerTurn();
        }
        public void StartNewGameWithPlayers(/*Player player1, Player player2*/)
        {
            TryComputerTurn();
        }
        async void TryComputerTurn()
        {

        }
        void OnBoardUpdated(/*Square[,] board*/)
        {

        }
        void OnGameWon(/*Player winner*/)
        {
        }
        void OnGameDrawn()
        {
        }
        public void HandleSquareClick(/*Square square*/)
        {

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
