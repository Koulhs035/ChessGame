namespace ChessGame
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.gameSettingsPanel = new System.Windows.Forms.Panel();
            this.playButton = new System.Windows.Forms.Button();
            this.min30Button = new System.Windows.Forms.Button();
            this.min15Button = new System.Windows.Forms.Button();
            this.min10Button = new System.Windows.Forms.Button();
            this.min7Button = new System.Windows.Forms.Button();
            this.min5Button = new System.Windows.Forms.Button();
            this.min3Button = new System.Windows.Forms.Button();
            this.min2Button = new System.Windows.Forms.Button();
            this.min1Button = new System.Windows.Forms.Button();
            this.rapidLabel = new System.Windows.Forms.Label();
            this.BlitzLabel = new System.Windows.Forms.Label();
            this.sec30Button = new System.Windows.Forms.Button();
            this.BulletLabel = new System.Windows.Forms.Label();
            this.player2TextBox = new System.Windows.Forms.TextBox();
            this.player2Label = new System.Windows.Forms.Label();
            this.player1TextBox = new System.Windows.Forms.TextBox();
            this.player1Label = new System.Windows.Forms.Label();
            this.gameSettingsAccentPanel = new System.Windows.Forms.Panel();
            this.playAiButton = new System.Windows.Forms.Button();
            this.PlayLocalButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gameSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // gameSettingsPanel
            // 
            this.gameSettingsPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gameSettingsPanel.AutoSize = true;
            this.gameSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.gameSettingsPanel.Controls.Add(this.playButton);
            this.gameSettingsPanel.Controls.Add(this.min30Button);
            this.gameSettingsPanel.Controls.Add(this.min15Button);
            this.gameSettingsPanel.Controls.Add(this.min10Button);
            this.gameSettingsPanel.Controls.Add(this.min7Button);
            this.gameSettingsPanel.Controls.Add(this.min5Button);
            this.gameSettingsPanel.Controls.Add(this.min3Button);
            this.gameSettingsPanel.Controls.Add(this.min2Button);
            this.gameSettingsPanel.Controls.Add(this.min1Button);
            this.gameSettingsPanel.Controls.Add(this.rapidLabel);
            this.gameSettingsPanel.Controls.Add(this.BlitzLabel);
            this.gameSettingsPanel.Controls.Add(this.sec30Button);
            this.gameSettingsPanel.Controls.Add(this.BulletLabel);
            this.gameSettingsPanel.Controls.Add(this.player2TextBox);
            this.gameSettingsPanel.Controls.Add(this.player2Label);
            this.gameSettingsPanel.Controls.Add(this.player1TextBox);
            this.gameSettingsPanel.Controls.Add(this.player1Label);
            this.gameSettingsPanel.Controls.Add(this.gameSettingsAccentPanel);
            this.gameSettingsPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gameSettingsPanel.ForeColor = System.Drawing.Color.White;
            this.gameSettingsPanel.Location = new System.Drawing.Point(486, 324);
            this.gameSettingsPanel.Name = "gameSettingsPanel";
            this.gameSettingsPanel.Size = new System.Drawing.Size(388, 686);
            this.gameSettingsPanel.TabIndex = 2;
            this.gameSettingsPanel.Visible = false;
            // 
            // playButton
            // 
            this.playButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.playButton.BackgroundImage = global::ChessGame.Properties.Resources.smooth_gray_wooden_textured_background_vector;
            this.playButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.playButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.playButton.FlatAppearance.BorderSize = 3;
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.Font = new System.Drawing.Font("Ebrima", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.playButton.Location = new System.Drawing.Point(134, 233);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(117, 39);
            this.playButton.TabIndex = 3;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // min30Button
            // 
            this.min30Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.min30Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.min30Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.min30Button.ForeColor = System.Drawing.Color.White;
            this.min30Button.Location = new System.Drawing.Point(299, 204);
            this.min30Button.Name = "min30Button";
            this.min30Button.Size = new System.Drawing.Size(75, 23);
            this.min30Button.TabIndex = 24;
            this.min30Button.Tag = "1800";
            this.min30Button.Text = "30 min";
            this.min30Button.UseVisualStyleBackColor = true;
            this.min30Button.Click += new System.EventHandler(this.getTag);
            // 
            // min15Button
            // 
            this.min15Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.min15Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.min15Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.min15Button.ForeColor = System.Drawing.Color.White;
            this.min15Button.Location = new System.Drawing.Point(158, 204);
            this.min15Button.Name = "min15Button";
            this.min15Button.Size = new System.Drawing.Size(75, 23);
            this.min15Button.TabIndex = 23;
            this.min15Button.Tag = "900";
            this.min15Button.Text = "15 min";
            this.min15Button.UseVisualStyleBackColor = true;
            this.min15Button.Click += new System.EventHandler(this.getTag);
            // 
            // min10Button
            // 
            this.min10Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.min10Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.min10Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.min10Button.ForeColor = System.Drawing.Color.White;
            this.min10Button.Location = new System.Drawing.Point(17, 204);
            this.min10Button.Name = "min10Button";
            this.min10Button.Size = new System.Drawing.Size(75, 23);
            this.min10Button.TabIndex = 22;
            this.min10Button.Tag = "600";
            this.min10Button.Text = "10 min";
            this.min10Button.UseVisualStyleBackColor = true;
            this.min10Button.Click += new System.EventHandler(this.getTag);
            // 
            // min7Button
            // 
            this.min7Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.min7Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.min7Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.min7Button.ForeColor = System.Drawing.Color.White;
            this.min7Button.Location = new System.Drawing.Point(299, 154);
            this.min7Button.Name = "min7Button";
            this.min7Button.Size = new System.Drawing.Size(75, 23);
            this.min7Button.TabIndex = 21;
            this.min7Button.Tag = "420";
            this.min7Button.Text = "7 min";
            this.min7Button.UseVisualStyleBackColor = true;
            this.min7Button.Click += new System.EventHandler(this.getTag);
            // 
            // min5Button
            // 
            this.min5Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.min5Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.min5Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.min5Button.ForeColor = System.Drawing.Color.White;
            this.min5Button.Location = new System.Drawing.Point(158, 154);
            this.min5Button.Name = "min5Button";
            this.min5Button.Size = new System.Drawing.Size(75, 23);
            this.min5Button.TabIndex = 20;
            this.min5Button.Tag = "300";
            this.min5Button.Text = "5 min";
            this.min5Button.UseVisualStyleBackColor = true;
            this.min5Button.Click += new System.EventHandler(this.getTag);
            // 
            // min3Button
            // 
            this.min3Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.min3Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.min3Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.min3Button.ForeColor = System.Drawing.Color.White;
            this.min3Button.Location = new System.Drawing.Point(17, 154);
            this.min3Button.Name = "min3Button";
            this.min3Button.Size = new System.Drawing.Size(75, 23);
            this.min3Button.TabIndex = 19;
            this.min3Button.Tag = "180";
            this.min3Button.Text = "3 min";
            this.min3Button.UseVisualStyleBackColor = true;
            this.min3Button.Click += new System.EventHandler(this.getTag);
            // 
            // min2Button
            // 
            this.min2Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.min2Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.min2Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.min2Button.ForeColor = System.Drawing.Color.White;
            this.min2Button.Location = new System.Drawing.Point(299, 104);
            this.min2Button.Name = "min2Button";
            this.min2Button.Size = new System.Drawing.Size(75, 23);
            this.min2Button.TabIndex = 18;
            this.min2Button.Tag = "120";
            this.min2Button.Text = "2 min";
            this.min2Button.UseVisualStyleBackColor = true;
            this.min2Button.Click += new System.EventHandler(this.getTag);
            // 
            // min1Button
            // 
            this.min1Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.min1Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.min1Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.min1Button.ForeColor = System.Drawing.Color.White;
            this.min1Button.Location = new System.Drawing.Point(158, 104);
            this.min1Button.Name = "min1Button";
            this.min1Button.Size = new System.Drawing.Size(75, 23);
            this.min1Button.TabIndex = 17;
            this.min1Button.Tag = "60";
            this.min1Button.Text = "1 min";
            this.min1Button.UseVisualStyleBackColor = true;
            this.min1Button.Click += new System.EventHandler(this.getTag);
            // 
            // rapidLabel
            // 
            this.rapidLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rapidLabel.AutoSize = true;
            this.rapidLabel.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rapidLabel.Location = new System.Drawing.Point(166, 180);
            this.rapidLabel.Name = "rapidLabel";
            this.rapidLabel.Size = new System.Drawing.Size(58, 21);
            this.rapidLabel.TabIndex = 16;
            this.rapidLabel.Text = "Rapid:";
            // 
            // BlitzLabel
            // 
            this.BlitzLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BlitzLabel.AutoSize = true;
            this.BlitzLabel.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlitzLabel.Location = new System.Drawing.Point(166, 130);
            this.BlitzLabel.Name = "BlitzLabel";
            this.BlitzLabel.Size = new System.Drawing.Size(48, 21);
            this.BlitzLabel.TabIndex = 12;
            this.BlitzLabel.Text = "Blitz:";
            // 
            // sec30Button
            // 
            this.sec30Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sec30Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sec30Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sec30Button.ForeColor = System.Drawing.Color.White;
            this.sec30Button.Location = new System.Drawing.Point(17, 104);
            this.sec30Button.Name = "sec30Button";
            this.sec30Button.Size = new System.Drawing.Size(75, 23);
            this.sec30Button.TabIndex = 3;
            this.sec30Button.Tag = "30";
            this.sec30Button.Text = "30 sec";
            this.sec30Button.UseVisualStyleBackColor = true;
            this.sec30Button.Click += new System.EventHandler(this.getTag);
            // 
            // BulletLabel
            // 
            this.BulletLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BulletLabel.AutoSize = true;
            this.BulletLabel.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BulletLabel.Location = new System.Drawing.Point(166, 80);
            this.BulletLabel.Name = "BulletLabel";
            this.BulletLabel.Size = new System.Drawing.Size(59, 21);
            this.BulletLabel.TabIndex = 8;
            this.BulletLabel.Text = "Bullet:";
            // 
            // player2TextBox
            // 
            this.player2TextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.player2TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.player2TextBox.ForeColor = System.Drawing.Color.White;
            this.player2TextBox.Location = new System.Drawing.Point(166, 48);
            this.player2TextBox.Name = "player2TextBox";
            this.player2TextBox.Size = new System.Drawing.Size(127, 22);
            this.player2TextBox.TabIndex = 7;
            // 
            // player2Label
            // 
            this.player2Label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.player2Label.AutoSize = true;
            this.player2Label.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player2Label.ForeColor = System.Drawing.Color.White;
            this.player2Label.Location = new System.Drawing.Point(88, 49);
            this.player2Label.Name = "player2Label";
            this.player2Label.Size = new System.Drawing.Size(75, 21);
            this.player2Label.TabIndex = 6;
            this.player2Label.Text = "Player 2:";
            // 
            // player1TextBox
            // 
            this.player1TextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.player1TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.player1TextBox.ForeColor = System.Drawing.Color.White;
            this.player1TextBox.Location = new System.Drawing.Point(166, 16);
            this.player1TextBox.Name = "player1TextBox";
            this.player1TextBox.Size = new System.Drawing.Size(127, 22);
            this.player1TextBox.TabIndex = 5;
            // 
            // player1Label
            // 
            this.player1Label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.player1Label.AutoSize = true;
            this.player1Label.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player1Label.ForeColor = System.Drawing.Color.White;
            this.player1Label.Location = new System.Drawing.Point(88, 16);
            this.player1Label.Name = "player1Label";
            this.player1Label.Size = new System.Drawing.Size(75, 21);
            this.player1Label.TabIndex = 4;
            this.player1Label.Text = "Player 1:";
            // 
            // gameSettingsAccentPanel
            // 
            this.gameSettingsAccentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.gameSettingsAccentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameSettingsAccentPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.gameSettingsAccentPanel.Location = new System.Drawing.Point(0, 0);
            this.gameSettingsAccentPanel.Name = "gameSettingsAccentPanel";
            this.gameSettingsAccentPanel.Size = new System.Drawing.Size(388, 5);
            this.gameSettingsAccentPanel.TabIndex = 3;
            // 
            // playAiButton
            // 
            this.playAiButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.playAiButton.BackgroundImage = global::ChessGame.Properties.Resources.smooth_gray_wooden_textured_background_vector;
            this.playAiButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.playAiButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playAiButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.playAiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playAiButton.Font = new System.Drawing.Font("Ebrima", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playAiButton.Location = new System.Drawing.Point(571, 242);
            this.playAiButton.Name = "playAiButton";
            this.playAiButton.Size = new System.Drawing.Size(226, 57);
            this.playAiButton.TabIndex = 1;
            this.playAiButton.Text = "Play AI";
            this.playAiButton.UseVisualStyleBackColor = true;
            this.playAiButton.Click += new System.EventHandler(this.playAiButton_Click);
            // 
            // PlayLocalButton
            // 
            this.PlayLocalButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PlayLocalButton.BackColor = System.Drawing.Color.White;
            this.PlayLocalButton.BackgroundImage = global::ChessGame.Properties.Resources.smooth_gray_wooden_textured_background_vector;
            this.PlayLocalButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayLocalButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PlayLocalButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.PlayLocalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayLocalButton.Font = new System.Drawing.Font("Ebrima", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayLocalButton.Location = new System.Drawing.Point(571, 160);
            this.PlayLocalButton.Name = "PlayLocalButton";
            this.PlayLocalButton.Size = new System.Drawing.Size(226, 57);
            this.PlayLocalButton.TabIndex = 0;
            this.PlayLocalButton.Text = "Play Versus";
            this.PlayLocalButton.UseVisualStyleBackColor = false;
            this.PlayLocalButton.Click += new System.EventHandler(this.PlayLocalButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::ChessGame.Properties.Resources.bgPieces;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-5, 388);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(459, 337);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::ChessGame.Properties.Resources.bgPieces2;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(912, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(454, 238);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(460, -17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 742);
            this.panel1.TabIndex = 12;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(1359, 722);
            this.Controls.Add(this.gameSettingsPanel);
            this.Controls.Add(this.playAiButton);
            this.Controls.Add(this.PlayLocalButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.gameSettingsPanel.ResumeLayout(false);
            this.gameSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PlayLocalButton;
        private System.Windows.Forms.Button playAiButton;
        private System.Windows.Forms.Panel gameSettingsPanel;
        private System.Windows.Forms.Panel gameSettingsAccentPanel;
        private System.Windows.Forms.TextBox player1TextBox;
        private System.Windows.Forms.Label player1Label;
        private System.Windows.Forms.TextBox player2TextBox;
        private System.Windows.Forms.Label player2Label;
        private System.Windows.Forms.Button sec30Button;
        private System.Windows.Forms.Label BulletLabel;
        private System.Windows.Forms.Label rapidLabel;
        private System.Windows.Forms.Label BlitzLabel;
        private System.Windows.Forms.Button min30Button;
        private System.Windows.Forms.Button min15Button;
        private System.Windows.Forms.Button min10Button;
        private System.Windows.Forms.Button min7Button;
        private System.Windows.Forms.Button min5Button;
        private System.Windows.Forms.Button min3Button;
        private System.Windows.Forms.Button min2Button;
        private System.Windows.Forms.Button min1Button;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
    }
}

