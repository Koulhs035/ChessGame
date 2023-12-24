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
            this.PlayLocalButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PlayLocalButton
            // 
            this.PlayLocalButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PlayLocalButton.Location = new System.Drawing.Point(339, 152);
            this.PlayLocalButton.Name = "PlayLocalButton";
            this.PlayLocalButton.Size = new System.Drawing.Size(75, 23);
            this.PlayLocalButton.TabIndex = 0;
            this.PlayLocalButton.Text = "Play Local";
            this.PlayLocalButton.UseVisualStyleBackColor = true;
            this.PlayLocalButton.Click += new System.EventHandler(this.PlayLocalButton_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(805, 464);
            this.Controls.Add(this.PlayLocalButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PlayLocalButton;
    }
}

