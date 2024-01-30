namespace Sudoku
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.newGameButton = new System.Windows.Forms.Button();
            this.checkButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.beginnerLevel = new System.Windows.Forms.RadioButton();
            this.IntermediateLevel = new System.Windows.Forms.RadioButton();
            this.AdvancedLevel = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.APLog = new System.Windows.Forms.RichTextBox();
            this.ServerText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UserText = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.DeathLinkCheckBox = new System.Windows.Forms.CheckBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.entryNormal = new System.Windows.Forms.RadioButton();
            this.entryCenter = new System.Windows.Forms.RadioButton();
            this.panelEntryMode = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(675, 675);
            this.panel1.TabIndex = 99;
            // 
            // newGameButton
            // 
            this.newGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.newGameButton.Location = new System.Drawing.Point(709, 178);
            this.newGameButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(199, 66);
            this.newGameButton.TabIndex = 7;
            this.newGameButton.Text = "New Game";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
            // 
            // checkButton
            // 
            this.checkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkButton.Location = new System.Drawing.Point(1015, 13);
            this.checkButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(199, 152);
            this.checkButton.TabIndex = 8;
            this.checkButton.Text = "Check Input";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clearButton.Location = new System.Drawing.Point(1015, 170);
            this.clearButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(199, 74);
            this.clearButton.TabIndex = 9;
            this.clearButton.Text = "Clear Input";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // beginnerLevel
            // 
            this.beginnerLevel.AutoSize = true;
            this.beginnerLevel.Checked = true;
            this.beginnerLevel.Location = new System.Drawing.Point(709, 66);
            this.beginnerLevel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.beginnerLevel.Name = "beginnerLevel";
            this.beginnerLevel.Size = new System.Drawing.Size(106, 29);
            this.beginnerLevel.TabIndex = 4;
            this.beginnerLevel.TabStop = true;
            this.beginnerLevel.Text = "Beginner";
            this.beginnerLevel.UseVisualStyleBackColor = true;
            // 
            // IntermediateLevel
            // 
            this.IntermediateLevel.AutoSize = true;
            this.IntermediateLevel.Location = new System.Drawing.Point(709, 103);
            this.IntermediateLevel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.IntermediateLevel.Name = "IntermediateLevel";
            this.IntermediateLevel.Size = new System.Drawing.Size(137, 29);
            this.IntermediateLevel.TabIndex = 5;
            this.IntermediateLevel.Text = "Intermediate";
            this.IntermediateLevel.UseVisualStyleBackColor = true;
            // 
            // AdvancedLevel
            // 
            this.AdvancedLevel.AutoSize = true;
            this.AdvancedLevel.Location = new System.Drawing.Point(709, 140);
            this.AdvancedLevel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AdvancedLevel.Name = "AdvancedLevel";
            this.AdvancedLevel.Size = new System.Drawing.Size(116, 29);
            this.AdvancedLevel.TabIndex = 6;
            this.AdvancedLevel.Text = "Advanced";
            this.AdvancedLevel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(709, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 36);
            this.label1.TabIndex = 4;
            this.label1.Text = "Level";
            // 
            // APLog
            // 
            this.APLog.AcceptsTab = true;
            this.APLog.BackColor = System.Drawing.SystemColors.ControlText;
            this.APLog.ForeColor = System.Drawing.Color.White;
            this.APLog.Location = new System.Drawing.Point(11, 695);
            this.APLog.Name = "APLog";
            this.APLog.ReadOnly = true;
            this.APLog.Size = new System.Drawing.Size(1203, 254);
            this.APLog.TabIndex = 5;
            this.APLog.Text = "";
            // 
            // ServerText
            // 
            this.ServerText.Location = new System.Drawing.Point(693, 584);
            this.ServerText.Name = "ServerText";
            this.ServerText.Size = new System.Drawing.Size(521, 31);
            this.ServerText.TabIndex = 2;
            this.ServerText.Text = "Archipelago.gg:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(693, 551);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 30);
            this.label2.TabIndex = 7;
            this.label2.Text = "Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(693, 417);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 30);
            this.label3.TabIndex = 9;
            this.label3.Text = "User";
            // 
            // UserText
            // 
            this.UserText.Location = new System.Drawing.Point(693, 450);
            this.UserText.Name = "UserText";
            this.UserText.Size = new System.Drawing.Size(521, 31);
            this.UserText.TabIndex = 1;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConnectButton.Location = new System.Drawing.Point(693, 622);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(199, 66);
            this.ConnectButton.TabIndex = 3;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // DeathLinkCheckBox
            // 
            this.DeathLinkCheckBox.AutoSize = true;
            this.DeathLinkCheckBox.Location = new System.Drawing.Point(1102, 659);
            this.DeathLinkCheckBox.Name = "DeathLinkCheckBox";
            this.DeathLinkCheckBox.Size = new System.Drawing.Size(112, 29);
            this.DeathLinkCheckBox.TabIndex = 100;
            this.DeathLinkCheckBox.Text = "Deathlink";
            this.DeathLinkCheckBox.UseVisualStyleBackColor = true;
            this.DeathLinkCheckBox.CheckedChanged += new System.EventHandler(this.DeathLinkCheckBox_CheckedChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPassword.Location = new System.Drawing.Point(693, 484);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(103, 30);
            this.lblPassword.TabIndex = 101;
            this.lblPassword.Text = "Password";
            // 
            // PasswordText
            // 
            this.PasswordText.Location = new System.Drawing.Point(693, 517);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.PasswordChar = '*';
            this.PasswordText.Size = new System.Drawing.Size(521, 31);
            this.PasswordText.TabIndex = 102;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(693, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 30);
            this.label4.TabIndex = 103;
            this.label4.Text = "Entry Mode";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(775, 252);
            this.label5.Name = "label3";
            this.label5.Size = new System.Drawing.Size(57, 30);
            this.label5.TabIndex = 104;
            this.label5.Text = "(Press Tab or Hold Shift to toggle)";
            //
            // panelEntryMode
            //
            this.panelEntryMode.Controls.Add(entryNormal);
            this.panelEntryMode.Controls.Add(entryCenter);
            this.panelEntryMode.Location = new System.Drawing.Point(709, 270);
            this.panelEntryMode.Size = new System.Drawing.Size(106, 78);
            // 
            // entryNormal
            // 
            this.entryNormal.AutoSize = true;
            this.entryNormal.Checked = true;
            this.entryNormal.Location = new System.Drawing.Point(0, 0);
            this.entryNormal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.entryNormal.Name = "entryNormal";
            this.entryNormal.Size = new System.Drawing.Size(106, 29);
            this.entryNormal.TabIndex = 105;
            this.entryNormal.TabStop = true;
            this.entryNormal.Text = "Normal";
            this.entryNormal.UseVisualStyleBackColor = true;
            this.entryNormal.CheckedChanged += SetEntryNormal;
            // 
            // entryCenter
            // 
            this.entryCenter.AutoSize = true;
            this.entryCenter.Location = new System.Drawing.Point(0, 20);
            this.entryCenter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.entryCenter.Name = "entryCenter";
            this.entryCenter.Size = new System.Drawing.Size(106, 29);
            this.entryCenter.TabIndex = 106;
            this.entryCenter.TabStop = true;
            this.entryCenter.Text = "Center";
            this.entryCenter.UseVisualStyleBackColor = true;
            this.entryCenter.CheckedChanged += SetEntryCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 957);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panelEntryMode);
            this.Controls.Add(this.PasswordText);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.DeathLinkCheckBox);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UserText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ServerText);
            this.Controls.Add(this.APLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AdvancedLevel);
            this.Controls.Add(this.IntermediateLevel);
            this.Controls.Add(this.beginnerLevel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.checkButton);
            this.Controls.Add(this.newGameButton);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Sudoku";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button newGameButton;
        private System.Windows.Forms.Button checkButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.RadioButton beginnerLevel;
        private System.Windows.Forms.RadioButton IntermediateLevel;
        private System.Windows.Forms.RadioButton AdvancedLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox APLog;
        private System.Windows.Forms.TextBox ServerText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox UserText;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.CheckBox DeathLinkCheckBox;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox PasswordText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton entryNormal;
        private System.Windows.Forms.RadioButton entryCenter;
        private System.Windows.Forms.Panel panelEntryMode;
    }
}

