namespace CollapseGameFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components?.Dispose();
            }
            EndGame();
            Thread.Sleep(300);
            base.Dispose(disposing);
            _client?.Dispose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.continueGame = new System.Windows.Forms.Button();
            this.quitGame = new System.Windows.Forms.Button();
            this.pause = new System.Windows.Forms.Button();
            this.players = new System.Windows.Forms.ListBox();
            this.gameResultMessage = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.GroupBox();
            this.gameField = new System.Windows.Forms.GroupBox();
            this.gameResultDialog = new System.Windows.Forms.GroupBox();
            this.menu.SuspendLayout();
            this.gameField.SuspendLayout();
            this.gameResultDialog.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.startButton.Location = new System.Drawing.Point(400, 240);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(240, 84);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start Game";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 82);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(104, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 82);
            this.button3.TabIndex = 2;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(192, 30);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(82, 82);
            this.button4.TabIndex = 3;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(280, 30);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(82, 82);
            this.button5.TabIndex = 4;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(368, 30);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(82, 82);
            this.button6.TabIndex = 5;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(16, 118);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(82, 82);
            this.button7.TabIndex = 6;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(104, 118);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(82, 82);
            this.button8.TabIndex = 7;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(192, 118);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(82, 82);
            this.button9.TabIndex = 8;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(280, 118);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(82, 82);
            this.button10.TabIndex = 9;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(368, 118);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(82, 82);
            this.button11.TabIndex = 10;
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(16, 206);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(82, 82);
            this.button12.TabIndex = 11;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(104, 206);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(82, 82);
            this.button13.TabIndex = 12;
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(192, 206);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(82, 82);
            this.button14.TabIndex = 13;
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(280, 206);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(82, 82);
            this.button15.TabIndex = 14;
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(368, 206);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(82, 82);
            this.button16.TabIndex = 15;
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(16, 294);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(82, 82);
            this.button17.TabIndex = 16;
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(104, 294);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(82, 82);
            this.button18.TabIndex = 17;
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(192, 294);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(82, 82);
            this.button19.TabIndex = 18;
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(280, 294);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(82, 82);
            this.button20.TabIndex = 19;
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(368, 294);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(82, 82);
            this.button21.TabIndex = 20;
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(16, 382);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(82, 82);
            this.button22.TabIndex = 21;
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button23
            // 
            this.button23.Location = new System.Drawing.Point(104, 382);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(82, 82);
            this.button23.TabIndex = 22;
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // button24
            // 
            this.button24.Location = new System.Drawing.Point(192, 382);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(82, 82);
            this.button24.TabIndex = 23;
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new System.EventHandler(this.button24_Click);
            // 
            // button25
            // 
            this.button25.Location = new System.Drawing.Point(280, 382);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(82, 82);
            this.button25.TabIndex = 24;
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // button26
            // 
            this.button26.Location = new System.Drawing.Point(368, 382);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(82, 82);
            this.button26.TabIndex = 25;
            this.button26.UseVisualStyleBackColor = true;
            this.button26.Click += new System.EventHandler(this.button26_Click);
            // 
            // continueGame
            // 
            this.continueGame.Location = new System.Drawing.Point(78, 22);
            this.continueGame.Margin = new System.Windows.Forms.Padding(4);
            this.continueGame.Name = "continueGame";
            this.continueGame.Size = new System.Drawing.Size(161, 76);
            this.continueGame.TabIndex = 27;
            this.continueGame.Text = "Continue";
            this.continueGame.UseVisualStyleBackColor = true;
            this.continueGame.Click += new System.EventHandler(this.continueGame_Click);
            // 
            // quitGame
            // 
            this.quitGame.Location = new System.Drawing.Point(78, 107);
            this.quitGame.Margin = new System.Windows.Forms.Padding(4);
            this.quitGame.Name = "quitGame";
            this.quitGame.Size = new System.Drawing.Size(161, 76);
            this.quitGame.TabIndex = 28;
            this.quitGame.Text = "Quit Game";
            this.quitGame.UseVisualStyleBackColor = true;
            this.quitGame.Click += new System.EventHandler(this.quitGame_Click);
            // 
            // pause
            // 
            this.pause.Location = new System.Drawing.Point(385, 0);
            this.pause.Margin = new System.Windows.Forms.Padding(4);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(107, 34);
            this.pause.TabIndex = 29;
            this.pause.Text = "|| Pause";
            this.pause.UseVisualStyleBackColor = true;
            this.pause.Visible = false;
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // players
            // 
            this.players.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.players.Enabled = false;
            this.players.FormattingEnabled = true;
            this.players.ItemHeight = 25;
            this.players.Location = new System.Drawing.Point(12, 11);
            this.players.Name = "players";
            this.players.Size = new System.Drawing.Size(223, 504);
            this.players.TabIndex = 30;
            this.players.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.players_DrawItem);
            // 
            // gameResultMessage
            // 
            this.gameResultMessage.Location = new System.Drawing.Point(12, 44);
            this.gameResultMessage.Name = "gameResultMessage";
            this.gameResultMessage.Size = new System.Drawing.Size(340, 129);
            this.gameResultMessage.TabIndex = 31;
            this.gameResultMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menu
            // 
            this.menu.Controls.Add(this.quitGame);
            this.menu.Controls.Add(this.continueGame);
            this.menu.Location = new System.Drawing.Point(250, 190);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(308, 202);
            this.menu.TabIndex = 32;
            this.menu.TabStop = false;
            this.menu.Text = "Menu";
            this.menu.Visible = false;
            // 
            // gameField
            // 
            this.gameField.Controls.Add(this.button26);
            this.gameField.Controls.Add(this.button2);
            this.gameField.Controls.Add(this.button3);
            this.gameField.Controls.Add(this.button4);
            this.gameField.Controls.Add(this.button5);
            this.gameField.Controls.Add(this.button22);
            this.gameField.Controls.Add(this.button6);
            this.gameField.Controls.Add(this.button23);
            this.gameField.Controls.Add(this.button21);
            this.gameField.Controls.Add(this.button24);
            this.gameField.Controls.Add(this.button20);
            this.gameField.Controls.Add(this.button25);
            this.gameField.Controls.Add(this.button19);
            this.gameField.Controls.Add(this.button18);
            this.gameField.Controls.Add(this.button7);
            this.gameField.Controls.Add(this.button17);
            this.gameField.Controls.Add(this.button8);
            this.gameField.Controls.Add(this.button16);
            this.gameField.Controls.Add(this.button9);
            this.gameField.Controls.Add(this.button15);
            this.gameField.Controls.Add(this.button10);
            this.gameField.Controls.Add(this.button14);
            this.gameField.Controls.Add(this.button11);
            this.gameField.Controls.Add(this.button13);
            this.gameField.Controls.Add(this.button12);
            this.gameField.Location = new System.Drawing.Point(256, 41);
            this.gameField.Name = "gameField";
            this.gameField.Size = new System.Drawing.Size(465, 474);
            this.gameField.TabIndex = 33;
            this.gameField.TabStop = false;
            this.gameField.Visible = false;
            this.gameField.Enter += new System.EventHandler(this.gameField_Enter);
            // 
            // gameResultDialog
            // 
            this.gameResultDialog.Controls.Add(this.gameResultMessage);
            this.gameResultDialog.Location = new System.Drawing.Point(170, 130);
            this.gameResultDialog.Name = "gameResultDialog";
            this.gameResultDialog.Size = new System.Drawing.Size(450, 290);
            this.gameResultDialog.TabIndex = 34;
            this.gameResultDialog.TabStop = false;
            this.gameResultDialog.Text = "Game Result";
            this.gameResultDialog.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1468, 725);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.gameResultDialog);
            this.Controls.Add(this.gameField);
            this.Controls.Add(this.players);
            this.Controls.Add(this.pause);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menu.ResumeLayout(false);
            this.gameField.ResumeLayout(false);
            this.gameResultDialog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button startButton;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;
        private Button button13;
        private Button button14;
        private Button button15;
        private Button button16;
        private Button button17;
        private Button button18;
        private Button button19;
        private Button button20;
        private Button button21;
        private Button button22;
        private Button button23;
        private Button button24;
        private Button button25;
        private Button button26;
        private Button continueGame;
        private Button quitGame;
        private Button pause;
        private ListBox players;
        private Label gameResultMessage;
        private GroupBox menu;
        private GroupBox gameField;
        private GroupBox gameResultDialog;
    }
}