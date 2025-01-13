namespace PrintPress.UI
{
    partial class Marketing
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
            label4 = new Label();
            textBox2 = new TextBox();
            label5 = new Label();
            label9 = new Label();
            textBox3 = new TextBox();
            label10 = new Label();
            textBox4 = new TextBox();
            label11 = new Label();
            textBox5 = new TextBox();
            SuspendLayout();
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(983, 582);
            label4.Name = "label4";
            label4.Size = new Size(113, 20);
            label4.TabIndex = 24;
            label4.Text = "Contact details";
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            textBox2.Location = new Point(1013, 631);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(328, 27);
            textBox2.TabIndex = 25;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(1013, 608);
            label5.Name = "label5";
            label5.Size = new Size(83, 20);
            label5.TabIndex = 26;
            label5.Text = "First names";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(1013, 661);
            label9.Name = "label9";
            label9.Size = new Size(76, 20);
            label9.TabIndex = 28;
            label9.Text = "Last name";
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            textBox3.Location = new Point(1013, 684);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(226, 27);
            textBox3.TabIndex = 27;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Location = new Point(1013, 714);
            label10.Name = "label10";
            label10.Size = new Size(46, 20);
            label10.TabIndex = 30;
            label10.Text = "Email";
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            textBox4.Location = new Point(1013, 737);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(328, 27);
            textBox4.TabIndex = 29;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Location = new Point(1013, 767);
            label11.Name = "label11";
            label11.Size = new Size(50, 20);
            label11.TabIndex = 32;
            label11.Text = "Phone";
            // 
            // textBox5
            // 
            textBox5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            textBox5.Location = new Point(1013, 790);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(226, 27);
            textBox5.TabIndex = 31;
            // 
            // Marketing
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1418, 879);
            Controls.Add(label11);
            Controls.Add(textBox5);
            Controls.Add(label10);
            Controls.Add(textBox4);
            Controls.Add(label9);
            Controls.Add(textBox3);
            Controls.Add(label5);
            Controls.Add(textBox2);
            Controls.Add(label4);
            Name = "Marketing";
            Text = "Marketing - PrintPress";
            Controls.SetChildIndex(label4, 0);
            Controls.SetChildIndex(textBox2, 0);
            Controls.SetChildIndex(label5, 0);
            Controls.SetChildIndex(textBox3, 0);
            Controls.SetChildIndex(label9, 0);
            Controls.SetChildIndex(textBox4, 0);
            Controls.SetChildIndex(label10, 0);
            Controls.SetChildIndex(textBox5, 0);
            Controls.SetChildIndex(label11, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label4;
        private TextBox textBox2;
        private Label label5;
        private Label label9;
        private TextBox textBox3;
        private Label label10;
        private TextBox textBox4;
        private Label label11;
        private TextBox textBox5;
    }
}