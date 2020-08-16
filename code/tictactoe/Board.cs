using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class Board
    {
        private int player_pos;
        private int ai_pos;
        private int[] board_state;
        private int total_plays;
        private int winner;
        public Board()
        {
            this.player_pos = 0;
            this.ai_pos = 0;
            this.board_state = new int[9];
            this.total_plays = 0;
        }

        public void ClearBoard()
        {
            this.player_pos = 0;
            this.ai_pos = 0;
            this.board_state = new int[9];
            this.total_plays = 0;
        }

        public void Play()
        {
            if (IsGameOver())
            {
                EndGame();
            }
            
            bool valid;
            int pos;
            do
            {
                Console.WriteLine("\nWhat position would you like to place your next piece?");
                pos = Convert.ToUInt16(Console.ReadLine());

                // check player position for correctness and unsure position has not already been played
                if (pos < 1 || pos > 9)
                {
                    Console.WriteLine("Your entry is invalid. Please try again!");
                    valid = false;
                }
                else
                {
                    if (this.board_state[pos - 1] != 0)
                    {
                        Console.WriteLine("This position has already been played. Please try again!");
                        valid = false;
                    } else
                    {
                        valid = true;
                    }
                }
            } while (!valid);

            this.player_pos = pos;
            UpdateBoard();
        }
        
        public void UpdateBoard()
        {
            // X (player) = 1, O (ai) = 2

            // check if player has had first turn. If so, update variables
            if (this.player_pos != 0)
            {
                this.board_state[this.player_pos - 1] = 1;
            }

            // check for game over
            if (IsGameOver())
            {
                CurrentBoard();
                EndGame();
            }

            // Generate AI position
            Console.WriteLine("\nAnd now the computer will play...");
            bool valid;
            do
            {
                Random r = new Random();
                this.ai_pos = r.Next(1, 10);

                // if position is already taken, roll again.
                if (this.board_state[this.ai_pos - 1] != 0)
                {
                    valid = false;
                } else
                {
                    valid = true;
                }
            } while (!valid);

            // update variables
            this.board_state[this.ai_pos - 1] = 2;
            CurrentBoard();
        }

        public bool IsGameOver()
        {
            //check for player win
            if (((this.board_state[0] == 1) && (this.board_state[1] == 1) && (this.board_state[2] == 1)) || // row 1
                ((this.board_state[3] == 1) && (this.board_state[4] == 1) && (this.board_state[5] == 1)) || // row 2
                ((this.board_state[6] == 1) && (this.board_state[7] == 1) && (this.board_state[8] == 1)) || // row 3
                ((this.board_state[0] == 1) && (this.board_state[3] == 1) && (this.board_state[6] == 1)) || // col 1
                ((this.board_state[1] == 1) && (this.board_state[4] == 1) && (this.board_state[7] == 1)) || // col 2
                ((this.board_state[2] == 1) && (this.board_state[5] == 1) && (this.board_state[8] == 1)) || // col 3
                ((this.board_state[0] == 1) && (this.board_state[4] == 1) && (this.board_state[8] == 1)) || // diag 1
                ((this.board_state[2] == 1) && (this.board_state[4] == 1) && (this.board_state[6] == 1)))   // diag 2
            {
                this.winner = 1;
                return true;
            }

            // check for ai win
            else if(((this.board_state[0] == 2) && (this.board_state[1] == 2) && (this.board_state[2] == 2)) || // row 1
                    ((this.board_state[3] == 2) && (this.board_state[4] == 2) && (this.board_state[5] == 2)) || // row 2
                    ((this.board_state[6] == 2) && (this.board_state[7] == 2) && (this.board_state[8] == 2)) || // row 3
                    ((this.board_state[0] == 2) && (this.board_state[3] == 2) && (this.board_state[6] == 2)) || // col 1
                    ((this.board_state[1] == 2) && (this.board_state[4] == 2) && (this.board_state[7] == 2)) || // col 2
                    ((this.board_state[2] == 2) && (this.board_state[5] == 2) && (this.board_state[8] == 2)) || // col 3
                    ((this.board_state[0] == 1) && (this.board_state[4] == 1) && (this.board_state[8] == 1)) || // diag 1
                    ((this.board_state[2] == 1) && (this.board_state[4] == 1) && (this.board_state[6] == 1)))   // diag 2
            {
                this.winner = 2;
                return true;
            }

            // check for tie
            else if (this.total_plays == 9)
            {
                this.winner = 0;
                return true;
            }

            // otherwise, game not over
            return false;
        }

        public void EndGame()
        {
            Console.Write("\nGAME OVER! Results: ");

            // tied game results
            if (this.winner == 0)
            {
                Console.WriteLine("Tied!");
                PlayAgain();
            } else if (this.winner == 1)
            {
                Console.WriteLine("You Win! Great Job.");
                PlayAgain();
            } else if (this.winner == 2)
            {
                Console.WriteLine("The Computer Wins! You'll get it next time.");
                PlayAgain();
            } else
            {
                Console.Write("Unknown Error Occurred. Game Resetting.");
                ClearBoard();
                Instructions();
            }
        }

        public void PlayAgain()
        {
            bool valid;
            do
            {
                Console.WriteLine("Play Again? [Y/N]: ");
                string again = Console.ReadLine();
                again.ToUpper();
                if (again == "Y")
                {
                    ClearBoard();
                    Instructions();
                    valid = true;
                }
                else if (again == "N")
                {
                    Console.WriteLine("Thanks for playing! You can press <enter> or close the game to quit.");
                    valid = true;
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }

                }
                else
                {
                    Console.WriteLine("Invalid entry. Try again.");
                    valid = false;
                }
            } while (!valid);
        }

        public void CurrentBoard()
        {
            Console.WriteLine("\n--Current Board State--");

            Console.Write("| ");
            for (int x = 0; x < this.board_state.Length; x++)
            {
                switch(this.board_state[x])
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("Key:\tPlayer = X\tComputer = O");
            Play();
        }

        public void Instructions()
        {
            Console.WriteLine("\n--Welcome to Tic-Tac-Toe!--/nPlease use the board reference below for position placement.");
            Console.WriteLine("\n| 1 | 2 | 3 |" +
                "              \n| 4 | 5 | 6 |" +
                "              \n| 7 | 8 | 9 |");
            Console.WriteLine("\nPress any key to begin the game.");
            Console.ReadLine();

            // roll to see who goes first.
            Random r = new Random();
            int x = r.Next(1, 3);
            if (x == 2)
            {
                Console.WriteLine("\nThe computer goes first...");
                UpdateBoard();
            }

            Play();
        }

    }
}
