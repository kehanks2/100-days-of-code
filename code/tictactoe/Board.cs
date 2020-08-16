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
        
        // constructor
        public Board()
        {
            this.player_pos = 0;
            this.ai_pos = 0;
            this.board_state = new int[9];
            this.total_plays = 0;
        }

        // reset board to begin new game
        public void ClearBoard()
        {
            this.player_pos = 0;
            this.ai_pos = 0;
            this.board_state = new int[9];
            this.total_plays = 0;
        }

        public void Play()
        {
            // check for game over
            if (IsGameOver())
                EndGame();
            
            bool valid;
            int pos;
            do 
            {
                Console.WriteLine("\nWhat position would you like to place your next piece?");
                pos = Convert.ToUInt16(Console.ReadLine());

                // check player position for correctness 
                if (pos < 1 || pos > 9)
                {
                    Console.WriteLine("Your entry is invalid. Please try again!");
                    valid = false;
                }
                else
                {
                    // check that position has not already been played
                    if (this.board_state[pos - 1] != 0)
                    {
                        Console.WriteLine("This position has already been played. Please try again!");
                        valid = false;
                    } else
                        valid = true;
                }
            } while (!valid);

            this.player_pos = pos;
            UpdateBoard();
        }
        
        public void UpdateBoard()
        {
            // X (player) = 1, O (ai) = 2

            // check if player has had first turn. If so, update board state and total plays
            if (this.player_pos != 0)
            {
                this.board_state[this.player_pos - 1] = 1;
                this.total_plays++;
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
                    valid = false;
                else
                    valid = true;

            } while (!valid);

            // update board state and total plays
            this.board_state[this.ai_pos - 1] = 2;
            this.total_plays++;
            CurrentBoard();
        }
        public void CurrentBoard()
        {
            Console.WriteLine("\n--Current Board State--");

            // prints out the current board with player and ai pieces in proper positions
            for (int x = 0; x < this.board_state.Length; x++)
            {
                switch (this.board_state[x])
                {
                    case 1:
                        Console.Write("| X ");
                        break;
                    case 2:
                        Console.Write("| O ");
                        break;
                    case 0:
                        Console.Write("|   ");
                        break;
                }
                // creates new line if position is last for that line
                if (x == 2 || x == 5 || x == 8)
                    Console.WriteLine("|");

            }

            Console.WriteLine("Key:\tPlayer = X\tComputer = O");
            Play();
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

            if (this.winner == 0)
            {
                // tied game results
                Console.WriteLine("Tied!");
                PlayAgain();
            } else if (this.winner == 1)
            {
                // player win results
                Console.WriteLine("You Win! Great Job.");
                PlayAgain();
            } else if (this.winner == 2)
            {
                // ai win results
                Console.WriteLine("The Computer Wins! You'll get it next time.");
                PlayAgain();
            } else
            {
                // should never run
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
                again = again.ToUpper();
                if (again == "Y")
                {
                    // starts new game with cleared board if yes
                    ClearBoard();
                    Instructions();
                    valid = true;
                }
                else if (again == "N")
                {
                    // user may exit game if no
                    Console.WriteLine("Thanks for playing!");
                    ExitConsole();
                    return;
                }
                else
                {
                    // invalid entry allows player to enter again
                    Console.WriteLine("Invalid entry. Try again.");
                    valid = false;
                }
            } while (!valid);
        }

        public void ExitConsole()
        {
            Environment.Exit(0);
        }

        public void Instructions()
        {
            Console.WriteLine("\n--Welcome to Tic-Tac-Toe!--\nPlease use the board reference below for position placement.");
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
