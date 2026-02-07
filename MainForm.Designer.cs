namespace Lr1
{
    partial class MainForm
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            AverageAttendace = new TextBox();
            Title = new TextBox();
            NumberOfSeats = new TextBox();
            SoldTickets = new TextBox();
            Number = new TextBox();
            Address = new TextBox();
            DateOfOpening = new DateTimePicker();
            Info = new Label();
            UpdateStationBtn = new Button();
            AddNewStationBtn = new Button();
            FieldValue = new Label();
            panel1 = new Panel();
            label8 = new Label();
            TicketsInHex = new TextBox();
            ErrorButton = new Button();
            OpenEditFormButton = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 12F);
            label1.Location = new Point(13, 27);
            label1.Name = "label1";
            label1.Size = new Size(149, 20);
            label1.TabIndex = 0;
            label1.Text = "Название вокзала";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 12F);
            label2.Location = new Point(13, 60);
            label2.Name = "label2";
            label2.Size = new Size(141, 20);
            label2.TabIndex = 1;
            label2.Text = "Количество мест";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 12F);
            label3.Location = new Point(13, 91);
            label3.Name = "label3";
            label3.Size = new Size(145, 20);
            label3.TabIndex = 2;
            label3.Text = "Продано билетов";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 12F);
            label4.Location = new Point(13, 124);
            label4.Name = "label4";
            label4.Size = new Size(159, 20);
            label4.TabIndex = 3;
            label4.Text = "Телефонный номер";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 12F);
            label5.Location = new Point(13, 158);
            label5.Name = "label5";
            label5.Size = new Size(192, 20);
            label5.TabIndex = 4;
            label5.Text = "Средняя посещаемость";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 12F);
            label6.Location = new Point(13, 193);
            label6.Name = "label6";
            label6.Size = new Size(125, 20);
            label6.TabIndex = 5;
            label6.Text = "Дата открытия";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 12F);
            label7.Location = new Point(13, 229);
            label7.Name = "label7";
            label7.Size = new Size(57, 20);
            label7.TabIndex = 6;
            label7.Text = "Адрес";
            // 
            // AverageAttendace
            // 
            AverageAttendace.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AverageAttendace.Font = new Font("Microsoft Sans Serif", 12F);
            AverageAttendace.Location = new Point(237, 156);
            AverageAttendace.MaxLength = 9;
            AverageAttendace.Name = "AverageAttendace";
            AverageAttendace.Size = new Size(176, 26);
            AverageAttendace.TabIndex = 13;
            AverageAttendace.TextChanged += CheckField;
            // 
            // Title
            // 
            Title.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Title.Font = new Font("Microsoft Sans Serif", 12F);
            Title.Location = new Point(237, 29);
            Title.MaxLength = 25;
            Title.Name = "Title";
            Title.Size = new Size(176, 26);
            Title.TabIndex = 9;
            Title.TextChanged += CheckField;
            // 
            // NumberOfSeats
            // 
            NumberOfSeats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NumberOfSeats.Font = new Font("Microsoft Sans Serif", 12F);
            NumberOfSeats.Location = new Point(237, 62);
            NumberOfSeats.MaxLength = 9;
            NumberOfSeats.Name = "NumberOfSeats";
            NumberOfSeats.Size = new Size(176, 26);
            NumberOfSeats.TabIndex = 10;
            NumberOfSeats.TextChanged += CheckField;
            // 
            // SoldTickets
            // 
            SoldTickets.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SoldTickets.Font = new Font("Microsoft Sans Serif", 12F);
            SoldTickets.Location = new Point(237, 93);
            SoldTickets.MaxLength = 9;
            SoldTickets.Name = "SoldTickets";
            SoldTickets.Size = new Size(176, 26);
            SoldTickets.TabIndex = 11;
            SoldTickets.TextChanged += CheckField;
            // 
            // Number
            // 
            Number.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Number.Font = new Font("Microsoft Sans Serif", 12F);
            Number.Location = new Point(237, 126);
            Number.MaxLength = 25;
            Number.Name = "Number";
            Number.Size = new Size(176, 26);
            Number.TabIndex = 12;
            Number.TextChanged += CheckField;
            // 
            // Address
            // 
            Address.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Address.Font = new Font("Microsoft Sans Serif", 12F);
            Address.Location = new Point(237, 231);
            Address.MaxLength = 25;
            Address.Name = "Address";
            Address.Size = new Size(176, 26);
            Address.TabIndex = 15;
            Address.TextChanged += CheckField;
            // 
            // DateOfOpening
            // 
            DateOfOpening.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            DateOfOpening.Font = new Font("Microsoft Sans Serif", 12F);
            DateOfOpening.ImeMode = ImeMode.NoControl;
            DateOfOpening.Location = new Point(237, 193);
            DateOfOpening.Name = "DateOfOpening";
            DateOfOpening.Size = new Size(176, 26);
            DateOfOpening.TabIndex = 14;
            DateOfOpening.Value = new DateTime(2024, 2, 12, 0, 0, 0, 0);
            // 
            // Info
            // 
            Info.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Info.AutoSize = true;
            Info.Font = new Font("Segoe UI", 12F);
            Info.Location = new Point(13, 363);
            Info.Name = "Info";
            Info.Size = new Size(0, 21);
            Info.TabIndex = 18;
            // 
            // UpdateStationBtn
            // 
            UpdateStationBtn.Anchor = AnchorStyles.Left;
            UpdateStationBtn.Font = new Font("Segoe UI", 12F);
            UpdateStationBtn.Location = new Point(13, 276);
            UpdateStationBtn.Margin = new Padding(3, 2, 3, 2);
            UpdateStationBtn.Name = "UpdateStationBtn";
            UpdateStationBtn.Size = new Size(175, 33);
            UpdateStationBtn.TabIndex = 19;
            UpdateStationBtn.Text = "Сохранить";
            UpdateStationBtn.UseVisualStyleBackColor = true;
            UpdateStationBtn.Click += UpdateStationBtn_Click;
            // 
            // AddNewStationBtn
            // 
            AddNewStationBtn.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            AddNewStationBtn.Font = new Font("Segoe UI", 12F);
            AddNewStationBtn.Location = new Point(238, 276);
            AddNewStationBtn.Margin = new Padding(3, 2, 3, 2);
            AddNewStationBtn.Name = "AddNewStationBtn";
            AddNewStationBtn.Size = new Size(175, 33);
            AddNewStationBtn.TabIndex = 21;
            AddNewStationBtn.Text = "Добавить новый";
            AddNewStationBtn.UseVisualStyleBackColor = true;
            AddNewStationBtn.Click += AddNewStationBtn_Click;
            // 
            // FieldValue
            // 
            FieldValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            FieldValue.AutoSize = true;
            FieldValue.Font = new Font("Segoe UI", 12F);
            FieldValue.Location = new Point(273, 587);
            FieldValue.Name = "FieldValue";
            FieldValue.Size = new Size(0, 21);
            FieldValue.TabIndex = 24;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Chocolate;
            panel1.Controls.Add(label8);
            panel1.Controls.Add(TicketsInHex);
            panel1.Location = new Point(433, 27);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(164, 92);
            panel1.TabIndex = 25;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(24, 6);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(101, 30);
            label8.TabIndex = 1;
            label8.Text = "Количество мест\r\nв 16-ричной СС";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TicketsInHex
            // 
            TicketsInHex.Location = new Point(12, 38);
            TicketsInHex.Margin = new Padding(2);
            TicketsInHex.Name = "TicketsInHex";
            TicketsInHex.Size = new Size(143, 23);
            TicketsInHex.TabIndex = 0;
            // 
            // ErrorButton
            // 
            ErrorButton.Font = new Font("Segoe UI", 12F);
            ErrorButton.Location = new Point(433, 276);
            ErrorButton.Name = "ErrorButton";
            ErrorButton.Size = new Size(175, 33);
            ErrorButton.TabIndex = 26;
            ErrorButton.Text = "Вызов ошибки";
            ErrorButton.UseVisualStyleBackColor = true;
            ErrorButton.Click += ErrorButton_Click;
            // 
            // OpenEditFormButton
            // 
            OpenEditFormButton.Location = new Point(472, 621);
            OpenEditFormButton.Name = "OpenEditFormButton";
            OpenEditFormButton.Size = new Size(136, 36);
            OpenEditFormButton.TabIndex = 27;
            OpenEditFormButton.Text = "Редактировать";
            OpenEditFormButton.UseVisualStyleBackColor = true;
            OpenEditFormButton.Click += OpenEditFormButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(615, 669);
            Controls.Add(OpenEditFormButton);
            Controls.Add(ErrorButton);
            Controls.Add(panel1);
            Controls.Add(FieldValue);
            Controls.Add(AddNewStationBtn);
            Controls.Add(UpdateStationBtn);
            Controls.Add(Info);
            Controls.Add(DateOfOpening);
            Controls.Add(Address);
            Controls.Add(Number);
            Controls.Add(SoldTickets);
            Controls.Add(NumberOfSeats);
            Controls.Add(Title);
            Controls.Add(AverageAttendace);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вокзал";
            Load += MainForm_Load_1;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox AverageAttendace;
        private TextBox Title;
        private TextBox NumberOfSeats;
        private TextBox SoldTickets;
        private TextBox Number;
        private TextBox Address;
        private DateTimePicker DateOfOpening;
        private Label Info;
        private Button UpdateStationBtn;
        private Button AddNewStationBtn;
        private Label FieldValue;
        private Panel panel1;
        private Label label8;
        private TextBox TicketsInHex;
        private Button ErrorButton;
        private Button OpenEditFormButton;
    }
}
