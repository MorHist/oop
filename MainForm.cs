using System;
using System.Linq;
using System.Windows.Forms;

namespace Lr1
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Фасад для работы со станциями
        /// </summary>
        private StationFacade _stationFacade;

        public MainForm()
        {
            InitializeComponent();

            // Инициализируем фасад
            _stationFacade = new StationFacade();

            // Добавляем тестовые данные через фасад
            _stationFacade.CreateFullStation("Пенза-1", 120, 3020, "+79875634543", 78.6, DateTime.Now, "Володарского 12");
            _stationFacade.CreateFullStation("Пенза-2", 10, 3020, "+79888888883", 24.9, DateTime.Now, "Володарского 13");
            _stationFacade.CreateFullStation("Пенза-3", 12370, 3020, "+71234567890", 13.2, DateTime.Now, "Володарского 14");

            // Подписываемся на события фасада ПОСЛЕ загрузки формы
            // Это сделаем в обработчике Load
        }

        /// <summary>
        /// Обработчик изменения коллекции станций
        /// </summary>
        private void OnStationCollectionChanged(object sender, StationFacade.StationCollectionChangedEventArgs e)
        {
            // Проверяем, создан ли дескриптор окна
            if (!this.IsHandleCreated || this.IsDisposed)
                return;

            // Обновляем интерфейс при изменениях
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => {
                    SetStationInfo();
                    UpdateStatusBar($"Станций: {e.TotalCount}");
                }));
            }
            else
            {
                SetStationInfo();
                UpdateStatusBar($"Станций: {e.TotalCount}");
            }
        }

        /// <summary>
        /// Обновить строку состояния
        /// </summary>
        private void UpdateStatusBar(string message)
        {
            // Простое обновление текста в Info
            // или можно добавить StatusStrip в дизайнере
            Info.Text += $"\n{message}";
        }

        /// <summary>
        /// Метод проверяет текстбоксы на пустоту
        /// </summary>
        private void CheckField(object sender, EventArgs e)
        {
            foreach (var i in Controls.OfType<TextBox>())
                if (i.Text.Length == 0)
                {
                    UpdateStationBtn.Enabled = false;
                    AddNewStationBtn.Enabled = false;
                    return;
                }
            UpdateStationBtn.Enabled = true;
            AddNewStationBtn.Enabled = true;
        }

        /// <summary>
        /// Метод устанавливает отображает информацию в виде строки
        /// </summary>
        private void SetInfo()
        {
            var stats = _stationFacade.GetStatistics();
            Info.Text = 
                       $"Всего станций: {stats.TotalStations}\n" +
                       $"Общее количество мест: {stats.TotalSeats}\n" +
                       $"Продано билетов: {stats.TotalTicketsSold}\n" +
                       $"Средняя посещаемость: {stats.AverageAttendance:F1}\n" ;

            var currentStation = _stationFacade.GetCurrentStation();
            if (currentStation != null)
            {
                Info.Text += $"\nТекущая станция:\n{currentStation}";
            }
        }

        /// <summary>
        /// Метод устанавливает в текстбоксы поля выбранного вокзала
        /// </summary>
        private void SetStationInfo()
        {
            var currentStation = _stationFacade.GetCurrentStation();

            if (currentStation == null)
            {
                ClearFields();
                UpdateStationBtn.Enabled = false;
                AddNewStationBtn.Enabled = false;
                Info.Text = "Нет данных о станциях";
                return;
            }

            Title.Text = currentStation.Title;
            NumberOfSeats.Text = currentStation.NumberOfSeats?.ToString() ?? "";
            SoldTickets.Text = currentStation.SoldTickets?.ToString() ?? "";
            Number.Text = currentStation.Number ?? "";
            AverageAttendace.Text = currentStation.AverageAttendace?.ToString() ?? "";
            DateOfOpening.Value = currentStation.DateOfOpening;
            Address.Text = currentStation.Address ?? "";

            UpdateStationBtn.Enabled = true;
            AddNewStationBtn.Enabled = true;

            SetInfo();
            TicketsInHex.Text = currentStation.NumberOfSeatsToHex();
        }

        /// <summary>
        /// Очистка полей ввода
        /// </summary>
        private void ClearFields()
        {
            Title.Text = "";
            NumberOfSeats.Text = "";
            SoldTickets.Text = "";
            Number.Text = "";
            AverageAttendace.Text = "";
            DateOfOpening.Value = DateTime.Now;
            Address.Text = "";
            TicketsInHex.Text = "";
        }

        /// <summary>
        /// Метод вызывается при нажатии на кнопку "Сохранить"
        /// Обновляет поля текущей станции
        /// </summary>
        private void UpdateStationBtn_Click(object sender, EventArgs e)
        {
            var currentStation = _stationFacade.GetCurrentStation();
            if (currentStation == null) return;

            try
            {
                currentStation.Title = Title.Text;
                currentStation.NumberOfSeats = Convert.ToInt32(NumberOfSeats.Text);
                currentStation.SoldTickets = Convert.ToInt32(SoldTickets.Text);
                currentStation.AverageAttendace = Convert.ToDouble(AverageAttendace.Text.Replace('.', ','));
                currentStation.Number = Number.Text;
                currentStation.DateOfOpening = DateOfOpening.Value;
                currentStation.Address = Address.Text;

                TicketsInHex.Text = currentStation.NumberOfSeatsToHex();
                SetInfo();

                MessageBox.Show("Станция обновлена успешно!", "Успех");
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильные числовые данные", "Ошибка");
            }
            catch (NegativeValueException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            catch (WrongNumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            catch (InvalidDateOfOpeningException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        /// <summary>
        /// Метод вызывается при нажатии на кнопку "Добавить новый"
        /// Создаёт новый вокзал и добавляет его в коллекцию
        /// </summary>
        private void AddNewStationBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var newStation = _stationFacade.CreateFullStation(
                    Title.Text,
                    Convert.ToInt32(NumberOfSeats.Text),
                    Convert.ToInt32(SoldTickets.Text),
                    Number.Text,
                    Convert.ToDouble(AverageAttendace.Text.Replace('.', ',')),
                    DateOfOpening.Value,
                    Address.Text
                );

                TicketsInHex.Text = newStation.NumberOfSeatsToHex();
                SetInfo();

                MessageBox.Show($"Станция '{newStation.Title}' добавлена успешно!", "Успех");
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильные числовые данные", "Ошибка");
            }
            catch (NegativeValueException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            catch (WrongNumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            catch (InvalidDateOfOpeningException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {
            DateOfOpening.Format = DateTimePickerFormat.Custom;
            DateOfOpening.CustomFormat = "dd MMM yyyy";
            MessageBox.Show("Петряев и Маляев 23ВП1\nВариант 3", "Лабораторная работа №4");

            // Теперь подписываемся на события, когда форма уже загружена
            _stationFacade.CollectionChanged += OnStationCollectionChanged;

            SetStationInfo();
        }

        private void ErrorButton_Click(object sender, EventArgs e)
        {
            MyExeption exp = new MyExeption();
            try
            {
                exp.createExeption();
            }
            catch (MyDivideByZeroException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void OpenEditFormButton_Click(object sender, EventArgs e)
        {
            ContainerForm containerForm = new ContainerForm(_stationFacade);
            containerForm.Show();
        }

        /// <summary>
        /// Добавить кнопки для демонстрации возможностей фасада
        /// </summary>
        private void AddFacadeDemoButtons()
        {
            // Кнопка "Показать статистику"
            var statsButton = new Button
            {
                Text = "Статистика",
                Location = new System.Drawing.Point(OpenEditFormButton.Left,
                                                   OpenEditFormButton.Bottom + 10),
                Size = OpenEditFormButton.Size
            };
            statsButton.Click += (s, e) =>
            {
                var stats = _stationFacade.GetStatistics();
                MessageBox.Show(
                    $"=== СТАТИСТИКА СТАНЦИЙ ===\n" +
                    $"Всего станций: {stats.TotalStations}\n" +
                    $"Общее количество мест: {stats.TotalSeats}\n" +
                    $"Продано билетов: {stats.TotalTicketsSold}\n" +
                    $"Средняя посещаемость: {stats.AverageAttendance:F1}\n\n" +
                    $"Самая старая станция: {stats.OldestStation?.Title}\n" +
                    $"Самая новая станция: {stats.NewestStation?.Title}",
                    "Статистика"
                );
            };
            this.Controls.Add(statsButton);

            // Кнопка "Экспорт"
            var exportButton = new Button
            {
                Text = "Экспорт",
                Location = new System.Drawing.Point(statsButton.Left,
                                                   statsButton.Bottom + 10),
                Size = statsButton.Size
            };
            exportButton.Click += (s, e) =>
            {
                var exportText = _stationFacade.ExportToText();
                MessageBox.Show(exportText, "Экспорт данных",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            this.Controls.Add(exportButton);

            // Кнопка "Удалить последнюю"
            var removeButton = new Button
            {
                Text = "Удалить последнюю",
                Location = new System.Drawing.Point(exportButton.Left,
                                                   exportButton.Bottom + 10),
                Size = exportButton.Size
            };
            removeButton.Click += (s, e) =>
            {
                if (_stationFacade.RemoveLastStation())
                {
                    MessageBox.Show("Последняя станция удалена", "Удаление");
                }
                else
                {
                    MessageBox.Show("Нет станций для удаления", "Информация");
                }
            };
            this.Controls.Add(removeButton);
        }
    }
}