namespace PrintPress.UI
{
    partial class Journalism
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
            sourcesText = new TextBox();
            label9 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // createButton
            // 
            createButton.Click += createButton_Click;
            // 
            // saveButton
            // 
            saveButton.Click += saveButton_Click;
            // 
            // sourcesText
            // 
            sourcesText.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            sourcesText.Location = new Point(983, 605);
            sourcesText.Multiline = true;
            sourcesText.Name = "sourcesText";
            sourcesText.ScrollBars = ScrollBars.Vertical;
            sourcesText.Size = new Size(422, 227);
            sourcesText.TabIndex = 26;
            sourcesText.TextChanged += sourcesText_TextChanged;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(980, 582);
            label9.Name = "label9";
            label9.Size = new Size(63, 20);
            label9.TabIndex = 29;
            label9.Text = "Sources";
            // 
            // Journalism
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1418, 879);
            Controls.Add(label9);
            Controls.Add(sourcesText);
            Name = "Journalism";
            Text = "Journalism - PrintPress";
            Controls.SetChildIndex(contentStatusComboBox, 0);
            Controls.SetChildIndex(label4, 0);
            Controls.SetChildIndex(contentText, 0);
            Controls.SetChildIndex(contentListView, 0);
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(titleText, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(submitButton, 0);
            Controls.SetChildIndex(createButton, 0);
            Controls.SetChildIndex(deleteButton, 0);
            Controls.SetChildIndex(pictureBox1, 0);
            Controls.SetChildIndex(saveButton, 0);
            Controls.SetChildIndex(notesText, 0);
            Controls.SetChildIndex(label6, 0);
            Controls.SetChildIndex(label8, 0);
            Controls.SetChildIndex(label7, 0);
            Controls.SetChildIndex(uploadImageButton, 0);
            Controls.SetChildIndex(removeImageButton, 0);
            Controls.SetChildIndex(saveStateTitleLabel, 0);
            Controls.SetChildIndex(saveStateLabel, 0);
            Controls.SetChildIndex(commentsText, 0);
            Controls.SetChildIndex(label5, 0);
            Controls.SetChildIndex(sourcesText, 0);
            Controls.SetChildIndex(label9, 0);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox sourcesText;
        private Label label9;
    }
}