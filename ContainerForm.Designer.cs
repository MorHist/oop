namespace Lr1
{
    partial class ContainerForm
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
            label1 = new Label();
            FindParamsComboBox = new ComboBox();
            ParamTextBox = new TextBox();
            FindButton = new Button();
            dataGridView1 = new DataGridView();
            NameOfStationColumn = new DataGridViewTextBoxColumn();
            NumberOfSeatsColumn = new DataGridViewTextBoxColumn();
            SoldTicketsColumn = new DataGridViewTextBoxColumn();
            NumberColumn = new DataGridViewTextBoxColumn();
            AverageAttendance = new DataGridViewTextBoxColumn();
            DateOfOpenColumn = new DataGridViewTextBoxColumn();
            AdressColumn = new DataGridViewTextBoxColumn();
            dataGridView2 = new DataGridView();
            ContainerColumn = new DataGridViewTextBoxColumn();
            InsertTimeColumn = new DataGridViewTextBoxColumn();
            SequentialReadTimeColumn = new DataGridViewTextBoxColumn();
            RandomReadTimeColumn = new DataGridViewTextBoxColumn();
            CompareButton = new Button();
            label2 = new Label();
            InfoLabel = new Label();
            groupBox1 = new GroupBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            btnInterpreterSearch = new Button();
            txtInterpreterQuery = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 28);
            label1.Name = "label1";
            label1.Size = new Size(107, 20);
            label1.TabIndex = 0;
            label1.Text = "Искать вокзал";
            // 
            // FindParamsComboBox
            // 
            FindParamsComboBox.FormattingEnabled = true;
            FindParamsComboBox.Location = new Point(19, 52);
            FindParamsComboBox.Margin = new Padding(3, 4, 3, 4);
            FindParamsComboBox.Name = "FindParamsComboBox";
            FindParamsComboBox.Size = new Size(215, 28);
            FindParamsComboBox.TabIndex = 1;
            FindParamsComboBox.SelectedIndexChanged += FindParamsComboBox_SelectedIndexChanged;
            // 
            // ParamTextBox
            // 
            ParamTextBox.Location = new Point(19, 91);
            ParamTextBox.Margin = new Padding(3, 4, 3, 4);
            ParamTextBox.Name = "ParamTextBox";
            ParamTextBox.Size = new Size(215, 27);
            ParamTextBox.TabIndex = 2;
            // 
            // FindButton
            // 
            FindButton.Location = new Point(256, 59);
            FindButton.Margin = new Padding(3, 4, 3, 4);
            FindButton.Name = "FindButton";
            FindButton.Size = new Size(55, 60);
            FindButton.TabIndex = 3;
            FindButton.Text = "Лупа";
            FindButton.UseVisualStyleBackColor = true;
            FindButton.Click += FindButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { NameOfStationColumn, NumberOfSeatsColumn, SoldTicketsColumn, NumberColumn, AverageAttendance, DateOfOpenColumn, AdressColumn });
            dataGridView1.Location = new Point(345, 28);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1017, 383);
            dataGridView1.TabIndex = 4;
            // 
            // NameOfStationColumn
            // 
            NameOfStationColumn.HeaderText = "Название";
            NameOfStationColumn.MinimumWidth = 6;
            NameOfStationColumn.Name = "NameOfStationColumn";
            NameOfStationColumn.Width = 125;
            // 
            // NumberOfSeatsColumn
            // 
            NumberOfSeatsColumn.HeaderText = "Количество мест";
            NumberOfSeatsColumn.MinimumWidth = 6;
            NumberOfSeatsColumn.Name = "NumberOfSeatsColumn";
            NumberOfSeatsColumn.Width = 125;
            // 
            // SoldTicketsColumn
            // 
            SoldTicketsColumn.HeaderText = "Продано билетов";
            SoldTicketsColumn.MinimumWidth = 6;
            SoldTicketsColumn.Name = "SoldTicketsColumn";
            SoldTicketsColumn.Width = 125;
            // 
            // NumberColumn
            // 
            NumberColumn.HeaderText = "Номер телефона";
            NumberColumn.MinimumWidth = 6;
            NumberColumn.Name = "NumberColumn";
            NumberColumn.Width = 125;
            // 
            // AverageAttendance
            // 
            AverageAttendance.HeaderText = "Средняя посещаемость";
            AverageAttendance.MinimumWidth = 6;
            AverageAttendance.Name = "AverageAttendance";
            AverageAttendance.Width = 125;
            // 
            // DateOfOpenColumn
            // 
            DateOfOpenColumn.HeaderText = "Дата открытия";
            DateOfOpenColumn.MinimumWidth = 6;
            DateOfOpenColumn.Name = "DateOfOpenColumn";
            DateOfOpenColumn.Width = 125;
            // 
            // AdressColumn
            // 
            AdressColumn.HeaderText = "Адрес";
            AdressColumn.MinimumWidth = 6;
            AdressColumn.Name = "AdressColumn";
            AdressColumn.Width = 125;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { ContainerColumn, InsertTimeColumn, SequentialReadTimeColumn, RandomReadTimeColumn });
            dataGridView2.Location = new Point(542, 419);
            dataGridView2.Margin = new Padding(3, 4, 3, 4);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(821, 204);
            dataGridView2.TabIndex = 5;
            dataGridView2.CellContentClick += dataGridView2_CellContentClick;
            // 
            // ContainerColumn
            // 
            ContainerColumn.HeaderText = "Контейнер";
            ContainerColumn.MinimumWidth = 6;
            ContainerColumn.Name = "ContainerColumn";
            ContainerColumn.Width = 125;
            // 
            // InsertTimeColumn
            // 
            InsertTimeColumn.HeaderText = "Время внесения данных";
            InsertTimeColumn.MinimumWidth = 6;
            InsertTimeColumn.Name = "InsertTimeColumn";
            InsertTimeColumn.Width = 125;
            // 
            // SequentialReadTimeColumn
            // 
            SequentialReadTimeColumn.HeaderText = "Время последовательного чтения";
            SequentialReadTimeColumn.MinimumWidth = 6;
            SequentialReadTimeColumn.Name = "SequentialReadTimeColumn";
            SequentialReadTimeColumn.Width = 125;
            // 
            // RandomReadTimeColumn
            // 
            RandomReadTimeColumn.HeaderText = "Время случайного чтения";
            RandomReadTimeColumn.MinimumWidth = 6;
            RandomReadTimeColumn.Name = "RandomReadTimeColumn";
            RandomReadTimeColumn.Width = 125;
            // 
            // CompareButton
            // 
            CompareButton.Location = new Point(402, 485);
            CompareButton.Margin = new Padding(3, 4, 3, 4);
            CompareButton.Name = "CompareButton";
            CompareButton.Size = new Size(133, 55);
            CompareButton.TabIndex = 6;
            CompareButton.Text = "Сравнить";
            CompareButton.UseVisualStyleBackColor = true;
            CompareButton.Click += CompareButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 143);
            label2.Name = "label2";
            label2.Size = new Size(151, 20);
            label2.TabIndex = 7;
            label2.Text = "Количество станций";
            // 
            // InfoLabel
            // 
            InfoLabel.AutoSize = true;
            InfoLabel.Location = new Point(19, 177);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(0, 20);
            InfoLabel.TabIndex = 8;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnInterpreterSearch);
            groupBox1.Controls.Add(txtInterpreterQuery);
            groupBox1.Location = new Point(25, 245);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(314, 433);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Interpretator search";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 343);
            label5.Name = "label5";
            label5.Size = new Size(239, 80);
            label5.TabIndex = 4;
            label5.Text = "Примеры:\r\nseats > 100\r\n(seats > 100 AND title <- \"Пенза\")\r\n\r\n";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 191);
            label4.Name = "label4";
            label4.Size = new Size(222, 140);
            label4.TabIndex = 3;
            label4.Text = "seats - места\r\nyear - год открытия\r\nsold - проданные билеты\r\nattendance - ср. посещаемость\r\ntitle - название\r\nphonenum - телефон\r\nadress - адрес\r\n";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 171);
            label3.Name = "label3";
            label3.Size = new Size(71, 20);
            label3.TabIndex = 2;
            label3.Text = "Словарь:";
            // 
            // btnInterpreterSearch
            // 
            btnInterpreterSearch.Location = new Point(97, 112);
            btnInterpreterSearch.Name = "btnInterpreterSearch";
            btnInterpreterSearch.Size = new Size(135, 40);
            btnInterpreterSearch.TabIndex = 1;
            btnInterpreterSearch.Text = "Лупа1";
            btnInterpreterSearch.UseVisualStyleBackColor = true;
            btnInterpreterSearch.Click += btnInterpreterSearch_Click;
            // 
            // txtInterpreterQuery
            // 
            txtInterpreterQuery.Location = new Point(16, 43);
            txtInterpreterQuery.Name = "txtInterpreterQuery";
            txtInterpreterQuery.Size = new Size(292, 27);
            txtInterpreterQuery.TabIndex = 0;
            txtInterpreterQuery.TextChanged += txtInterpreterQuery_TextChanged;
            // 
            // ContainerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1376, 681);
            Controls.Add(groupBox1);
            Controls.Add(InfoLabel);
            Controls.Add(label2);
            Controls.Add(CompareButton);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Controls.Add(FindButton);
            Controls.Add(ParamTextBox);
            Controls.Add(FindParamsComboBox);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ContainerForm";
            Text = "ContainerForm";
            Load += ContainerForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox FindParamsComboBox;
        private TextBox ParamTextBox;
        private Button FindButton;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn NameOfStationColumn;
        private DataGridViewTextBoxColumn NumberOfSeatsColumn;
        private DataGridViewTextBoxColumn SoldTicketsColumn;
        private DataGridViewTextBoxColumn NumberColumn;
        private DataGridViewTextBoxColumn AverageAttendance;
        private DataGridViewTextBoxColumn DateOfOpenColumn;
        private DataGridViewTextBoxColumn AdressColumn;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn ContainerColumn;
        private DataGridViewTextBoxColumn InsertTimeColumn;
        private DataGridViewTextBoxColumn SequentialReadTimeColumn;
        private DataGridViewTextBoxColumn RandomReadTimeColumn;
        private Button CompareButton;
        private Label label2;
        private Label InfoLabel;
        private GroupBox groupBox1;
        private Button btnInterpreterSearch;
        private TextBox txtInterpreterQuery;
        private Label label4;
        private Label label3;
        private Label label5;
    }
}