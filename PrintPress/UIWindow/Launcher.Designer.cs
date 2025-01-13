namespace PrintPress
{
    partial class Launcher
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
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            clientComboBox = new ComboBox();
            label1 = new Label();
            launchButton = new Button();
            clientStatusLabel = new Label();
            pictureBox1 = new PictureBox();
            emailText = new TextBox();
            passwordText = new TextBox();
            label2 = new Label();
            label3 = new Label();
            logInButton = new Button();
            loginStatusLabel = new Label();
            adminLabel = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // clientComboBox
            // 
            clientComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            clientComboBox.FormattingEnabled = true;
            clientComboBox.Location = new Point(12, 225);
            clientComboBox.Name = "clientComboBox";
            clientComboBox.Size = new Size(170, 28);
            clientComboBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 202);
            label1.Name = "label1";
            label1.Size = new Size(47, 20);
            label1.TabIndex = 1;
            label1.Text = "Client";
            // 
            // launchButton
            // 
            launchButton.Enabled = false;
            launchButton.Location = new Point(199, 224);
            launchButton.Name = "launchButton";
            launchButton.Size = new Size(73, 28);
            launchButton.TabIndex = 2;
            launchButton.Text = "Launch";
            launchButton.UseVisualStyleBackColor = true;
            launchButton.Click += LaunchButton_Click;
            // 
            // clientStatusLabel
            // 
            clientStatusLabel.AutoSize = true;
            clientStatusLabel.ForeColor = SystemColors.ControlDarkDark;
            clientStatusLabel.Location = new Point(12, 256);
            clientStatusLabel.Name = "clientStatusLabel";
            clientStatusLabel.Size = new Size(152, 20);
            clientStatusLabel.TabIndex = 3;
            clientStatusLabel.Text = "log in to launch client";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(287, 67);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(216, 201);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // emailText
            // 
            emailText.Location = new Point(12, 32);
            emailText.Name = "emailText";
            emailText.Size = new Size(331, 27);
            emailText.TabIndex = 5;
            // 
            // passwordText
            // 
            passwordText.Location = new Point(12, 85);
            passwordText.Name = "passwordText";
            passwordText.PasswordChar = '*';
            passwordText.Size = new Size(331, 27);
            passwordText.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 62);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 7;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(46, 20);
            label3.TabIndex = 8;
            label3.Text = "Email";
            // 
            // logInButton
            // 
            logInButton.Location = new Point(12, 148);
            logInButton.Name = "logInButton";
            logInButton.Size = new Size(73, 28);
            logInButton.TabIndex = 9;
            logInButton.Text = "Log In";
            logInButton.UseVisualStyleBackColor = true;
            logInButton.Click += LogInButton_Click;
            // 
            // loginStatusLabel
            // 
            loginStatusLabel.AutoSize = true;
            loginStatusLabel.ForeColor = SystemColors.ControlDarkDark;
            loginStatusLabel.Location = new Point(12, 115);
            loginStatusLabel.Name = "loginStatusLabel";
            loginStatusLabel.Size = new Size(119, 20);
            loginStatusLabel.TabIndex = 10;
            loginStatusLabel.Text = "enter credentials";
            // 
            // adminLabel
            // 
            adminLabel.AutoSize = true;
            adminLabel.Location = new Point(450, 35);
            adminLabel.Name = "adminLabel";
            adminLabel.Size = new Size(51, 20);
            adminLabel.TabIndex = 11;
            adminLabel.TabStop = true;
            adminLabel.Text = "admin";
            adminLabel.LinkClicked += adminLabel_LinkClicked;
            // 
            // Launcher
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(513, 285);
            Controls.Add(adminLabel);
            Controls.Add(loginStatusLabel);
            Controls.Add(logInButton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(passwordText);
            Controls.Add(emailText);
            Controls.Add(pictureBox1);
            Controls.Add(clientStatusLabel);
            Controls.Add(launchButton);
            Controls.Add(label1);
            Controls.Add(clientComboBox);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Launcher";
            Text = "Launcher - PrintPress";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox clientComboBox;
        private Label label1;
        private Button launchButton;
        private Label clientStatusLabel;
        private PictureBox pictureBox1;
        private TextBox emailText;
        private TextBox passwordText;
        private Label label2;
        private Label label3;
        private Button logInButton;
        private Label loginStatusLabel;
        private LinkLabel adminLabel;
    }
}
