namespace Lr1
{
    partial class MainForm
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
            Info = new Label();
            UpdateStationBtn = new Button();
            AddNewStationBtn = new Button();
            ErrorButton = new Button();
            OpenEditFormButton = new Button();
            InfoPanel = new Panel();
            stationTypeComboBox = new ComboBox();
            label9 = new Label();
            panel1 = new Panel();
            textBox1 = new TextBox();
            label1 = new Label();
            InfoPanel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Info
            // 
            Info.Dock = DockStyle.Fill;
            Info.Font = new Font("Segoe UI", 10F);
            Info.Location = new Point(11, 13);
            Info.Name = "Info";
            Info.Size = new Size(412, 374);
            Info.TabIndex = 18;
            Info.Text = "Информация о станции";
            // 
            // UpdateStationBtn
            // 
            UpdateStationBtn.Font = new Font("Segoe UI", 12F);
            UpdateStationBtn.Location = new Point(482, 215);
            UpdateStationBtn.Margin = new Padding(3, 4, 3, 4);
            UpdateStationBtn.Name = "UpdateStationBtn";
            UpdateStationBtn.Size = new Size(229, 53);
            UpdateStationBtn.TabIndex = 19;
            UpdateStationBtn.Text = "Редактировать";
            UpdateStationBtn.UseVisualStyleBackColor = true;
            UpdateStationBtn.Click += UpdateStationBtn_Click;
            // 
            // AddNewStationBtn
            // 
            AddNewStationBtn.Font = new Font("Segoe UI", 12F);
            AddNewStationBtn.Location = new Point(482, 276);
            AddNewStationBtn.Margin = new Padding(3, 4, 3, 4);
            AddNewStationBtn.Name = "AddNewStationBtn";
            AddNewStationBtn.Size = new Size(229, 53);
            AddNewStationBtn.TabIndex = 21;
            AddNewStationBtn.Text = "Добавить новый";
            AddNewStationBtn.UseVisualStyleBackColor = true;
            AddNewStationBtn.Click += AddNewStationBtn_Click;
            // 
            // ErrorButton
            // 
            ErrorButton.Font = new Font("Segoe UI", 12F);
            ErrorButton.Location = new Point(482, 337);
            ErrorButton.Margin = new Padding(3, 4, 3, 4);
            ErrorButton.Name = "ErrorButton";
            ErrorButton.Size = new Size(229, 53);
            ErrorButton.TabIndex = 26;
            ErrorButton.Text = "Вызов ошибки";
            ErrorButton.UseVisualStyleBackColor = true;
            ErrorButton.Click += ErrorButton_Click;
            // 
            // OpenEditFormButton
            // 
            OpenEditFormButton.Font = new Font("Segoe UI", 12F);
            OpenEditFormButton.Location = new Point(482, 398);
            OpenEditFormButton.Margin = new Padding(3, 4, 3, 4);
            OpenEditFormButton.Name = "OpenEditFormButton";
            OpenEditFormButton.Size = new Size(229, 53);
            OpenEditFormButton.TabIndex = 27;
            OpenEditFormButton.Text = "Просмотр контейнера";
            OpenEditFormButton.UseVisualStyleBackColor = true;
            OpenEditFormButton.Click += OpenEditFormButton_Click;
            // 
            // InfoPanel
            // 
            InfoPanel.BackColor = SystemColors.ControlLight;
            InfoPanel.BorderStyle = BorderStyle.FixedSingle;
            InfoPanel.Controls.Add(Info);
            InfoPanel.Location = new Point(18, 52);
            InfoPanel.Margin = new Padding(3, 4, 3, 4);
            InfoPanel.Name = "InfoPanel";
            InfoPanel.Padding = new Padding(11, 13, 11, 13);
            InfoPanel.Size = new Size(436, 402);
            InfoPanel.TabIndex = 40;
            // 
            // stationTypeComboBox
            // 
            stationTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            stationTypeComboBox.Font = new Font("Segoe UI", 10F);
            stationTypeComboBox.FormattingEnabled = true;
            stationTypeComboBox.Location = new Point(195, 13);
            stationTypeComboBox.Margin = new Padding(3, 4, 3, 4);
            stationTypeComboBox.Name = "stationTypeComboBox";
            stationTypeComboBox.Size = new Size(285, 31);
            stationTypeComboBox.TabIndex = 29;
            stationTypeComboBox.SelectedIndexChanged += stationTypeComboBox_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label9.Location = new Point(18, 13);
            label9.Name = "label9";
            label9.Size = new Size(135, 28);
            label9.TabIndex = 28;
            label9.Text = "Тип станции";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Chocolate;
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(482, 63);
            panel1.Name = "panel1";
            panel1.Size = new Size(229, 117);
            panel1.TabIndex = 41;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(40, 61);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(154, 27);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 8);
            label1.Name = "label1";
            label1.Size = new Size(137, 40);
            label1.TabIndex = 0;
            label1.Text = "Первый параметр\r\nв 16-ричной СС";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(754, 488);
            Controls.Add(panel1);
            Controls.Add(InfoPanel);
            Controls.Add(stationTypeComboBox);
            Controls.Add(label9);
            Controls.Add(OpenEditFormButton);
            Controls.Add(ErrorButton);
            Controls.Add(AddNewStationBtn);
            Controls.Add(UpdateStationBtn);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Управление станциями";
            Load += MainForm_Load_1;
            InfoPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label Info;
        private Button UpdateStationBtn;
        private Button AddNewStationBtn;
        private Button ErrorButton;
        private Button OpenEditFormButton;
        private Panel InfoPanel;
        private ComboBox stationTypeComboBox;
        private Label label9;
        private Panel panel1;
        private TextBox textBox1;
        private Label label1;
    }
}