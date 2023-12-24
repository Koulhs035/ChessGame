using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class MainMenu : Form
    {
        private string connectionString = "Data Source=Database.db;Version=3;";
        public MainMenu()
        {
            InitializeComponent();
        }

        private void PlayLocalButton_Click(object sender, EventArgs e)
        {
            ChessBoard chessBoard = new ChessBoard();
            chessBoard.generateLegalMoves();
            string allLegal = "";
            foreach (string move in chessBoard.LegalMoves)
            {
                allLegal += move + '\n';
            }
            MessageBox.Show(allLegal);
        }
    }
}
