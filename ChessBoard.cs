using System;
using System.Collections.Generic;

namespace ChessGame
{
    internal class ChessBoard
    {


        /*  
         *   The pieces are represented by a Char 
         *   Upper case letters are used for white, lower case for black
         *   Naturally the board is displayed by White's side
        */

        public char[][] Chessboard = new char[8][];

        // The special moves works with the same logic as a bitboard
        byte[] specialMoves = new byte[3];

        // Respective positions in the special moves array
        const int CASTLE = 0;
        const int BENP = 1; // Black en-passant
        const int WENP = 2; // White en-passant
        // If white piece has moved forward with 2 moves, it assigns to its color the en-passant


        private bool turn; // True for white, False for black
        private float evaluation;

        //--------------------------------------------- Start Board ---------------------------------------------//
        public ChessBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                Chessboard[i] = new char[8];
                for (int j = 0; j < 8; j++)
                {
                    Chessboard[i][j] = ' ';
                }
            }
            specialMoves[CASTLE] = 231;
            /* The reason we give the number 231 is because it's binary value is 11100111 the reason being:
               * The left 3 bits that we have as 1 are white side's castling
               * The left bit is the A file rook, the middle bit is the king, and the right is the H file rook
               * same goes for the 3 rightmost bits, but it's black's side castle
               * The zeros in the middle, we do not care about, it's just a barrier to view everything neatly
             */


            turn = true; // Starting with white pieces

            // Pawn placement
            for (int col = 0; col < 8; col++)
            {
                Chessboard[1][col] = 'P'; //White pawn
                Chessboard[6][col] = 'p'; //Black pawn
            }

            placePiece('R', 0); // Place rooks
            placePiece('N', 1); // Place knights
            placePiece('B', 2); // Place bishops

            // Queens
            Chessboard[0][3] = 'Q';
            Chessboard[7][3] = 'q';

