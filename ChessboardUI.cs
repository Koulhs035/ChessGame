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
        public ChessboardUI(MainMenu menu, Utility util)
        {
            InitializeComponent();
            InitializeBoard(); // Fixes the squares

            int time = util.timeControl;
            whiteTime = time;
            blackTime = time;

            mainMenu = menu;
            User1Label.Text = utility.pNames[0];
            User2Label.Text = utility.pNames[1];
            whiteSideTimerLabel.Text = Utility.FormatTime(time);
            blackSideTimerLabel.Text = Utility.FormatTime(time);
        }

        private void ChessboardUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainMenu.Close();
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

        private string FetchPieceImage(char curPiece)
        {
            // This makes the correct Path to get from the resources so we can update the icon
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

        string toMove = string.Empty;

        private void panel_MouseClick(object sender, EventArgs e)
        {
            chessboard.generateLegalMoves();
            Panel curPanel = sender as Panel;
           
            string location = curPanel.Tag.ToString();

            int row = (int)Char.GetNumericValue(location[1]);
            int col = ChessBoard.getNumberFromCol(location[0]);
            
            string moveExecuted;
            if (toMove == string.Empty)
            {
                toMove = location;
            }
            else
            {
                moveExecuted = $"{toMove} {location}";

                if (chessboard.LegalMoves.Contains(moveExecuted))
                {
                    EnableTimerIfNotEnabled();
                    chessboard.ExecuteMove(moveExecuted);
                    updateChessboardVisual(moveExecuted);
                    chessboard.turn = !chessboard.turn;

                    if (chessboard.specialMove) updateSpecialMoveVisuals();
                    fixTimerColors();

                    //TODO: Add sound effects
                }

                toMove = string.Empty;
            }
        }

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
            int curCol = ChessBoard.getNumberFromCol(sepMoves[0][0]); ;
            int curRow = int.Parse(sepMoves[0][1].ToString()) - 1;
            int curIndex = 7 - curRow + 8 * curCol;


            int tarCol = ChessBoard.getNumberFromCol(sepMoves[1][0]);
            int tarRow = int.Parse(sepMoves[1][1].ToString()) - 1;
            int tarIndex = 7 - tarRow + 8 * tarCol;

            // Fix the icons accordingly 

            squareList[tarIndex].BackgroundImage = squareList[curIndex].BackgroundImage;
            squareList[curIndex].BackgroundImage = null;
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
    }
}
