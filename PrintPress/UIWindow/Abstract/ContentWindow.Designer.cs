
namespace PrintPress.UI
{
    #if DEBUG
    internal partial class ContentWindow : PrintPress.UI.Tools.DummyClient
    #else
    internal partial class ContentWindow : PrintPress.UI.ClientWindow
    #endif
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
            ListViewGroup listViewGroup1 = new ListViewGroup("In Progress", HorizontalAlignment.Left);
            ListViewGroup listViewGroup2 = new ListViewGroup("Returned", HorizontalAlignment.Left);
            ListViewGroup listViewGroup3 = new ListViewGroup("Awaiting review", HorizontalAlignment.Left);
            ListViewGroup listViewGroup4 = new ListViewGroup("In use", HorizontalAlignment.Left);
            ListViewGroup listViewGroup5 = new ListViewGroup("Retired", HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentWindow));
            contentText = new TextBox();
            contentListView = new ListView();
            label1 = new Label();
            label2 = new Label();
            titleText = new TextBox();
            label3 = new Label();
            submitButton = new Button();
            createButton = new Button();
            deleteButton = new Button();
            pictureBox1 = new PictureBox();
            saveStateTitleLabel = new Label();
            saveStateLabel = new Label();
            saveButton = new Button();
            label8 = new Label();
            label7 = new Label();
            uploadImageButton = new Button();
            removeImageButton = new Button();
            label6 = new Label();
            notesText = new TextBox();
            label4 = new Label();
            commentsText = new TextBox();
            label5 = new Label();
            contentStatusComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // contentText
            // 
            contentText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            contentText.Location = new Point(283, 103);
            contentText.Multiline = true;
            contentText.Name = "contentText";
            contentText.ScrollBars = ScrollBars.Vertical;
            contentText.Size = new Size(694, 417);
            contentText.TabIndex = 1;
            contentText.TextChanged += contentText_TextChanged;
            // 
            // contentListView
            // 
            contentListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listViewGroup1.CollapsedState = ListViewGroupCollapsedState.Expanded;
            listViewGroup1.Header = "In Progress";
            listViewGroup1.Name = "inProgressGroup";
            listViewGroup2.Header = "Returned";
            listViewGroup2.Name = "returnedGroup";
            listViewGroup3.Header = "Awaiting review";
            listViewGroup3.Name = "awaitingReviewGroup";
            listViewGroup4.Header = "In use";
            listViewGroup4.Name = "inUseGroup";
            listViewGroup5.Header = "Retired";
            listViewGroup5.Name = "retiredGroup";
            contentListView.Groups.AddRange(new ListViewGroup[] { listViewGroup1, listViewGroup2, listViewGroup3, listViewGroup4, listViewGroup5 });
            contentListView.HeaderStyle = ColumnHeaderStyle.None;
            contentListView.ImeMode = ImeMode.Off;
            contentListView.Location = new Point(12, 50);
            contentListView.Name = "contentListView";
            contentListView.Size = new Size(265, 782);
            contentListView.TabIndex = 2;
            contentListView.UseCompatibleStateImageBehavior = false;
            contentListView.View = View.SmallIcon;
            contentListView.SelectedIndexChanged += contentListView_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(12, 27);
            label1.Name = "label1";
            label1.Size = new Size(133, 20);
            label1.TabIndex = 5;
            label1.Text = "Assigned Content";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(283, 27);
            label2.Name = "label2";
            label2.Size = new Size(40, 20);
            label2.TabIndex = 6;
            label2.Text = "Title";
            // 
            // titleText
            // 
            titleText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titleText.Location = new Point(283, 50);
            titleText.Name = "titleText";
            titleText.Size = new Size(694, 27);
            titleText.TabIndex = 7;
            titleText.TextChanged += titleText_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(283, 80);
            label3.Name = "label3";
            label3.Size = new Size(97, 20);
            label3.TabIndex = 8;
            label3.Text = "Content text";
            // 
            // submitButton
            // 
            submitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            submitButton.Location = new Point(1254, 838);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(151, 29);
            submitButton.TabIndex = 9;
            submitButton.Text = "Submit for review";
            submitButton.UseVisualStyleBackColor = true;
            // 
            // createButton
            // 
            createButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            createButton.Location = new Point(25, 838);
            createButton.Name = "createButton";
            createButton.Size = new Size(123, 29);
            createButton.TabIndex = 11;
            createButton.Text = "Create";
            createButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            deleteButton.Location = new Point(154, 838);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(123, 29);
            deleteButton.TabIndex = 12;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBox1.Location = new Point(983, 103);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(423, 417);
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // saveStateTitleLabel
            // 
            saveStateTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            saveStateTitleLabel.AutoSize = true;
            saveStateTitleLabel.BackColor = SystemColors.ControlLight;
            saveStateTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            saveStateTitleLabel.Location = new Point(1266, 27);
            saveStateTitleLabel.Name = "saveStateTitleLabel";
            saveStateTitleLabel.Size = new Size(85, 20);
            saveStateTitleLabel.TabIndex = 14;
            saveStateTitleLabel.Text = "Save State:";
            // 
            // saveStateLabel
            // 
            saveStateLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            saveStateLabel.AutoSize = true;
            saveStateLabel.BackColor = SystemColors.ControlLight;
            saveStateLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            saveStateLabel.ForeColor = Color.Green;
            saveStateLabel.Location = new Point(1358, 27);
            saveStateLabel.Name = "saveStateLabel";
            saveStateLabel.RightToLeft = RightToLeft.Yes;
            saveStateLabel.Size = new Size(49, 20);
            saveStateLabel.TabIndex = 15;
            saveStateLabel.Text = "saved";
            saveStateLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.Location = new Point(1148, 838);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(100, 29);
            saveButton.TabIndex = 16;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(783, 399);
            label8.Name = "label8";
            label8.Size = new Size(0, 20);
            label8.TabIndex = 20;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label7.Location = new Point(983, 80);
            label7.Name = "label7";
            label7.Size = new Size(53, 20);
            label7.TabIndex = 21;
            label7.Text = "Image";
            // 
            // uploadImageButton
            // 
            uploadImageButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            uploadImageButton.Location = new Point(1144, 526);
            uploadImageButton.Name = "uploadImageButton";
            uploadImageButton.Size = new Size(128, 29);
            uploadImageButton.TabIndex = 22;
            uploadImageButton.Text = "Upload image";
            uploadImageButton.UseVisualStyleBackColor = true;
            // 
            // removeImageButton
            // 
            removeImageButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            removeImageButton.Location = new Point(1278, 526);
            removeImageButton.Name = "removeImageButton";
            removeImageButton.Size = new Size(128, 29);
            removeImageButton.TabIndex = 23;
            removeImageButton.Text = "Remove image";
            removeImageButton.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.Location = new Point(283, 582);
            label6.Name = "label6";
            label6.Size = new Size(122, 20);
            label6.TabIndex = 18;
            label6.Text = "Notes for Editor";
            // 
            // notesText
            // 
            notesText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            notesText.Location = new Point(283, 605);
            notesText.Multiline = true;
            notesText.Name = "notesText";
            notesText.ScrollBars = ScrollBars.Vertical;
            notesText.Size = new Size(694, 118);
            notesText.TabIndex = 17;
            notesText.TextChanged += notesText_TextChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(283, 726);
            label4.Name = "label4";
            label4.Size = new Size(129, 20);
            label4.TabIndex = 25;
            label4.Text = "Editor comments";
            // 
            // commentsText
            // 
            commentsText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            commentsText.Location = new Point(283, 749);
            commentsText.Multiline = true;
            commentsText.Name = "commentsText";
            commentsText.ReadOnly = true;
            commentsText.ScrollBars = ScrollBars.Vertical;
            commentsText.Size = new Size(694, 118);
            commentsText.TabIndex = 24;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(283, 523);
            label5.Name = "label5";
            label5.Size = new Size(53, 20);
            label5.TabIndex = 26;
            label5.Text = "Status";
            // 
            // contentStatusComboBox
            // 
            contentStatusComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            contentStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            contentStatusComboBox.FormattingEnabled = true;
            contentStatusComboBox.Items.AddRange(new object[] { "In Progress", "Awaiting Review", "Returned", "In Use", "Retired" });
            contentStatusComboBox.Location = new Point(283, 546);
            contentStatusComboBox.Name = "contentStatusComboBox";
            contentStatusComboBox.Size = new Size(255, 28);
            contentStatusComboBox.TabIndex = 28;
            contentStatusComboBox.SelectedIndexChanged += contentStatusComboBox_SelectedIndexChanged;
            // 
            // ContentWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(1418, 879);
            Controls.Add(contentStatusComboBox);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(commentsText);
            Controls.Add(saveStateLabel);
            Controls.Add(saveStateTitleLabel);
            Controls.Add(removeImageButton);
            Controls.Add(uploadImageButton);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(notesText);
            Controls.Add(saveButton);
            Controls.Add(pictureBox1);
            Controls.Add(deleteButton);
            Controls.Add(createButton);
            Controls.Add(submitButton);
            Controls.Add(label3);
            Controls.Add(titleText);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(contentListView);
            Controls.Add(contentText);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ContentWindow";
            Text = " ";
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
            Controls.SetChildIndex(label4, 0);
            Controls.SetChildIndex(label5, 0);
            Controls.SetChildIndex(contentStatusComboBox, 0);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        protected TextBox contentText;
        protected ListView contentListView;
        protected Label label1;
        protected Label label2;
        protected TextBox titleText;
        protected Label label3;
        protected Button submitButton;
        protected Button createButton;
        protected Button deleteButton;
        protected PictureBox pictureBox1;
        protected Label saveStateTitleLabel;
        protected Label saveStateLabel;
        protected Button saveButton;
        protected Label label8;
        protected Label label7;
        protected Button uploadImageButton;
        protected Button removeImageButton;
        protected Label label6;
        protected TextBox notesText;
        protected Label label4;
        protected TextBox commentsText;
        protected Label label5;
        protected ComboBox contentStatusComboBox;
    }
}