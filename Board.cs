using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe3
{
    public class Board
    {
        private Player player1;
        private Player player2;
        public int TurnCount { get; set; }
        public int TotalGames { get; set; }
        public int Size { get; private set; } = 3;
        public string[] CurrentState { get; set; }
        public Player Player1
        {
            get { return player1; }
            private set { player1 = value; }
        }
        public Player Player2
        {
            get { return player2; }
            private set { player2 = value; }
        }
        public Board()
        {
            this.TurnCount = 0;
            this.TotalGames = 0;
            this.CurrentState = new string[Size * Size];
            this.player1 = new Player();
            this.player2 = new Player();
            this.player1.Char = "x";
            this.player2.Char = "o";
        }

        private void fillBoard()
        {
            for (int i = 0; i < 9; i++) { CurrentState[i] = " "; }
        }
        public void Rematch()
        {
            this.TurnCount = 0;
            this.player1.Turns = 0;
            this.player2.Turns = 0;
            this.player2.movesChain.Clear();
            this.player2.movesChain.Clear();
            fillBoard();
        }

        public void NewGame()
        {
            this.TurnCount = 0;
            this.TotalGames = 0;
            fillBoard();
            this.player1.ResetPlayerData();
            this.player1.ResetPlayerData();
            Console.WriteLine("Enter Player1 name (1 char. or more): ");
            player1.setName(Console.ReadLine());
            Console.WriteLine($"Player 1 name is {player1.Name}");
            Console.WriteLine("Enter Player2 name (1 char. or more): ");
            player2.setName(Console.ReadLine());
            Console.WriteLine($"Player 2 name is {player2.Name}");
            Console.WriteLine($"New board prepared. Total games in session so far: {this.TotalGames}");

        }


    }
}
