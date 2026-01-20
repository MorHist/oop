using System;
using System.Drawing;
using System.Windows.Forms;
using Lr1.Models;

namespace Lr1
{
    public partial class AddStationForm : Form
    {
        private string _stationType;
        private StationBase _createdStation;

        // Динамические элементы
        private Label[] dynamicLabels;
        private TextBox[] dynamicTextBoxes;
        private CheckBox[] dynamicCheckBoxes;
        private ComboBox[] dynamicComboBoxes;

        public StationBase CreatedStation => _createdStation;

        public AddStationForm(string stationType)
        {
            InitializeComponent();
            _stationType = stationType;
            InitializeDynamicControls();
            SetupForStationType();
        }

        private void InitializeDynamicControls()
        {
            // Инициализируем массивы
            dynamicLabels = new Label[5];
            dynamicTextBoxes = new TextBox[5];
            dynamicCheckBoxes = new CheckBox[2];
            dynamicComboBoxes = new ComboBox[2];

            int startY = 180; // Сдвигаем вниз, так как общие поля занимают место
            int spacing = 35;

            // Создаем динамические элементы
            for (int i = 0; i < 5; i++)
            {
                dynamicLabels[i] = new Label
                {
                    Name = $"dynamicLabel{i}",
                    Location = new Point(20, startY + (i * spacing)),
                    Size = new Size(180, 25),
                    Font = new Font("Microsoft Sans Serif", 10F),
                    Visible = false
                };

                dynamicTextBoxes[i] = new TextBox
                {
                    Name = $"dynamicTextBox{i}",
                    Location = new Point(210, startY + (i * spacing)),
                    Size = new Size(200, 27),
                    Font = new Font("Microsoft Sans Serif", 10F),
                    Visible = false,
                    Tag = i
                };

                this.Controls.Add(dynamicLabels[i]);
                this.Controls.Add(dynamicTextBoxes[i]);
            }

            for (int i = 0; i < 2; i++)
            {
                dynamicCheckBoxes[i] = new CheckBox
                {
                    Name = $"dynamicCheckBox{i}",
                    Location = new Point(210, startY + ((5 + i) * spacing)),
                    Size = new Size(200, 25),
                    Font = new Font("Microsoft Sans Serif", 10F),
                    Visible = false
                };

                dynamicComboBoxes[i] = new ComboBox
                {
                    Name = $"dynamicComboBox{i}",
                    Location = new Point(210, startY + ((7 + i) * spacing)),
                    Size = new Size(200, 28),
                    Visible = false,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                this.Controls.Add(dynamicCheckBoxes[i]);
                this.Controls.Add(dynamicComboBoxes[i]);
            }
        }

        private void SetupForStationType()
        {
            this.Text = $"Добавить {_stationType}";

            // Общие поля всегда видимы
            commonTitleLabel.Visible = true;
            commonTitleTextBox.Visible = true;
            commonPhoneLabel.Visible = true;
            commonPhoneTextBox.Visible = true;
            commonDateLabel.Visible = true;
            commonDatePicker.Visible = true;
            commonAddressLabel.Visible = true;
            commonAddressTextBox.Visible = true;

            // Сначала скрываем ВСЕ динамические элементы
            HideAllDynamicControls();

            // Настраиваем специфичные поля
            switch (_stationType)
            {
                case "Вокзал":
                    SetupForRailwayStation();
                    break;

                case "Полустанок":
                    SetupForHalte();
                    break;

                case "Узел":
                    SetupForHub();
                    break;
            }

            // Перемещаем кнопки вниз в зависимости от видимых элементов
            PositionButtons();
        }

        private void HideAllDynamicControls()
        {
            // Скрываем все динамические элементы
            for (int i = 0; i < 5; i++)
            {
                dynamicLabels[i].Visible = false;
                dynamicTextBoxes[i].Visible = false;
                dynamicTextBoxes[i].Text = ""; // Очищаем текст
            }

            for (int i = 0; i < 2; i++)
            {
                dynamicCheckBoxes[i].Visible = false;
                dynamicCheckBoxes[i].Checked = false; // Сбрасываем чекбоксы
                dynamicComboBoxes[i].Visible = false;
                dynamicComboBoxes[i].Items.Clear(); // Очищаем ComboBox
            }
        }

        private void SetupForRailwayStation()
        {
            // Показываем нужные элементы для Вокзала
            dynamicLabels[0].Text = "Количество мест:";
            dynamicLabels[0].Visible = true;
            dynamicTextBoxes[0].Visible = true;

            dynamicLabels[1].Text = "Продано билетов:";
            dynamicLabels[1].Visible = true;
            dynamicTextBoxes[1].Visible = true;

            dynamicLabels[2].Text = "Средняя посещаемость:";
            dynamicLabels[2].Visible = true;
            dynamicTextBoxes[2].Visible = true;

            dynamicLabels[3].Text = "Количество касс:";
            dynamicLabels[3].Visible = true;
            dynamicTextBoxes[3].Visible = true;

            // Чекбокс для камеры хранения
            dynamicCheckBoxes[0].Text = "Камера хранения";
            dynamicCheckBoxes[0].Visible = true;

            // ComboBox для типа вокзала
            dynamicLabels[4].Text = "Тип вокзала:";
            dynamicLabels[4].Visible = true;
            dynamicComboBoxes[0].Visible = true;
            dynamicComboBoxes[0].Items.AddRange(new string[] { "Междугородный", "Пригородный", "Скоростной", "Тупиковый" });
            dynamicComboBoxes[0].SelectedIndex = 0;
        }

        private void SetupForHalte()
        {
            // Для полустанка
            dynamicLabels[0].Text = "Количество платформ:";
            dynamicLabels[0].Visible = true;
            dynamicTextBoxes[0].Visible = true;

            // Чекбокс для навеса
            dynamicCheckBoxes[0].Text = "Есть навес от дождя";
            dynamicCheckBoxes[0].Visible = true;

            // ComboBox для типа полустанка
            dynamicLabels[1].Text = "Тип полустанка:";
            dynamicLabels[1].Visible = true;
            dynamicComboBoxes[0].Visible = true;
            dynamicComboBoxes[0].Items.AddRange(new string[] { "Пассажирский", "Грузопассажирский", "Технический" });
            dynamicComboBoxes[0].SelectedIndex = 0;
        }

        private void SetupForHub()
        {
            // Для узла
            dynamicLabels[0].Text = "Количество кранов:";
            dynamicLabels[0].Visible = true;
            dynamicTextBoxes[0].Visible = true;

            dynamicLabels[1].Text = "Грузопередача (т/час):";
            dynamicLabels[1].Visible = true;
            dynamicTextBoxes[1].Visible = true;

            dynamicLabels[2].Text = "Грузовые платформы:";
            dynamicLabels[2].Visible = true;
            dynamicTextBoxes[2].Visible = true;

            // Чекбокс для весового контроля
            dynamicCheckBoxes[0].Text = "Весовой контроль";
            dynamicCheckBoxes[0].Visible = true;

            // ComboBox для типа узла
            dynamicLabels[3].Text = "Тип узла:";
            dynamicLabels[3].Visible = true;
            dynamicComboBoxes[0].Visible = true;
            dynamicComboBoxes[0].Items.AddRange(new string[] { "Сортировочный", "Промежуточный", "Конечный", "Промышленный" });
            dynamicComboBoxes[0].SelectedIndex = 0;
        }

        private void PositionButtons()
        {
            // Определяем самую нижнюю видимую позицию
            int maxY = 180; // Начальная позиция динамических элементов

            // Ищем последний видимый элемент
            for (int i = 4; i >= 0; i--)
            {
                if (dynamicLabels[i].Visible)
                {
                    maxY = dynamicLabels[i].Location.Y + 40;
                    break;
                }
            }

            for (int i = 1; i >= 0; i--)
            {
                if (dynamicCheckBoxes[i].Visible)
                {
                    maxY = Math.Max(maxY, dynamicCheckBoxes[i].Location.Y + 40);
                }
                if (dynamicComboBoxes[i].Visible)
                {
                    maxY = Math.Max(maxY, dynamicComboBoxes[i].Location.Y + 40);
                }
            }

            // Устанавливаем позицию кнопок
            okButton.Location = new Point(100, maxY + 20);
            cancelButton.Location = new Point(220, maxY + 20);

            // Изменяем размер формы
            this.ClientSize = new Size(450, maxY + 100);
        }

        private bool ValidateInputs()
        {
            // Проверка общих полей
            if (string.IsNullOrWhiteSpace(commonTitleTextBox.Text))
            {
                MessageBox.Show("Введите название станции", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(commonPhoneTextBox.Text))
            {
                MessageBox.Show("Введите телефон", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(commonAddressTextBox.Text))
            {
                MessageBox.Show("Введите адрес", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка специфичных полей в зависимости от типа
            switch (_stationType)
            {
                case "Вокзал":
                    return ValidateRailwayStation();

                case "Полустанок":
                    return ValidateHalte();

                case "Узел":
                    return ValidateHub();
            }

            return true;
        }

        public void SetStationData(StationBase station)
        {
            // Общие поля
            commonTitleTextBox.Text = station.Title ?? "";
            commonPhoneTextBox.Text = station.Number ?? "";
            commonDatePicker.Value = station.DateOfOpening;
            commonAddressTextBox.Text = station.Address ?? "";

            // Специфичные поля
            if (station is RailwayStation railwayStation)
            {
                dynamicTextBoxes[0].Text = railwayStation.NumberOfSeats?.ToString() ?? "";
                dynamicTextBoxes[1].Text = railwayStation.SoldTickets?.ToString() ?? "";
                dynamicTextBoxes[2].Text = railwayStation.AverageAttendance?.ToString("F2") ?? "";
                dynamicTextBoxes[3].Text = railwayStation.TicketOfficesCount.ToString();
                dynamicCheckBoxes[0].Checked = railwayStation.HasLeftLuggageOffice;
            }
            else if (station is Halte halte)
            {
                dynamicTextBoxes[0].Text = halte.PlatformsCount.ToString();
                dynamicCheckBoxes[0].Checked = halte.HasRoof;
            }
            else if (station is Hub hub)
            {
                dynamicTextBoxes[0].Text = hub.CranesCount.ToString();
                dynamicTextBoxes[1].Text = hub.CargoThroughput.ToString("F2");
                dynamicTextBoxes[2].Text = hub.CargoPlatformsCount.ToString();
                dynamicCheckBoxes[0].Checked = hub.HasWeightControl;
            }
        }

        private bool ValidateRailwayStation()
        {
            // Количество мест
            if (string.IsNullOrWhiteSpace(dynamicTextBoxes[0].Text))
            {
                MessageBox.Show("Введите количество мест", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(dynamicTextBoxes[0].Text, out int seats) || seats <= 0)
            {
                MessageBox.Show("Количество мест должно быть положительным числом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Количество касс
            if (string.IsNullOrWhiteSpace(dynamicTextBoxes[3].Text))
            {
                MessageBox.Show("Введите количество касс", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(dynamicTextBoxes[3].Text, out int offices) || offices <= 0)
            {
                MessageBox.Show("Количество касс должно быть положительным числом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Проверка необязательных полей
            if (!string.IsNullOrWhiteSpace(dynamicTextBoxes[1].Text))
            {
                if (!int.TryParse(dynamicTextBoxes[1].Text, out int tickets) || tickets < 0)
                {
                    MessageBox.Show("Количество проданных билетов должно быть неотрицательным числом", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(dynamicTextBoxes[2].Text))
            {
                if (!double.TryParse(dynamicTextBoxes[2].Text.Replace('.', ','), out double attendance) || attendance < 0)
                {
                    MessageBox.Show("Средняя посещаемость должна быть неотрицательным числом", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private bool ValidateHalte()
        {
            // Количество платформ
            if (string.IsNullOrWhiteSpace(dynamicTextBoxes[0].Text))
            {
                MessageBox.Show("Введите количество платформ", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(dynamicTextBoxes[0].Text, out int platforms) || platforms <= 0)
            {
                MessageBox.Show("Количество платформ должно быть положительным числом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ValidateHub()
        {
            // Количество кранов
            if (string.IsNullOrWhiteSpace(dynamicTextBoxes[0].Text))
            {
                MessageBox.Show("Введите количество кранов", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(dynamicTextBoxes[0].Text, out int cranes) || cranes <= 0)
            {
                MessageBox.Show("Количество кранов должно быть положительным числом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Грузопередача
            if (string.IsNullOrWhiteSpace(dynamicTextBoxes[1].Text))
            {
                MessageBox.Show("Введите грузопередачу", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!double.TryParse(dynamicTextBoxes[1].Text.Replace('.', ','), out double throughput) || throughput <= 0)
            {
                MessageBox.Show("Грузопередача должна быть положительным числом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Грузовые платформы
            if (string.IsNullOrWhiteSpace(dynamicTextBoxes[2].Text))
            {
                MessageBox.Show("Введите количество грузовых платформ", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(dynamicTextBoxes[2].Text, out int cargoPlatforms) || cargoPlatforms <= 0)
            {
                MessageBox.Show("Количество грузовых платформ должно быть положительным числом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void CreateStation()
        {
            try
            {
                switch (_stationType)
                {
                    case "Вокзал":
                        _createdStation = CreateRailwayStation();
                        break;

                    case "Полустанок":
                        _createdStation = CreateHalte();
                        break;

                    case "Узел":
                        _createdStation = CreateHub();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания станции: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _createdStation = null;
            }
        }

        private RailwayStation CreateRailwayStation()
        {
            var station = new RailwayStation(commonTitleTextBox.Text)
            {
                Number = commonPhoneTextBox.Text,
                DateOfOpening = commonDatePicker.Value,
                Address = commonAddressTextBox.Text,
                NumberOfSeats = Convert.ToInt32(dynamicTextBoxes[0].Text),
                TicketOfficesCount = Convert.ToInt32(dynamicTextBoxes[3].Text),
                HasLeftLuggageOffice = dynamicCheckBoxes[0].Checked
            };

            // Необязательные поля
            if (!string.IsNullOrWhiteSpace(dynamicTextBoxes[1].Text))
                station.SoldTickets = Convert.ToInt32(dynamicTextBoxes[1].Text);

            if (!string.IsNullOrWhiteSpace(dynamicTextBoxes[2].Text))
                station.AverageAttendance = Convert.ToDouble(dynamicTextBoxes[2].Text.Replace('.', ','));

            return station;
        }

        private Halte CreateHalte()
        {
            var station = new Halte(
                commonTitleTextBox.Text,
                Convert.ToInt32(dynamicTextBoxes[0].Text),
                dynamicCheckBoxes[0].Checked)
            {
                Number = commonPhoneTextBox.Text,
                DateOfOpening = commonDatePicker.Value,
                Address = commonAddressTextBox.Text
            };

            return station;
        }

        private Hub CreateHub()
        {
            var station = new Hub(
                commonTitleTextBox.Text,
                Convert.ToInt32(dynamicTextBoxes[0].Text),
                Convert.ToDouble(dynamicTextBoxes[1].Text.Replace('.', ',')),
                Convert.ToInt32(dynamicTextBoxes[2].Text),
                dynamicCheckBoxes[0].Checked)
            {
                Number = commonPhoneTextBox.Text,
                DateOfOpening = commonDatePicker.Value,
                Address = commonAddressTextBox.Text
            };

            return station;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                CreateStation();
                if (_createdStation != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}