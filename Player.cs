using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe3
{
    public class Player
    {
        public string Name { get; set; }
        public int Turns { get; set; }
        public int Wins { get; set; }
        public string Char { get; set; }
        public List<int> movesChain { get; set; }

        public Player()
        {
            this.Name = "";
            this.Char = "";
            this.Wins = 0;
            this.Turns = 0;
            this.movesChain = new List<int>();
        }

        public void setName(string text)
        {
            while (text.Length < 1)
            {
                Console.WriteLine("Invalid Player name, try again: ");
                text = Console.ReadLine();
            }
            this.Name = text;
        }

        public void ResetPlayerData()
        {
            this.Name = "";
            this.Turns = 0;
            this.Wins = 0;
            this.movesChain.Clear();
        }
    }
}
