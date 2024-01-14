using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class ChessboardUI : Form
    {
        MainMenu mainMenu;
        public ChessboardUI(MainMenu menu, string connStr)
        {
            InitializeComponent();
            InitializeBoard();
            mainMenu = menu;
        }

        private void backToMMButton_Click(object sender, EventArgs e)
        {
            //  mainMenu.Show();
            //  this.Close();
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

                    panel.MouseEnter += panel_MouseEnter;
                    panel.MouseLeave += panel_MouseLeave;
                    panel.MouseClick += panel_MouseClick;

                    panel.Size = new System.Drawing.Size(squareSize, squareSize);
                    panel.Location = new Point(row * squareSize, col * squareSize);
                    panel.Tag = $"{ChessBoard.letters[row]}{8 - col}"; // Making sure it has the correct notation so we can check for legal moves later
                    panel.BorderStyle = BorderStyle.Fixed3D;
                    panel.BringToFront();

                    char curPiece = chessboard.Chessboard[7 - col][row];



                    squareList.Add(panel);
                    ChessboardSquaresPanel.Controls.Add(panel);
                }
            }


            updateChessboardVisual();


        }

        private string FetchPieceImage(char curPiece)
        {
            // This makes the correct Path to get from the resources
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




        private void panel_MouseEnter(object sender, EventArgs e)
        {
            Panel curPanel = sender as Panel;
            label1.Text = curPanel.Tag.ToString();
            curPanel.BackColor = Color.FromArgb(80, Color.Navy);
        }

        string toMove = string.Empty;
        private void panel_MouseClick(object sender, EventArgs e)
        {

            chessboard.generateLegalMoves();
            Panel curPanel = sender as Panel;
            string location = curPanel.Tag.ToString();

            char colLetter = location[0];
            int row;
            int.TryParse(location.Substring(1), out row);
            int col = ChessBoard.getNumberFromCol(colLetter);
            char selectedPiece = chessboard.Chessboard[row - 1][col];

            string moveExecuted;
            if (toMove == string.Empty)
            {
                toMove = location;
            }
            else
            {
                moveExecuted = toMove + " " + location;
                if (chessboard.LegalMoves.Contains(moveExecuted))
                {
                    chessboard.ExecuteMove(moveExecuted);
                    updateChessboardVisual();
                    chessboard.turn = !chessboard.turn;
                }
                toMove = string.Empty;
            }


        }

        private void updateChessboardVisual()
        {
            foreach (Panel panel in squareList)
            {
                panel.BackgroundImageLayout = ImageLayout.Zoom;

                BackgroundImage = null;
            }


            for (int row = 0; row < 8; row++) // This goes through the array of the board
            {
                for (int col = 0; col < 8; col++)
                {
                    int index =  (7-row + 8 * col);
                    char curPiece = chessboard.Chessboard[row][col];

                    string addPiece = FetchPieceImage(curPiece);

                    var resourceManager = Properties.Resources.ResourceManager;
                    squareList[index].BackgroundImage = (Bitmap)resourceManager.GetObject(addPiece);

                }
            }

        }

        private void panel_MouseLeave(object sender, EventArgs e)
        {
            Panel curPanel = sender as Panel;
            curPanel.BackColor = Color.Transparent;


        }



    }
}
