using System;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class MainMenu : Form
    {
        int timeControl= 600;
        string[] pNames = new string[2];
        private string connectionString = "Data Source=Database.db;Version=3;";
        public MainMenu()
        {
            InitializeComponent();
        }

        private void PlayLocalButton_Click(object sender, EventArgs e)
        {
          
          
          
            gameSettingsPanel.Visible = true;

        }

    
        private void getTag(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string tag = button.Tag as string;
            timeControl = int.Parse(tag);
        }

        private void player1TextBox_TextChanged(object sender, EventArgs e)
        {
            pNames[0] = player1TextBox.Text;
        }

        private void player2TextBox_TextChanged(object sender, EventArgs e)
        {
            pNames[1] = player2TextBox.Text;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            ChessboardUI chessboardUI = new ChessboardUI(this, connectionString, timeControl, pNames);
            chessboardUI.Show();
            Hide();
        }
    }



}
