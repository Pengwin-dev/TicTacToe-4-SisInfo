using System;
using System.Collections.Generic;

namespace TicTacToe3
{
    public class Program
    {
        private static Board currentBoard;
        private static Player currentPlayer;
        private static List<string> tempChain;
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to TicTacToe Autistic Edition v0.0001");
            MainMenu();

        }

        // tried to fix the bugged checkWin()
        private static bool checkForWinner()

        {
            bool ans=false;
            //rows
            if (currentBoard.CurrentState[0] == currentBoard.CurrentState[1] && currentBoard.CurrentState[1] == currentBoard.CurrentState[2])
            {
                ans = true;
            }
            if (currentBoard.CurrentState[3] == currentBoard.CurrentState[4] && currentBoard.CurrentState[4] == currentBoard.CurrentState[5])
            {
                ans = true;
            }
            if (currentBoard.CurrentState[8] == currentBoard.CurrentState[7] && currentBoard.CurrentState[7] == currentBoard.CurrentState[8])
            {
                ans = true;
            }
            //columns
            if (currentBoard.CurrentState[0] == currentBoard.CurrentState[3] && currentBoard.CurrentState[3] == currentBoard.CurrentState[6])
            {
                ans = true;
            }
            if (currentBoard.CurrentState[1] == currentBoard.CurrentState[4] && currentBoard.CurrentState[4] == currentBoard.CurrentState[7])
            {
                ans = true;
            }
            if (currentBoard.CurrentState[2] == currentBoard.CurrentState[5] && currentBoard.CurrentState[5] == currentBoard.CurrentState[8])
            {
                ans = true;
            }
            //diagonals
            if (currentBoard.CurrentState[0] == currentBoard.CurrentState[4] && currentBoard.CurrentState[4] == currentBoard.CurrentState[8])
            {
                ans = true;
            }
            if (currentBoard.CurrentState[2] == currentBoard.CurrentState[4] && currentBoard.CurrentState[4] == currentBoard.CurrentState[6])
            {
                ans = true;
            }
           return ans;

        }
        private static bool difficultiesMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose your difficulty:");
            Console.WriteLine("1) Easy");
            Console.WriteLine("2) Medium");
            Console.WriteLine("3) Hard");
            Console.Write("\r\n Select an option: ");

