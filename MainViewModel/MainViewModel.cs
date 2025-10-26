using Othello.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainViewModel
{
    class MainViewModel
    {
        // private GameManager _gameManager;
        public ICommand SquareClickCommand { get; private init; }
        public ICommand NewGameCommand { get; private init; }
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
        }
        public void StartNewGame()
        {
        }
        public void StartNewGameWithPlayers(/*Player player1, Player player2*/)
        {

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
