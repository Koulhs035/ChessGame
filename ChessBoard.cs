﻿using System;
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

        public char[,] Chessboard = new char[8, 8];

        // The special moves works with the same logic as a bitboard
        byte[] specialMoves = new byte[3];

        // Respective positions in the special moves array
        private const int CASTLE = 0;
        private const int BENP = 1; // Black en-passant
        private const int WENP = 2; // White en-passant
        // If white piece has moved forward with 2 moves, it assigns to its color for en-passant


        public bool turn; // True for white, False for black

        struct moveString
        {
            public int curRow;
            public int curCol;
            public int tarRow;
            public int tarCol;

            public moveString(string move)
            {
                curCol = getNumberFromCol(move[0]);
                curRow = (int)char.GetNumericValue(move[1]) - 1;
                tarCol = getNumberFromCol(move[2]);
                tarRow = (int)char.GetNumericValue(move[3]) - 1;
            }
        };

        //--------------------------------------------- Start Board ---------------------------------------------//
        public ChessBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Chessboard[i, j] = ' ';
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
                Chessboard[1, col] = 'P'; //White pawn
                Chessboard[6, col] = 'p'; //Black pawn
            }

            placePiece('R', 0); // Place rooks
            placePiece('N', 1); // Place knights
            placePiece('B', 2); // Place bishops

            // Queens
            Chessboard[0, 3] = 'Q';
            Chessboard[7, 3] = 'q';

            //Kings
            Chessboard[0, 4] = 'K';
            Chessboard[7, 4] = 'k';
        }

        public bool noKing = false;

        void placePiece(char piece, short col)
        {
            char blackPiece = Char.ToLower(piece);
            Chessboard[0, col] = piece;
            Chessboard[0, 7 - col] = piece;

            Chessboard[7, 7 - col] = blackPiece;
            Chessboard[7, col] = blackPiece;
        }


        //--------------------------------------------- Generate Legal Moves---------------------------------------------//
        public List<string> LegalMoves = new List<string>();
        private char curPiece;
        private int row;
        private int col;
        private bool d2Gen = false;
        public void generateMoves()
        {
            LegalMoves.Clear();
            // Go through every square of the chessboard
            for (row = 0; row < 8; row++)
            {
                for (col = 0; col < 8; col++)
                {
                    curPiece = Chessboard[row, col];
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
            if (Chessboard[newRow, col] == ' ')
            {
                MovePiece(newRow, col);

                // Special case: Moving two squares forward from the starting position
                if ((turn && row == 1) || (!turn && row == 6)) // Check if it's in starting positions
                {
                    int newTwiceRow = turn ? row + 2 : row - 2;
                    if (Chessboard[newTwiceRow, col] == ' ')
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
                char leftPiece = Chessboard[newRow, leftCol];
                if (leftPiece != ' ' && diffColor(leftPiece))
                    MovePiece(newRow, leftCol);

            }
            if (rightCol <= 7)
            { // Capture right
                char rightPiece = Chessboard[newRow, rightCol];
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
                    string curMove = $"{letters[col]}{row + 1}{letters[rightSide]}{row + 2}";
                    LegalMoves.Add(curMove);
                }

                if (leftSide >= 0 && CheckBit(specialMoves[BENP], leftSide))
                {
                    string curMove = $"{letters[col]}{row + 1}{letters[leftSide]}{row + 2}";
                    LegalMoves.Add(curMove);
                }
            }
            else if (!turn && row == 3) // Black side en passant
            {
                if (rightSide < 8 && CheckBit(specialMoves[WENP], rightSide))
                {
                    string curMove = $"{letters[col]}{row + 1}{letters[rightSide]}{row}";
                    LegalMoves.Add(curMove);
                }

                if (leftSide >= 0 && CheckBit(specialMoves[WENP], leftSide))
                {
                    string curMove = $"{letters[col]}{row + 1}{letters[leftSide]}{row}";
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
                    if (Chessboard[newRow, newCol] == ' ' || diffColor(Chessboard[newRow, newCol]))
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

                    char targetPiece = Chessboard[newRow, newCol];
                    if (targetPiece == ' ')
                    {
                        MovePiece(newRow, newCol);
                    }
                    else
                    {

                        if (diffColor(Chessboard[newRow, newCol]))
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


                    char targetPiece = Chessboard[newRow, newCol];
                    if (targetPiece == ' ')
                    {
                        MovePiece(newRow, newCol);
                    }
                    else
                    {
                        if (diffColor(Chessboard[newRow, newCol]))
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
                    char targetPiece = Chessboard[newRow, newCol];
                    if (targetPiece == ' ' || diffColor(Chessboard[newRow, newCol]))
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
                if ((Chessboard[0, 5] == ' ' && Chessboard[0, 6] == ' ') &&
                    CheckBit(tempByte, 2) &&
                    CheckBit(tempByte, 1) // King
                    )
                {
                    LegalMoves.Add("e1g1");
                }
                // Queen's side castling
                else if ((Chessboard[0, 1] == ' ' && Chessboard[0, 1] == ' ' && Chessboard[0, 1] == ' ') &&
                    CheckBit(tempByte, 0) &&
                    CheckBit(tempByte, 1)
                    )
                {
                    LegalMoves.Add("e1c1");
                }
            }
            else
            {
                // King's side castling
                if ((Chessboard[7, 5] == ' ' && Chessboard[7, 6] == ' ') &&
                    CheckBit(tempByte, 7) &&
                    CheckBit(tempByte, 6) // King
                    )
                {
                    LegalMoves.Add("e8g8");
                }
                // Queen's side castling
                else if ((Chessboard[7, 1] == ' ' && Chessboard[7, 2] == ' ' && Chessboard[7, 3] == ' ') &&
                    CheckBit(tempByte, 5) &&
                    CheckBit(tempByte, 6)
                    )
                {
                    LegalMoves.Add("e8c8");
                }
            }
        }

        //---------------------------------------------Tools---------------------------------------------//
        private static byte SetBit(byte curByte, int pos, bool set)
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

        public static bool CheckBit(byte value, int bitPosition)
        {
            // Creating a mask with only the bit at the specified position set to 1
            byte mask = (byte)(1 << bitPosition);

            // Performing bitwise AND operation to check if the bit is set
            return (value & mask) != 0;
        }

        private static int GetFirstSetBit(byte value)
        {
            for (int i = 0; i < 8; i++)
            {
                if (CheckBit(value, i))
                {
                    return i;
                }
            }
            return -1; // No set bit found
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

        public static char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        //---------------------------------------------Move Execution & Notation---------------------------------------------//
        public bool specialMove;


        public void ExecuteMove(string move, Utility utility)
        {
            // The first location e.g. b1 is the piece we selected to move
            // The second one is the target location
            // Notation example e2e4


            moveString myMove = new moveString(move);
            // Get the coordinates for the moves selected
            int curCol = myMove.curCol;
            int curRow = myMove.curRow;

            int tarCol = myMove.tarCol;
            int tarRow = myMove.tarRow;


            char tempPiece = Chessboard[curRow, curCol];

            specialMove = false;

            switch (char.ToUpper(tempPiece))
            {
                case 'P':
                    handlePawnMoves(curRow, curCol, tarRow, tarCol);
                    break;
                case 'K':
                    handleKingMoves(curCol, tarCol);
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
            Chessboard[tarRow, tarCol] = Chessboard[curRow, curCol];
            Chessboard[curRow, curCol] = ' ';
            string tempFen = toFEN();
            utility.movesDone += tempFen + '\n'; // This will be used to store the game progression in our database
        }

        private void handleKingMoves(int curCol, int tarCol)
        {
            if (turn)
            {
                if (curCol == 4)
                {
                    if (tarCol == 2)
                    { // Queen's side castling
                        Chessboard[0, 3] = 'R';
                        Chessboard[0, 0] = ' ';
                        specialMove = true;
                    }
                    else if (tarCol == 6)
                    { // King's side castling
                        Chessboard[0, 5] = 'R';
                        Chessboard[0, 7] = ' ';
                        specialMove = true;
                    }
                }
                // Disable castling from this side
                specialMoves[CASTLE] = SetBit(specialMoves[CASTLE], 1, false);
            }
            else
            {
                if (curCol == 4)
                {
                    if (tarCol == 2)
                    { // Queen's side castling
                        Chessboard[7, 3] = 'r';
                        Chessboard[7, 0] = ' ';
                        specialMove = true;
                    }
                    else if (tarCol == 6)
                    { // King's side castling
                        Chessboard[7, 5] = 'r';
                        Chessboard[7, 7] = ' ';
                        specialMove = true;
                    }
                }
                // Disable castling from this side
                specialMoves[CASTLE] = SetBit(specialMoves[CASTLE], 6, false);

            }
            setEnPassantZero();
        }

        private void handlePawnMoves(int curRow, int curCol, int tarRow, int tarCol)
        {
            char tempPiece = Chessboard[curRow, curCol];
            int rightSide = curCol + 1;
            int leftSide = curCol - 1;
            if (turn)
            {
                // Twice forward check
                if (tarRow == 3 && curRow == 1)
                    specialMoves[WENP] = SetBit(0, curCol, true);

                // En passant executed
                if (curRow != 4) return;

                else if (rightSide < 8 && CheckBit(specialMoves[BENP], rightSide))
                {
                    Chessboard[4, rightSide] = ' ';
                }
                else if (leftSide >= 0 && CheckBit(specialMoves[BENP], leftSide))
                {
                    Chessboard[4, leftSide] = ' ';
                }
                setEnPassantZero();
                specialMove = true;
            }
            else
            {
                // Twice forward check
                if (tempPiece == 'p' && tarRow == 4 && curRow == 6)
                    specialMoves[BENP] = SetBit(0, curCol, true);

                // En passant executed
                if (curRow != 3) return;
                else if (rightSide < 8 && CheckBit(specialMoves[WENP], rightSide))
                {
                    Chessboard[3, rightSide] = ' ';

                }
                else if (leftSide >= 0 && CheckBit(specialMoves[WENP], leftSide))
                {
                    Chessboard[3, leftSide] = ' ';
                }
                setEnPassantZero();
                specialMove = true;
            }
        }

        private ChessBoard(ChessBoard prevChessBoard, string move)
        {
            /*
              * This function generates all the possible outcomes that come after a move is played
              * If we see that after a move, the king can be captured, it becomes illegal and therefore can not be played.
              * If we have 0 legal moves, then it's checkmate.
            */



            // Make a new chessboard with the move that is about to happen
            Array.Copy(prevChessBoard.Chessboard, Chessboard, prevChessBoard.Chessboard.Length);
            turn = !prevChessBoard.turn;
            d2Gen = true;


            moveString moveString = new moveString(move);
            int curCol = moveString.curCol;
            int curRow = moveString.curRow;
            int tarCol = moveString.tarCol;
            int tarRow = moveString.tarRow;

            // Make the move changes on the board
            curPiece = Chessboard[curRow, curCol];
            Chessboard[curRow, curCol] = ' ';
            Chessboard[tarRow, tarCol] = curPiece;

            // Generate all possibilities that could happen if that move got played
            generateMoves();

            char[,] tempCB = new char[8, 8];
            foreach (string moveDone in LegalMoves)
            {   // Look at all possible moves to check if the king was ever captured
                Array.Copy(Chessboard, tempCB, Chessboard.Length);
                moveString = new moveString(moveDone);
                curCol = moveString.curCol;
                curRow = moveString.curRow;
                tarCol = moveString.tarCol;
                tarRow = moveString.tarRow;
                var tempPiece = tempCB[tarRow, tarCol];
                if (char.ToUpper(tempPiece) == 'K')
                {
                    noKing = true;
                    return;
                }
                tempCB[curRow, curCol] = ' ';
                tempCB[tarRow, tarCol] = tempPiece;
            }
        }

        private void MovePiece(int targetRow, int targetCol)
        {   // This function, adds all the legal moves in a list
            char targetPiece = Chessboard[targetRow, targetCol];

            // The reason we use this notation e.g. e2e4 is so we can connect it with Stockfish in the future
            // which uses the same notation
            string curMove = $"{letters[col]}{row + 1}{letters[targetCol]}{targetRow + 1}";

            //ChessBoard newCB = ChessBoard(this, curMove);
            if (!d2Gen)
            {
                ChessBoard tempCB = new ChessBoard(this, curMove);
                if (tempCB.noKing) return;
            }
            LegalMoves.Add(curMove);
        }

        public string toFEN()
        {   // FEN or "Forsyth–Edwards Notation" is the algebraic notation of a chess position, 
            // which interfaces like Stockfish use
            string fen = string.Empty;

            // Adding pieces
            for (int row = 7; row >= 0; row--)
            {
                int count = 0;
                for (int col = 0; col < 8; col++)
                {
                    char curPiece = Chessboard[row, col];
                    if (curPiece != ' ')
                    {
                        if (count > 0)
                        {
                            fen += count.ToString();
                            count = 0;
                        }
                        fen += curPiece;
                    }
                    else
                    {
                        count++;
                    }
                }
                if (count > 0)
                    fen += count.ToString();

                if (row != 0)
                    fen += '/';
            }

            char fTurn = turn ? 'w' : 'b';
            fen += $" {fTurn} ";

            // Adding Castle
            byte castling = specialMoves[CASTLE];
            if (castling == 0)
            {
                fen += '-';
            }
            else
            {
                if (CheckBit(castling, 0)) // Queen's side white
                    fen += 'Q';
                if (CheckBit(castling, 1)) // King's side white
                    fen += 'K';
                if (CheckBit(castling, 2)) // Queen's side black
                    fen += 'q';
                if (CheckBit(castling, 3)) // King's side black
                    fen += 'k';
            }
            fen += " ";

            // Add possible en passant to our FEN
            if (specialMoves[BENP] != 0)
            {
                fen += letters[GetFirstSetBit(specialMoves[BENP])] + "6";
            }
            else if (specialMoves[WENP] != 0)
            {
                fen += letters[GetFirstSetBit(specialMoves[WENP])] + "3";
            }
            else
            {
                fen += "-";
            }

            // Add half move clock and full move number
            fen += $" 0 1";

            return fen;
        }
    }
}