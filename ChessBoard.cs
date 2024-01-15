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


        public bool turn; // True for white, False for black

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

        private void enPassant()
        {
            int leftSide = col - 1;
            int rightSide = col + 1;

            if (turn && row == 4) // White side en passant
            {

                if (rightSide < 8 && CheckBit(specialMoves[BENP], rightSide))
                {
                    string curMove = $"{letters[col]}{row + 1} {letters[rightSide]}{row + 2}";
                    LegalMoves.Add(curMove);
                }

                if (leftSide >= 0 && CheckBit(specialMoves[BENP], leftSide))
                {
                    string curMove = $"{letters[col]}{row + 1} {letters[leftSide]}{row + 2}";
                    LegalMoves.Add(curMove);
                }
            }
            else if (!turn && row == 3) // Black side en passant
            {
                if (rightSide < 8 && CheckBit(specialMoves[WENP], rightSide))
                {
                    string curMove = $"{letters[col]}{row + 1} {letters[rightSide]}{row}";
                    LegalMoves.Add(curMove);
                }

                if (leftSide >= 0 && CheckBit(specialMoves[WENP], leftSide))
                {
                    string curMove = $"{letters[col]}{row + 1} {letters[leftSide]}{row}";
                    LegalMoves.Add(curMove);
                }
            }
        }

        private void generateMovesKnight()
        {
            int[] dx = { 2, 2, -2, -2, 1, 1, -1, -1 };
            int[] dy = { 1, -1, 1, -1, 2, -2, 2, -2 };

            int newRow, newCol;
            for (int i = 0; i < 8; ++i)
            {
                newRow = row + dx[i];
                newCol = col + dy[i];

                if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                {
                    if (Chessboard[newRow][newCol] == ' ' || diffColor(Chessboard[newRow][newCol]))
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
            castle();
        }

        private void castle()
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
                    LegalMoves.Add("e1 g1");
                }
                // Queen's side castling
                else if ((Chessboard[0][1] == ' ' && Chessboard[0][1] == ' ' && Chessboard[0][1] == ' ') &&
                    CheckBit(tempByte, 0) &&
                    CheckBit(tempByte, 1)
                    )
                {
                    LegalMoves.Add("e1 c1");
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
                    LegalMoves.Add("e8 g8");
                }
                // Queen's side castling
                else if ((Chessboard[7][1] == ' ' && Chessboard[7][2] == ' ' && Chessboard[7][3] == ' ') &&
                    CheckBit(tempByte, 5) &&
                    CheckBit(tempByte, 6)
                    )
                {
                    LegalMoves.Add("e8 c8");
                }
            }
        }

        //---------------------------------------------Tools---------------------------------------------//

        private byte SetBit(byte curByte, int pos, bool set)
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
        private bool CheckBit(byte value, int bitPosition)
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

        private void setEnPassantZero()
        {
            specialMoves[WENP] = 0;
            specialMoves[BENP] = 0;
        }

        public static int getNumberFromCol(char letter)
        {
            for (int i = 0; i < letters.Length; i++)
            {
                if (letter == letters[i])
                {
                    return i;
                }
            }
            return 10;
        }

        public bool specialMove;

        public static char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        //---------------------------------------------Move Execution & Notation---------------------------------------------//
        public void ExecuteMove(string move)
        {
            // The first location e.g. b1 is the piece we selected to move
            // The second one is the target location

            string[] sepMoves = move.Split(' ');

            // Get the coordinates for the moves selected
            int curCol = getNumberFromCol(sepMoves[0][0]);
            int curRow = (int)char.GetNumericValue(sepMoves[0][1]) - 1;


            int tarCol = getNumberFromCol(sepMoves[1][0]);
            int tarRow = (int)char.GetNumericValue(sepMoves[1][1]) - 1;

            char tempPiece = Chessboard[curRow][curCol];

            specialMove = false;
            int rightSide = curCol + 1;
            int leftSide = curCol - 1;
            switch (tempPiece)
            {

                case 'P':
                    // Twice forward check
                    if (tarRow == 3 && curRow == 1)
                        specialMoves[WENP] = SetBit(0, curCol, true);

                    // En passant executed
                    if (curRow != 4) break;

                    else if (rightSide < 8 && CheckBit(specialMoves[BENP], rightSide))
                    {
                        Chessboard[4][rightSide] = ' ';
                    }
                    else if (leftSide >= 0 && CheckBit(specialMoves[BENP], leftSide))
                    {
                        Chessboard[4][leftSide] = ' ';
                    }
                    setEnPassantZero();
                    specialMove = true;
                    break;
                case 'p':
                    // Twice forward check
                    if (tempPiece == 'p' && tarRow == 4 && curRow == 6)
                        specialMoves[BENP] = SetBit(0, curCol, true);

                    // En passant executed
                    if (curRow != 3) break;
                    else if (rightSide < 8 && CheckBit(specialMoves[WENP], rightSide))
                    {
                        Chessboard[3][rightSide] = ' ';

                    }
                    else if (leftSide >= 0 && CheckBit(specialMoves[WENP], leftSide))
                    {
                        Chessboard[3][leftSide] = ' ';
                    }
                    setEnPassantZero();
                    specialMove = true;
                    break;
                case 'K':
                    if (tarCol == 2)
                    { // Queen's side castling
                        Chessboard[0][3] = 'R';
                        Chessboard[0][0] = ' ';
                        specialMove = true;
                    }
                    else if (tarCol == 6)
                    { // King's side castling
                        Chessboard[0][5] = 'R';
                        Chessboard[0][7] = ' ';
                        specialMove = true;
                    }
                    setEnPassantZero();
                    // Disable castling from this side
                    specialMoves[CASTLE] = SetBit(specialMoves[CASTLE], 1, false);
                    break;
                case 'k':
                    if (tarCol == 2)
                    { // Queen's side castling
                        Chessboard[7][3] = 'r';
                        Chessboard[7][0] = ' ';
                        specialMove = true;
                    }
                    else if (tarCol == 6)
                    { // King's side castling
                        Chessboard[7][5] = 'r';
                        Chessboard[7][7] = ' ';
                        specialMove = true;
                    }
                    setEnPassantZero();
                    // Disable castling from this side
                    specialMoves[CASTLE] = SetBit(specialMoves[CASTLE], 6, false);
                    break;
                default:
                    setEnPassantZero();
                    if (tarRow == 0 || curRow == 0)
                    {
                        // Set white side castling to off depending on the square that is affected
                        if (tarCol == 0 || curCol == 0) // Queen's side
                            specialMoves[CASTLE] = SetBit(specialMoves[CASTLE], 0, false);

                        else if (tarCol == 7 || curCol == 7) // King's side
                            specialMoves[CASTLE] = SetBit(specialMoves[CASTLE], 3, false);
                    }
                    if (tarRow == 7 || curRow == 7)
                    {
                        // Set white side castling to off depending on the square that is affected
                        if (tarCol == 0 || curCol == 0) // Queen's side
                            specialMoves[CASTLE] = SetBit(specialMoves[CASTLE], 5, false);

                        else if (tarCol == 7 || curCol == 7) // King's side
                            specialMoves[CASTLE] = SetBit(specialMoves[CASTLE], 7, false);
                    }
                    break;
            }

            // Update the chessboard
            Chessboard[tarRow][tarCol] = Chessboard[curRow][curCol];
            Chessboard[curRow][curCol] = ' ';
        }

        private void MovePiece(int targetRow, int targetCol)
        {   // This adds all the legal moves in an array
            char targetPiece = Chessboard[targetRow][targetCol];

            // The reason we use this notation e.g. e2 e4 is so we can connect it with Stockfish in the future
            //   which uses the same notation
            string curMove = $"{letters[col]}{row + 1} {letters[targetCol]}{targetRow + 1}";
            LegalMoves.Add(curMove);
        }
    }
}