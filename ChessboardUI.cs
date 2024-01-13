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
                    panel.Size = new System.Drawing.Size(squareSize, squareSize);
                    panel.Location = new Point(row * squareSize, col * squareSize);
                    panel.Tag = $"{ChessBoard.letters[row]}{8 - col} "; // Making sure it has the correct notation so we can check for legal moves later
                    panel.BorderStyle = BorderStyle.Fixed3D;
                    panel.BringToFront();

                    char curPiece = chessboard.Chessboard[7 - col][row];
                    string AddPiece = char.IsUpper(curPiece) ? "w" : "b";
                    var resourceManager = Properties.Resources.ResourceManager;
                    switch (char.ToUpper(curPiece))
                    {
                        case 'P':
                            AddPiece += "Pawn";
                            break;
                        case 'K':
                            AddPiece += "King";
                            break;
                        case 'Q':
                            AddPiece += "Queen";
                            break;
                        case 'R':
                            AddPiece += "Rook";
                            break;
                        case 'B':
                            AddPiece += "Bishop";
                            break;
                        case 'N':
                            AddPiece += "Knight";
                            break;
                    }

                    panel.BackgroundImage = (Bitmap)resourceManager.GetObject(AddPiece);
                    panel.BackgroundImageLayout = ImageLayout.Zoom;

                    squareList.Add(panel);
                    ChessboardSquaresPanel.Controls.Add(panel);
                }
            }




        }

        private void panel_MouseEnter(object sender, EventArgs e)
        {
            Panel curPanel = sender as Panel;
            label1.Text = curPanel.Tag.ToString();
            curPanel.BackColor = Color.Green;
        }
        private void panel_MouseLeave(object sender, EventArgs e)
        {
            Panel curPanel = sender as Panel;
            curPanel.BackColor = Color.Transparent;
        }

    }
}
