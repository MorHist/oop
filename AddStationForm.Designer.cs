namespace Lr1
{
    partial class AddStationForm
    {
        private System.ComponentModel.IContainer components = null;

        // Общие элементы (всегда видимые)
        private System.Windows.Forms.Label commonTitleLabel;
        private System.Windows.Forms.TextBox commonTitleTextBox;
        private System.Windows.Forms.Label commonPhoneLabel;
        private System.Windows.Forms.TextBox commonPhoneTextBox;
        private System.Windows.Forms.Label commonDateLabel;
        private System.Windows.Forms.DateTimePicker commonDatePicker;
        private System.Windows.Forms.Label commonAddressLabel;
        private System.Windows.Forms.TextBox commonAddressTextBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            commonTitleLabel = new Label();
            commonTitleTextBox = new TextBox();
            commonPhoneLabel = new Label();
            commonPhoneTextBox = new TextBox();
            commonDateLabel = new Label();
            commonDatePicker = new DateTimePicker();
            commonAddressLabel = new Label();
            commonAddressTextBox = new TextBox();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // commonTitleLabel
            // 
            commonTitleLabel.AutoSize = true;
            commonTitleLabel.Location = new Point(20, 20);
            commonTitleLabel.Name = "commonTitleLabel";
            commonTitleLabel.Size = new Size(80, 20);
            commonTitleLabel.TabIndex = 0;
            commonTitleLabel.Text = "Название:";
            // 
            // commonTitleTextBox
            // 
            commonTitleTextBox.Location = new Point(120, 17);
            commonTitleTextBox.Name = "commonTitleTextBox";
            commonTitleTextBox.Size = new Size(250, 27);
            commonTitleTextBox.TabIndex = 1;
            // 
            // commonPhoneLabel
            // 
            commonPhoneLabel.AutoSize = true;
            commonPhoneLabel.Location = new Point(20, 60);
            commonPhoneLabel.Name = "commonPhoneLabel";
            commonPhoneLabel.Size = new Size(72, 20);
            commonPhoneLabel.TabIndex = 2;
            commonPhoneLabel.Text = "Телефон:";
            // 
            // commonPhoneTextBox
            // 
            commonPhoneTextBox.Location = new Point(120, 57);
            commonPhoneTextBox.Name = "commonPhoneTextBox";
            commonPhoneTextBox.Size = new Size(250, 27);
            commonPhoneTextBox.TabIndex = 3;
            // 
            // commonDateLabel
            // 
            commonDateLabel.AutoSize = true;
            commonDateLabel.Location = new Point(20, 100);
            commonDateLabel.Name = "commonDateLabel";
            commonDateLabel.Size = new Size(113, 20);
            commonDateLabel.TabIndex = 4;
            commonDateLabel.Text = "Дата открытия:";
            // 
            // commonDatePicker
            // 
            commonDatePicker.Format = DateTimePickerFormat.Short;
            commonDatePicker.Location = new Point(140, 97);
            commonDatePicker.Name = "commonDatePicker";
            commonDatePicker.Size = new Size(230, 27);
            commonDatePicker.TabIndex = 5;
            // 
            // commonAddressLabel
            // 
            commonAddressLabel.AutoSize = true;
            commonAddressLabel.Location = new Point(20, 140);
            commonAddressLabel.Name = "commonAddressLabel";
            commonAddressLabel.Size = new Size(54, 20);
            commonAddressLabel.TabIndex = 6;
            commonAddressLabel.Text = "Адрес:";
            // 
            // commonAddressTextBox
            // 
            commonAddressTextBox.Location = new Point(120, 137);
            commonAddressTextBox.Name = "commonAddressTextBox";
            commonAddressTextBox.Size = new Size(250, 27);
            commonAddressTextBox.TabIndex = 7;
            // 
            // okButton
            // 
            okButton.Dock = DockStyle.Bottom;
            okButton.Location = new Point(0, 518);
            okButton.Name = "okButton";
            okButton.Size = new Size(450, 35);
            okButton.TabIndex = 8;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Dock = DockStyle.Bottom;
            cancelButton.Location = new Point(0, 483);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(450, 35);
            cancelButton.TabIndex = 9;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // AddStationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(450, 553);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(commonAddressTextBox);
            Controls.Add(commonAddressLabel);
            Controls.Add(commonDatePicker);
            Controls.Add(commonDateLabel);
            Controls.Add(commonPhoneTextBox);
            Controls.Add(commonPhoneLabel);
            Controls.Add(commonTitleTextBox);
            Controls.Add(commonTitleLabel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddStationForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Добавить станцию";
            ResumeLayout(false);
            PerformLayout();
        }
        private Button okButton;
        private Button cancelButton;
    }
}