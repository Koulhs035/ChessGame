namespace ChessGame
{
    partial class ChessboardUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChessboardUI));
            this.ChessboardSquaresPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChessboardSquaresPanel
            // 
            this.ChessboardSquaresPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ChessboardSquaresPanel.BackColor = System.Drawing.Color.Transparent;
            this.ChessboardSquaresPanel.BackgroundImage = global::ChessGame.Properties.Resources.chessboard2;
            this.ChessboardSquaresPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ChessboardSquaresPanel.Location = new System.Drawing.Point(405, 110);
            this.ChessboardSquaresPanel.Name = "ChessboardSquaresPanel";
            this.ChessboardSquaresPanel.Size = new System.Drawing.Size(600, 600);
            this.ChessboardSquaresPanel.TabIndex = 0;
            this.ChessboardSquaresPanel.TabStop = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(402, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // ChessboardUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1410, 816);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChessboardSquaresPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChessboardUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChessboardUI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChessboardUI_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel ChessboardSquaresPanel;
        private System.Windows.Forms.Label label1;
    }
}