// Ignore Spelling: util

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class ChessboardUI : Form
    {
        MainMenu mainMenu;
        int whiteTime;
        int blackTime;
        Utility utility = new Utility();
        //---------------------------------------------Start Board---------------------------------------------//
        public ChessboardUI(MainMenu menu, Utility util)
        {
            InitializeComponent();
            utility = util;

            int time = utility.timeControl;
            whiteTime = time;
            blackTime = time;

            mainMenu = menu;
            User1Label.Text = utility.pNames[0];
            User2Label.Text = utility.pNames[1];
            whiteSideTimerLabel.Text = Utility.FormatTime(time);
            blackSideTimerLabel.Text = Utility.FormatTime(time);
        }

        private void ChessboardUI_Load(object sender, EventArgs e)
        {
            InitializeBoard(); // Fixes the squares
            utility.StartStockfish();
        }

        List<Panel> squareList = new List<Panel>();
        ChessBoard chessboard = new ChessBoard();
        private void InitializeBoard()
        {
            int squareSize = ChessboardSquaresPanel.Size.Width / 8; // Since it's square we don't need height as well

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Panel panel = new Panel();

                    panel.MouseClick += panel_MouseClick;

                    panel.Size = new System.Drawing.Size(squareSize, squareSize);
                    panel.Location = new Point(row * squareSize, col * squareSize);
                    panel.Tag = $"{ChessBoard.letters[row]}{8 - col}"; // Making sure it has the correct notation so we can check for legal moves later
                    panel.BorderStyle = BorderStyle.Fixed3D;

                    panel.BackgroundImageLayout = ImageLayout.Zoom;


                    squareList.Add(panel);
                    ChessboardSquaresPanel.Controls.Add(panel);
                }
            }
            updateSpecialMoveVisuals();
        }

        //---------------------------------------------Movement---------------------------------------------//
        string place1 = string.Empty;
        char prevPiece;
        private void panel_MouseClick(object sender, EventArgs e)
        {
           
            Panel curPanel = sender as Panel;

            string location = curPanel.Tag.ToString();

            int row = (int)Char.GetNumericValue(location[1]);
            int col = ChessBoard.getNumberFromCol(location[0]);

            char curPiece = chessboard.Chessboard[row - 1][col];
            if (place1 == string.Empty && curPiece != ' ' && Char.IsUpper(curPiece) == chessboard.turn)
            {
                place1 = location;
            }
            else if (place1 != string.Empty) 
            {
                string executeMove = place1 + location;
                chessboard.generateLegalMoves(place1);
                if (chessboard.LegalMoves.Contains(executeMove))
                {   // Here the move is executed
                    chessboard.ExecuteMove(executeMove);
                    MoveUpdates(executeMove);
                }
                place1 = string.Empty;
            }
        }



        //---------------------------------------------Tools---------------------------------------------//
        private void EnableTimerIfNotEnabled()
        {
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
            }
        }

        private void updateSpecialMoveVisuals()
        {   // This copies the array and changes only the background image to the correct account 
            var resourceManager = Properties.Resources.ResourceManager;
            char curPiece;
            string addPiece;
            int index;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    curPiece = chessboard.Chessboard[row][col];

                    index = 7 - row + 8 * col;
                    if (curPiece == ' ') squareList[index].BackgroundImage = null;
                    else
                    {
                        addPiece = FetchPieceImage(curPiece);
                        squareList[index].BackgroundImage = (Bitmap)resourceManager.GetObject(addPiece);
                    }
                }
            }
        }

        private void fixTimerColors()
        {
            if (chessboard.turn)
            {
                whiteSideTimerLabel.ForeColor = Color.Black;
                whiteSideTimerPanel.BackColor = Color.White;

                blackSideTimerLabel.ForeColor = Color.White;
                blackSideTimerPanel.BackColor = Color.Black;
            }
            else
            {
                blackSideTimerLabel.ForeColor = Color.Black;
                blackSideTimerPanel.BackColor = Color.White;

                whiteSideTimerLabel.ForeColor = Color.White;
                whiteSideTimerPanel.BackColor = Color.Black;
            }
        }

        private void updateChessboardVisual(string move)
        {
            string[] sepMoves = move.Split(' ');

            // Get the coordinates for the moves selected
            int curCol = ChessBoard.getNumberFromCol(move[0]); ;
            int curRow = int.Parse(move[1].ToString()) - 1;
            int curIndex = 7 - curRow + 8 * curCol;


            int tarCol = ChessBoard.getNumberFromCol(move[2]);
            int tarRow = int.Parse(move[3].ToString()) - 1;
            int tarIndex = 7 - tarRow + 8 * tarCol;

            // Fix the icons accordingly 

            squareList[tarIndex].BackgroundImage = squareList[curIndex].BackgroundImage;
            squareList[curIndex].BackgroundImage = null;
        }

        private string FetchPieceImage(char curPiece)
        {
            // This makes the correct Path to get from Resources so we can update the icon
            string piece = char.IsUpper(curPiece) ? "w" : "b";
            switch (char.ToUpper(curPiece))
            {
                case 'P':
                    piece += "Pawn";
                    break;
                case 'K':
                    piece += "King";
                    break;
                case 'Q':
                    piece += "Queen";
                    break;
                case 'R':
                    piece += "Rook";
                    break;
                case 'B':
                    piece += "Bishop";
                    break;
                case 'N':
                    piece += "Knight";
                    break;
            }
            return piece;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (chessboard.turn)
            {
                whiteTime -= 1;
                whiteSideTimerLabel.Text = Utility.FormatTime(whiteTime);
                if (whiteTime == 0)
                    timer1.Stop();
            }
            else
            {

                blackTime -= 1;
                blackSideTimerLabel.Text = Utility.FormatTime(blackTime);
                if (blackTime == 0)
                    timer1.Stop();
            }
        }

        private void ChessboardUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainMenu.Show();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MoveUpdates(string executeMove)
        {
            // UI Updates
            updateChessboardVisual(executeMove);
            if (chessboard.specialMove) updateSpecialMoveVisuals();
            chessboard.turn = !chessboard.turn;

            // Timer handling
            EnableTimerIfNotEnabled();
            fixTimerColors();
        }

        private void p1ResignButton_Click(object sender, EventArgs e)
        {

        }
    }
}
