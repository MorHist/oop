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
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 21);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 0;
            label1.Text = "Искать вокзал";
            // 
            // FindParamsComboBox
            // 
            FindParamsComboBox.FormattingEnabled = true;
            FindParamsComboBox.Location = new Point(17, 39);
            FindParamsComboBox.Name = "FindParamsComboBox";
            FindParamsComboBox.Size = new Size(189, 23);
            FindParamsComboBox.TabIndex = 1;
            FindParamsComboBox.SelectedIndexChanged += FindParamsComboBox_SelectedIndexChanged;
            // 
            // ParamTextBox
            // 
            ParamTextBox.Location = new Point(17, 68);
            ParamTextBox.Name = "ParamTextBox";
            ParamTextBox.Size = new Size(189, 23);
            ParamTextBox.TabIndex = 2;
            // 
            // FindButton
            // 
            FindButton.Location = new Point(224, 44);
            FindButton.Name = "FindButton";
            FindButton.Size = new Size(48, 45);
            FindButton.TabIndex = 3;
            FindButton.Text = "Лупа";
            FindButton.UseVisualStyleBackColor = true;
            FindButton.Click += FindButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { NameOfStationColumn, NumberOfSeatsColumn, SoldTicketsColumn, NumberColumn, AverageAttendance, DateOfOpenColumn, AdressColumn });
            dataGridView1.Location = new Point(302, 21);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(890, 287);
            dataGridView1.TabIndex = 4;
            // 
            // NameOfStationColumn
            // 
            NameOfStationColumn.HeaderText = "Название";
            NameOfStationColumn.Name = "NameOfStationColumn";
            // 
            // NumberOfSeatsColumn
            // 
            NumberOfSeatsColumn.HeaderText = "Количество мест";
            NumberOfSeatsColumn.Name = "NumberOfSeatsColumn";
            // 
            // SoldTicketsColumn
            // 
            SoldTicketsColumn.HeaderText = "Продано билетов";
            SoldTicketsColumn.Name = "SoldTicketsColumn";
            // 
            // NumberColumn
            // 
            NumberColumn.HeaderText = "Номер телефона";
            NumberColumn.Name = "NumberColumn";
            // 
            // AverageAttendance
            // 
            AverageAttendance.HeaderText = "Средняя посещаемость";
            AverageAttendance.Name = "AverageAttendance";
            // 
            // DateOfOpenColumn
            // 
            DateOfOpenColumn.HeaderText = "Дата открытия";
            DateOfOpenColumn.Name = "DateOfOpenColumn";
            // 
            // AdressColumn
            // 
            AdressColumn.HeaderText = "Адрес";
            AdressColumn.Name = "AdressColumn";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { ContainerColumn, InsertTimeColumn, SequentialReadTimeColumn, RandomReadTimeColumn });
            dataGridView2.Location = new Point(474, 314);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(718, 153);
            dataGridView2.TabIndex = 5;
            dataGridView2.CellContentClick += dataGridView2_CellContentClick;
            // 
            // ContainerColumn
            // 
            ContainerColumn.HeaderText = "Контейнер";
            ContainerColumn.Name = "ContainerColumn";
            // 
            // InsertTimeColumn
            // 
            InsertTimeColumn.HeaderText = "Время внесения данных";
            InsertTimeColumn.Name = "InsertTimeColumn";
            // 
            // SequentialReadTimeColumn
            // 
            SequentialReadTimeColumn.HeaderText = "Время последовательного чтения";
            SequentialReadTimeColumn.Name = "SequentialReadTimeColumn";
            // 
            // RandomReadTimeColumn
            // 
            RandomReadTimeColumn.HeaderText = "Время случайного чтения";
            RandomReadTimeColumn.Name = "RandomReadTimeColumn";
            // 
            // CompareButton
            // 
            CompareButton.Location = new Point(352, 364);
            CompareButton.Name = "CompareButton";
            CompareButton.Size = new Size(116, 41);
            CompareButton.TabIndex = 6;
            CompareButton.Text = "Сравнить";
            CompareButton.UseVisualStyleBackColor = true;
            CompareButton.Click += CompareButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 107);
            label2.Name = "label2";
            label2.Size = new Size(120, 15);
            label2.TabIndex = 7;
            label2.Text = "Количество станций";
            // 
            // InfoLabel
            // 
            InfoLabel.AutoSize = true;
            InfoLabel.Location = new Point(17, 133);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(0, 15);
            InfoLabel.TabIndex = 8;
            // 
            // ContainerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1204, 479);
            Controls.Add(InfoLabel);
            Controls.Add(label2);
            Controls.Add(CompareButton);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Controls.Add(FindButton);
            Controls.Add(ParamTextBox);
            Controls.Add(FindParamsComboBox);
            Controls.Add(label1);
            Name = "ContainerForm";
            Text = "ContainerForm";
            Load += ContainerForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
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
    }
}