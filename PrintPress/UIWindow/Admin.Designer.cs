namespace PrintPress.UI
{
    partial class Admin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            classifiedPage = new TabPage();
            sendClassifiedSqlButton = new Button();
            clasSqlStringText = new TextBox();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            commercialData = new TabPage();
            sendSqlButton = new Button();
            sqlStringText = new TextBox();
            CreateCoTableButton = new Button();
            commercialDataGrid = new DataGridView();
            adminTabControl = new TabControl();
            EmployeeTab = new TabPage();
            splitContainer1 = new SplitContainer();
            addEmployeeButton = new Button();
            label13 = new Label();
            label10 = new Label();
            clearanceText = new TextBox();
            jobDescText = new TextBox();
            label11 = new Label();
            postcodeText = new TextBox();
            Postcode = new Label();
            countryText = new TextBox();
            label9 = new Label();
            countyText = new TextBox();
            label5 = new Label();
            cityText = new TextBox();
            label6 = new Label();
            phoneText = new TextBox();
            label7 = new Label();
            emailText = new TextBox();
            label8 = new Label();
            roadNameText = new TextBox();
            label4 = new Label();
            houseNameNumText = new TextBox();
            label3 = new Label();
            lastNameText = new TextBox();
            label2 = new Label();
            firstNamesText = new TextBox();
            label1 = new Label();
            passwordText = new TextBox();
            label14 = new Label();
            classifiedPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            commercialData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)commercialDataGrid).BeginInit();
            adminTabControl.SuspendLayout();
            EmployeeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // classifiedPage
            // 
            classifiedPage.Controls.Add(sendClassifiedSqlButton);
            classifiedPage.Controls.Add(clasSqlStringText);
            classifiedPage.Controls.Add(button2);
            classifiedPage.Controls.Add(dataGridView1);
            classifiedPage.Location = new Point(4, 29);
            classifiedPage.Name = "classifiedPage";
            classifiedPage.Padding = new Padding(3);
            classifiedPage.Size = new Size(788, 436);
            classifiedPage.TabIndex = 2;
            classifiedPage.Text = "ClassifiedData";
            classifiedPage.UseVisualStyleBackColor = true;
            // 
            // sendClassifiedSqlButton
            // 
            sendClassifiedSqlButton.Location = new Point(661, 41);
            sendClassifiedSqlButton.Name = "sendClassifiedSqlButton";
            sendClassifiedSqlButton.Size = new Size(115, 29);
            sendClassifiedSqlButton.TabIndex = 11;
            sendClassifiedSqlButton.Text = "Send SQL";
            sendClassifiedSqlButton.UseVisualStyleBackColor = true;
            sendClassifiedSqlButton.Click += sendClassifiedSqlButton_Click;
            // 
            // clasSqlStringText
            // 
            clasSqlStringText.Location = new Point(6, 42);
            clasSqlStringText.Name = "clasSqlStringText";
            clasSqlStringText.Size = new Size(649, 27);
            clasSqlStringText.TabIndex = 10;
            // 
            // button2
            // 
            button2.Location = new Point(6, 7);
            button2.Name = "button2";
            button2.Size = new Size(127, 29);
            button2.TabIndex = 9;
            button2.Text = "Create Table";
            button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 78);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 323);
            dataGridView1.TabIndex = 8;
            // 
            // commercialData
            // 
            commercialData.Controls.Add(sendSqlButton);
            commercialData.Controls.Add(sqlStringText);
            commercialData.Controls.Add(CreateCoTableButton);
            commercialData.Controls.Add(commercialDataGrid);
            commercialData.Location = new Point(4, 29);
            commercialData.Name = "commercialData";
            commercialData.Padding = new Padding(3);
            commercialData.Size = new Size(788, 436);
            commercialData.TabIndex = 1;
            commercialData.Text = "CommercialData";
            commercialData.UseVisualStyleBackColor = true;
            // 
            // sendSqlButton
            // 
            sendSqlButton.Location = new Point(661, 43);
            sendSqlButton.Name = "sendSqlButton";
            sendSqlButton.Size = new Size(115, 29);
            sendSqlButton.TabIndex = 7;
            sendSqlButton.Text = "Send SQL";
            sendSqlButton.UseVisualStyleBackColor = true;
            sendSqlButton.Click += sendSqlButton_Click;
            // 
            // sqlStringText
            // 
            sqlStringText.Location = new Point(6, 44);
            sqlStringText.Name = "sqlStringText";
            sqlStringText.Size = new Size(649, 27);
            sqlStringText.TabIndex = 6;
            // 
            // CreateCoTableButton
            // 
            CreateCoTableButton.Location = new Point(6, 9);
            CreateCoTableButton.Name = "CreateCoTableButton";
            CreateCoTableButton.Size = new Size(127, 29);
            CreateCoTableButton.TabIndex = 1;
            CreateCoTableButton.Text = "Create Table";
            CreateCoTableButton.UseVisualStyleBackColor = true;
            CreateCoTableButton.Click += CreateCoTableButton_Click;
            // 
            // commercialDataGrid
            // 
            commercialDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            commercialDataGrid.Location = new Point(6, 80);
            commercialDataGrid.Name = "commercialDataGrid";
            commercialDataGrid.RowHeadersWidth = 51;
            commercialDataGrid.Size = new Size(776, 323);
            commercialDataGrid.TabIndex = 0;
            // 
            // adminTabControl
            // 
            adminTabControl.Controls.Add(commercialData);
            adminTabControl.Controls.Add(classifiedPage);
            adminTabControl.Controls.Add(EmployeeTab);
            adminTabControl.Dock = DockStyle.Fill;
            adminTabControl.Location = new Point(0, 0);
            adminTabControl.Name = "adminTabControl";
            adminTabControl.SelectedIndex = 0;
            adminTabControl.Size = new Size(800, 518);
            adminTabControl.TabIndex = 0;
            // 
            // EmployeeTab
            // 
            EmployeeTab.Controls.Add(splitContainer1);
            EmployeeTab.Location = new Point(4, 29);
            EmployeeTab.Name = "EmployeeTab";
            EmployeeTab.Padding = new Padding(3);
            EmployeeTab.Size = new Size(792, 485);
            EmployeeTab.TabIndex = 3;
            EmployeeTab.Text = "Employee";
            EmployeeTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(label14);
            splitContainer1.Panel1.Controls.Add(passwordText);
            splitContainer1.Panel1.Controls.Add(addEmployeeButton);
            splitContainer1.Panel1.Controls.Add(label13);
            splitContainer1.Panel1.Controls.Add(label10);
            splitContainer1.Panel1.Controls.Add(clearanceText);
            splitContainer1.Panel1.Controls.Add(jobDescText);
            splitContainer1.Panel1.Controls.Add(label11);
            splitContainer1.Panel1.Controls.Add(postcodeText);
            splitContainer1.Panel1.Controls.Add(Postcode);
            splitContainer1.Panel1.Controls.Add(countryText);
            splitContainer1.Panel1.Controls.Add(label9);
            splitContainer1.Panel1.Controls.Add(countyText);
            splitContainer1.Panel1.Controls.Add(label5);
            splitContainer1.Panel1.Controls.Add(cityText);
            splitContainer1.Panel1.Controls.Add(label6);
            splitContainer1.Panel1.Controls.Add(phoneText);
            splitContainer1.Panel1.Controls.Add(label7);
            splitContainer1.Panel1.Controls.Add(emailText);
            splitContainer1.Panel1.Controls.Add(label8);
            splitContainer1.Panel1.Controls.Add(roadNameText);
            splitContainer1.Panel1.Controls.Add(label4);
            splitContainer1.Panel1.Controls.Add(houseNameNumText);
            splitContainer1.Panel1.Controls.Add(label3);
            splitContainer1.Panel1.Controls.Add(lastNameText);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(firstNamesText);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Size = new Size(786, 479);
            splitContainer1.SplitterDistance = 385;
            splitContainer1.TabIndex = 2;
            // 
            // addEmployeeButton
            // 
            addEmployeeButton.Location = new Point(253, 439);
            addEmployeeButton.Name = "addEmployeeButton";
            addEmployeeButton.Size = new Size(125, 29);
            addEmployeeButton.TabIndex = 27;
            addEmployeeButton.Text = "Add employee";
            addEmployeeButton.UseVisualStyleBackColor = true;
            addEmployeeButton.Click += addEmployeeButton_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(3, 310);
            label13.Name = "label13";
            label13.Size = new Size(167, 20);
            label13.TabIndex = 24;
            label13.Text = "Clearance (',' separated)";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(3, 330);
            label10.Name = "label10";
            label10.Size = new Size(346, 17);
            label10.TabIndex = 26;
            label10.Text = "Admin/Accounts/Editing/Journalism/Marketing/Processing";
            // 
            // clearanceText
            // 
            clearanceText.Location = new Point(3, 353);
            clearanceText.Name = "clearanceText";
            clearanceText.Size = new Size(375, 27);
            clearanceText.TabIndex = 25;
            // 
            // jobDescText
            // 
            jobDescText.Location = new Point(3, 247);
            jobDescText.Name = "jobDescText";
            jobDescText.Size = new Size(177, 27);
            jobDescText.TabIndex = 21;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(3, 224);
            label11.Name = "label11";
            label11.Size = new Size(110, 20);
            label11.TabIndex = 20;
            label11.Text = "Job description";
            // 
            // postcodeText
            // 
            postcodeText.Location = new Point(201, 300);
            postcodeText.Name = "postcodeText";
            postcodeText.Size = new Size(177, 27);
            postcodeText.TabIndex = 19;
            // 
            // Postcode
            // 
            Postcode.AutoSize = true;
            Postcode.Location = new Point(201, 277);
            Postcode.Name = "Postcode";
            Postcode.Size = new Size(69, 20);
            Postcode.TabIndex = 18;
            Postcode.Text = "Postcode";
            // 
            // countryText
            // 
            countryText.Location = new Point(201, 247);
            countryText.Name = "countryText";
            countryText.Size = new Size(177, 27);
            countryText.TabIndex = 17;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(201, 224);
            label9.Name = "label9";
            label9.Size = new Size(60, 20);
            label9.TabIndex = 16;
            label9.Text = "Country";
            // 
            // countyText
            // 
            countyText.Location = new Point(201, 194);
            countyText.Name = "countyText";
            countyText.Size = new Size(177, 27);
            countyText.TabIndex = 15;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(201, 171);
            label5.Name = "label5";
            label5.Size = new Size(55, 20);
            label5.TabIndex = 14;
            label5.Text = "County";
            // 
            // cityText
            // 
            cityText.Location = new Point(201, 141);
            cityText.Name = "cityText";
            cityText.Size = new Size(177, 27);
            cityText.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(201, 118);
            label6.Name = "label6";
            label6.Size = new Size(34, 20);
            label6.TabIndex = 12;
            label6.Text = "City";
            // 
            // phoneText
            // 
            phoneText.Location = new Point(3, 194);
            phoneText.Name = "phoneText";
            phoneText.Size = new Size(177, 27);
            phoneText.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 171);
            label7.Name = "label7";
            label7.Size = new Size(105, 20);
            label7.TabIndex = 10;
            label7.Text = "Phone number";
            // 
            // emailText
            // 
            emailText.Location = new Point(3, 141);
            emailText.Name = "emailText";
            emailText.Size = new Size(177, 27);
            emailText.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 118);
            label8.Name = "label8";
            label8.Size = new Size(46, 20);
            label8.TabIndex = 8;
            label8.Text = "Email";
            // 
            // roadNameText
            // 
            roadNameText.Location = new Point(201, 88);
            roadNameText.Name = "roadNameText";
            roadNameText.Size = new Size(177, 27);
            roadNameText.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(201, 65);
            label4.Name = "label4";
            label4.Size = new Size(85, 20);
            label4.TabIndex = 6;
            label4.Text = "Road name";
            // 
            // houseNameNumText
            // 
            houseNameNumText.Location = new Point(201, 35);
            houseNameNumText.Name = "houseNameNumText";
            houseNameNumText.Size = new Size(177, 27);
            houseNameNumText.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(201, 12);
            label3.Name = "label3";
            label3.Size = new Size(149, 20);
            label3.TabIndex = 4;
            label3.Text = "House name/number";
            // 
            // lastNameText
            // 
            lastNameText.Location = new Point(3, 88);
            lastNameText.Name = "lastNameText";
            lastNameText.Size = new Size(177, 27);
            lastNameText.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 65);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 2;
            label2.Text = "Last name";
            // 
            // firstNamesText
            // 
            firstNamesText.Location = new Point(3, 35);
            firstNamesText.Name = "firstNamesText";
            firstNamesText.Size = new Size(177, 27);
            firstNamesText.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 12);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 0;
            label1.Text = "First names";
            // 
            // passwordText
            // 
            passwordText.Location = new Point(5, 406);
            passwordText.Name = "passwordText";
            passwordText.Size = new Size(375, 27);
            passwordText.TabIndex = 28;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(5, 383);
            label14.Name = "label14";
            label14.Size = new Size(70, 20);
            label14.TabIndex = 29;
            label14.Text = "Password";
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 518);
            Controls.Add(adminTabControl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Admin";
            Text = "Admin - PrintPress";
            classifiedPage.ResumeLayout(false);
            classifiedPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            commercialData.ResumeLayout(false);
            commercialData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)commercialDataGrid).EndInit();
            adminTabControl.ResumeLayout(false);
            EmployeeTab.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage classifiedPage;
        private TabPage commercialData;
        private DataGridView commercialDataGrid;
        private TabControl adminTabControl;
        private Button CreateCoTableButton;
        private Button sendSqlButton;
        private TextBox sqlStringText;
        private Button sendClassifiedSqlButton;
        private TextBox clasSqlStringText;
        private Button button2;
        private DataGridView dataGridView1;
        private TabPage EmployeeTab;
        private Label label1;
        private TextBox firstNamesText;
        private SplitContainer splitContainer1;
        private TextBox lastNameText;
        private Label label2;
        private TextBox houseNameNumText;
        private Label label3;
        private TextBox postcodeText;
        private TextBox jobDescText;
        private Label label11;
        private Label Postcode;
        private TextBox countryText;
        private Label label9;
        private TextBox countyText;
        private Label label5;
        private TextBox cityText;
        private Label label6;
        private TextBox phoneText;
        private Label label7;
        private TextBox emailText;
        private Label label8;
        private TextBox roadNameText;
        private Label label4;
        private TextBox clearanceText;
        private Label label13;
        private Label label10;
        private Button addEmployeeButton;
        private Label label14;
        private TextBox passwordText;
    }
}