            return true;
        }

        private static bool MainMenu()
        {

            currentBoard = new Board();
            tempChain = new List<string>(3);
            NewGame();
            Console.Clear();
            Console.WriteLine("Choose an game mode:");
            Console.WriteLine("1) 1 vs 1");
            Console.WriteLine("2) 1 vs PC");
            Console.WriteLine("3) Online");
            Console.Write("\r\n Select an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("1st Player: Get your nickname:");
                    string nick = Console.ReadLine();
                    Player p1 = new Player();
                    p1.setName(nick);
                    Console.WriteLine("2nt Player: Get your nickname:");
                    nick = Console.ReadLine();
                    Player p2 = new Player();
                    p2.setName(nick);
                    return true;

                case "2":
                    Console.WriteLine("Unimplemented Feature srry... we are working on that ^^'");
                    return true;

                case "3":
                    Console.WriteLine("Unimplemented Feature srry... we are working on that ^^'");

                    return false;
                default:
                    return true;
            }
        }

        private static void NewGame()
        {
            currentBoard.NewGame();
            //deberia ser un puntero a Player, y shuffle una funcion de saltar al otro jugador. (pares o algo similar)
            currentPlayer = Shuffle(); // selecciona quien empieza.... 
            Console.WriteLine($"Board size is {currentBoard.Size}. Player {currentPlayer.Name} has the first turn:");
            drawCurrentBoard();
            processGame();
        }

        private static void processGame()
        {
            if (currentBoard.TurnCount >= 9) // || checkWinner()
            {
                endGameDraw();
            }
            // orden de casillas esta invertido :s
            Console.WriteLine($"{currentPlayer.Name}, enter the number from 1 to 9, each number indicates square on the board: ");
            int squareNumber = processInput(Console.ReadLine());
            processMove(currentPlayer, squareNumber);

        }

        private static void processMove(Player curPlayer, int square)
        {
            if (currentBoard.CurrentState[square - 1] == " ")
            {
                Console.WriteLine($"{currentPlayer.Name} takes square {square}");
                currentBoard.CurrentState[square - 1] = currentPlayer.Char;
                currentBoard.TurnCount++;
                currentPlayer.Turns++;
                //movesChain: guarda los movimientos de un player.
                currentPlayer.movesChain.Add(square);
                //dibuja la t abla
                drawCurrentBoard();
                if (checkWin() == false)
                {
                    currentPlayer = nextPlayer();
                    processGame();
                }
                else
                {
                    endGameWin();
                }
            }
            else
            {
                Console.WriteLine($"Board square {square} is already occupied by \"{currentBoard.CurrentState[square - 1]}\"");
                processGame();
            }
        }

        private static int processInput(string input)
        {
            int number = -1;
            while (number == -1)
            {
                try
                {
                    number = Int32.Parse(input);
                    if (number >= 1 && number <= 9)
                    { return number; }
                    else
                    {
                        Console.WriteLine($"{input} is not a valid number in 1-9 range, try again: ");
                        number = -1;
                        input = Console.ReadLine();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"{input} is not a valid number in 1-9 range, try again: ");
                    number = -1;
                    input = Console.ReadLine();
                }
            }
            return number;
        }

        private static void processGameMode(string input)
        {
            while (input.ToLower() != "new" || input.ToLower() != "rematch")
            {
                if (input.ToLower() == "new")
                {
                    NewGame();
                    break;
                }
                else if (input.ToLower() == "rematch")
                {
                    Rematch();
                    break;
                }
                else
                {
                    Console.WriteLine("Unknown command, try again!");
                    input = Console.ReadLine();
                    continue;
                }
            }

        }

        private static void drawCurrentBoard()
        {
            var z = currentBoard.CurrentState;
            Console.Write($" {z[6]} | {z[7]} | {z[8]} \n");
            Console.Write($"---+---+---\n");
            Console.Write($" {z[3]} | {z[4]} | {z[5]} \n");
            Console.Write($"---+---+---\n");
            Console.Write($" {z[0]} | {z[1]} | {z[2]} \n");
        }

        private static Player nextPlayer()
        {
            if (currentPlayer == currentBoard.Player1) { return currentBoard.Player2; }
            else { return currentBoard.Player1; }
        }
        private static Player Shuffle()
        {
            Random randomID = new Random();
            int rpl = randomID.Next(1, 101);
            if (rpl % 2 == 0) { return currentBoard.Player1; }
            else { return currentBoard.Player2; }
        }

        private static bool checkWin()
        {
            if (currentPlayer.Turns < 3 || currentPlayer.movesChain.Count < 3)
            { return false; }
            else
            {
                tempChain.Clear();
                for (int i = 1; i <= 3; i++)
                {
                    tempChain.Add(currentPlayer.movesChain[currentPlayer.movesChain.Count - i].ToString());
                }
                tempChain.Sort();
                var chain = string.Join<string>("", tempChain);
                if (chain == "147" || chain == "258" || chain == "369" || chain == "789" || chain == "456" || chain == "123" || chain == "357" || chain == "159")
                { return true; }
                else { return false; }
            }

        }

        private static void endGameWin()
        {
            currentBoard.TotalGames++;
            currentPlayer.Wins++;
            Console.WriteLine($"Congratulations, player {currentPlayer.Name} won!");
            Console.WriteLine($"Game stats: turns this round - {currentBoard.TurnCount} | total rounds - {currentBoard.TotalGames}");
            Console.WriteLine($"Player stats: {currentBoard.Player1.Name}\'s wins: {currentBoard.Player1.Wins} | {currentBoard.Player2.Name}\'s wins: {currentBoard.Player2.Wins}");
            Console.WriteLine("Type \"rematch\" for a rematch or \"new\" to start new game with new players");
            processGameMode(Console.ReadLine());
        }

        private static void endGameDraw()
        {
            currentBoard.TotalGames++;
            Console.WriteLine($"Board is full, none of the player have won.");
            Console.WriteLine("Type \"rematch\" for a rematch or \"new\" to start new game with new players");
            processGameMode(Console.ReadLine());
        }

        private static void Rematch()
        {
            Console.WriteLine("Starting new round");
            currentBoard.Rematch();
            drawCurrentBoard();
            processGame();
        }
    }





}

