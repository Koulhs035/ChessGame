using System;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class MainMenu : Form
    {
        Utility utility = new Utility();
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
            utility.timeControl = int.Parse(tag);
        }

        private void player1TextBox_TextChanged(object sender, EventArgs e)
        {
            utility.pNames[0] = player1TextBox.Text;
        }

        private void player2TextBox_TextChanged(object sender, EventArgs e)
        {
            utility.pNames[1] = player2TextBox.Text;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            utility.engineToPlay = false;
            ChessboardUI chessboardUI = new ChessboardUI(this, utility);
            chessboardUI.Show();
            Hide();
        }

        private void playAiButton_Click(object sender, EventArgs e)
        {
            utility.engineToPlay = true;
            gameSettingsPanel.Visible = false;
            ChessboardUI chessboardUI = new ChessboardUI(this, utility);
            chessboardUI.Show();
            Hide();

        }
    }
}