            //Kings
            Chessboard[0][4] = 'K';
            Chessboard[7][4] = 'k';
        }


        void placePiece(char piece, short col)
        {
            char blackPiece = Char.ToLower(piece);
            Chessboard[0][col] = piece;
            Chessboard[0][7 - col] = piece;

            Chessboard[7][7 - col] = blackPiece;
            Chessboard[7][col] = blackPiece;
        }

        //--------------------------------------------- Generate Legal Moves---------------------------------------------//
        public List<string> LegalMoves = new List<string>();
        char curPiece;
        int row;
        int col;
        public void generateLegalMoves()
        {
            LegalMoves.Clear();
            // Go through every square of the chessboard
            for (row = 0; row < 8; row++)
            {
                for (col = 0; col < 8; col++)
                {
                    curPiece = Chessboard[row][col];
                    if (curPiece == ' ' || !char.IsUpper(curPiece) == turn) continue;
                    switch (char.ToUpper(curPiece))
                    {
                        case 'P':
                            generateMovesPawn();
                            break;
                        case 'N':
                            generateMovesKnight();
                            break;
                        case 'R':
                            generateMovesRook();
                            break;
                        case 'B':
                            generateMovesBishop();
                            break;
                        case 'Q':
                            // The queen has the same moves as a Rook and a Bishop combined
                            generateMovesBishop();
                            generateMovesRook();

                            break;
                        case 'K':
                            generateMovesKing();
                            break;
                    }
                }
            }
        }


        private void generateMovesPawn()
        {
            int newRow = turn ? row + 1 : row - 1; // For pawn direction

            if (newRow < 0 && newRow > 7) return;// Bounds check


            //---Move Forward
            if (Chessboard[newRow][col] == ' ')
            {
                MovePiece(newRow, col);

                // Special case: Moving two squares forward from the starting position
                if ((turn && row == 1) || (!turn && row == 6)) // Check if it's in starting positions
                {
                    int newTwiceRow = turn ? row + 2 : row - 2;
                    if (Chessboard[newTwiceRow][col] == ' ')
                    {
                        MovePiece(newTwiceRow, col);
                        int enpDet = turn ? WENP : BENP;
                        specialMoves[enpDet] = SetBit(0, col, true);
                    }

                }
            }



            //---Captures
            int leftCol = col - 1;
            int rightCol = col + 1;
            // For black, capture sides are reversed
            if (leftCol >= 0)
            { // Capture left 
                char leftPiece = Chessboard[newRow][leftCol];
                if (leftPiece != ' ' && diffColor(leftPiece))
                    MovePiece(newRow, leftCol);

            }
            if (rightCol <= 7)
            { // Capture right
                char rightPiece = Chessboard[newRow][rightCol];
                if (rightPiece != ' ' && diffColor(rightPiece))
                    MovePiece(newRow, rightCol);
            }

            enPassant();



        }

        // TODO: Fix the capture notation
        private void enPassant()
        {
            int leftSide = col - 1;
            int rightSide = col + 1;
            if ((turn && row == 3)) // White side en passant
            {

                if (rightSide < 7 && CheckBit(specialMoves[BENP], rightSide))
                {
                    Chessboard[row][col] = ' ';
                    Chessboard[row][rightSide] = ' ';
                    Chessboard[row + 1][rightSide] = 'P';
                }
                else if (leftSide > 0 && CheckBit(specialMoves[BENP], leftSide))
                {
                    Chessboard[row][col] = ' ';
                    Chessboard[row][leftSide] = ' ';
                    Chessboard[row + 1][leftSide] = 'P';
                }
            }
            else if ((!turn && row == 4)) // Black side en passant
            {
                if (rightSide < 7 && CheckBit(specialMoves[WENP], rightSide))
                {
                    Chessboard[row][col] = ' ';
                    Chessboard[row][rightSide] = ' ';
                    Chessboard[row - 1][rightSide] = 'p';
                }
                else if (leftSide > 0 && CheckBit(specialMoves[WENP], leftSide))
                {
                    Chessboard[row][col] = ' ';
                    Chessboard[row][leftSide] = ' ';
                    Chessboard[row - 1][leftSide] = 'p';
                }
            }
        }


        private void generateMovesKnight()
        {
            // Possible knight move offsets
            int[] dx = { 2, 2, -2, -2, 1, 1, -1, -1 };
            int[] dy = { 1, -1, 1, -1, 2, -2, 2, -2 };

            int newRow;
            int newCol;
            for (int i = 0; i < 8; ++i)
            { // Go through all the possible knight offsets
                newRow = row + dx[i];
                newCol = col + dy[i];

                // Check if the new position is within the chessboard
                if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                {
                    if (diffColor(Chessboard[newRow][newCol]))
                    {
                        MovePiece(newRow, newCol);
                    }
                }
            }
        }

        void generateMovesBishop()
        {
            int[,] directions = { { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 } }; // All possible directions of the bishop
            for (int dir = 0; dir < 4; dir++) // For all four possible offsets
            {
                for (int step = 1; step < 8; step++) // Covers the entire Chessboard
                {
                    int newRow = row + step * directions[dir, 0];
                    int newCol = col + step * directions[dir, 1];
                    if (newRow < 0 || newRow >= 8 || newCol < 0 || newCol >= 8) break; // Boundary check

                    char targetPiece = Chessboard[newRow][newCol];
                    if (targetPiece == ' ')
                    {
                        MovePiece(newRow, newCol);
                    }
                    else
                    {

                        if (diffColor(Chessboard[newRow][newCol]))
                        {
                            MovePiece(newRow, newCol);
                        }
                        break; // If there is a piece, either capture (if possible) and pause or pause one step before
                    }
                }
            }
        }

        void generateMovesRook()
        {
            int[,] directions = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } }; // All possible directions of the rook
            for (int dir = 0; dir < 4; dir++) // For all four possible directions (vertical and horizontal)
            {
                for (int step = 1; step < 8; step++) // Covers the entire Chessboard
                {
                    int newRow = row + step * directions[dir, 0];
                    int newCol = col + step * directions[dir, 1];
                    if (newRow < 0 || newRow >= 8 || newCol < 0 || newCol >= 8) break; // Boundary check


                    char targetPiece = Chessboard[newRow][newCol];
                    if (targetPiece == ' ')
                    {
                        MovePiece(newRow, newCol);
                    }
                    else
                    {
                        if (diffColor(Chessboard[newRow][newCol]))
                        {
                            MovePiece(newRow, newCol);
                        }
                        break; // If there is a piece, either capture (if possible) and pause or pause one step before
                    }
                }
            }
        }
        void generateMovesKing()
        {
            int[,] directions = {
        { -1, -1 }, { -1, 0 }, { -1, 1 },{ 0, -1 },{ 0, 1 },{ 1, -1 }, { 1, 0 }, { 1, 1 } }; // All possible directions of the king

            for (int dir = 0; dir < 8; dir++) // For all eight possible directions (including diagonals and straight movements)
            {
                int newRow = row + directions[dir, 0];
                int newCol = col + directions[dir, 1];

                if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8) // Boundary check
                {
                    char targetPiece = Chessboard[newRow][newCol];
                    if (targetPiece == ' ' || diffColor(Chessboard[newRow][newCol]))
                    {
                        MovePiece(newRow, newCol);
                    }
                }
            }
        }

        void castle()
        {
            ///11100111
            byte tempByte = specialMoves[CASTLE];
            if (turn)
            {
                // King's side castling
                if ((Chessboard[0][5] == ' ' && Chessboard[0][6] == ' ') &&
                    CheckBit(tempByte, 2) &&
                    CheckBit(tempByte, 1) // King
                    )
                {
                    LegalMoves.Add("O-O");
                }
                // Queen's side castling
                else if ((Chessboard[0][1] == ' ' && Chessboard[9][1] == ' ' && Chessboard[0][1] == ' ') &&
                    CheckBit(tempByte, 0) &&
                    CheckBit(tempByte, 1)
                    )
                {
                    LegalMoves.Add("O-O-O");
                }
            }
            else
            {
                // King's side castling
                if ((Chessboard[7][5] == ' ' && Chessboard[7][6] == ' ') &&
                    CheckBit(tempByte, 7) &&
                    CheckBit(tempByte, 6) // King
                    )
                {
                    LegalMoves.Add("o-o");
                }
                // Queen's side castling
                else if ((Chessboard[7][0] == ' ' && Chessboard[7][1] == ' ' && Chessboard[7][1] == ' ') &&
                    CheckBit(tempByte, 5) &&
                    CheckBit(tempByte, 6)
                    )
                {
                    LegalMoves.Add("o-o-o");
                }
            }
        }

        //---------------------------------------------Tools---------------------------------------------//

        static private byte SetBit(byte curByte, int pos, bool set)
        {
            if (set)
            {
                curByte |= (byte)(1 << pos); // Set the bit to 1 at the specified position
            }
            else
            {
                curByte &= (byte)(~(1 << pos)); // Set the bit to 0 at the specified position
            }
            return curByte;
        }


        static private bool CheckBit(byte value, int bitPosition)
        {
            // Creating a mask with only the bit at the specified position set to 1
            byte mask = (byte)(1 << bitPosition);

            // Performing bitwise AND operation to check if the bit is set
            return (value & mask) != 0;
        }

        private bool diffColor(char capturedPiece)
        {
            return turn != char.IsUpper(capturedPiece);
        }


        public static char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        private void MovePiece(int targetRow, int targetCol)
        {
            char targetPiece = Chessboard[targetRow][targetCol];

            string curMove = $"{curPiece}{letters[targetCol]}{targetRow + 1}";

            if (targetPiece != ' ')
            {
                curMove = $"{curPiece}x{letters[targetCol]}{targetRow + 1}";
            }

            LegalMoves.Add(curMove);
        }
    }
}