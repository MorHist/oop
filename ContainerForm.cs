using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lr1
{
    public partial class ContainerForm : Form
    {
        private StationContainer _stationContainer;
        private Random _random = new Random();

        // Конструктор, принимающий StationContainer
        public ContainerForm(StationContainer stationContainer)
        {
            InitializeComponent(); // ДОЛЖЕН БЫТЬ ТОЛЬКО ОДИН РАЗ

            _stationContainer = stationContainer;

            // Подписка на события
            _stationContainer.StationAdded += OnStationAdded;
            _stationContainer.StationRemoved += OnStationRemoved;

            // Инициализация интерфейса
            InitializeDataGridView();
            InitializeCompareDataGridView(); // Инициализация таблицы сравнения
            UpdateDataGridView();
            PopulateComboBox();
        }

        private void InitializeDataGridView()
        {
            // Настройка DataGridView для станций
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Добавление колонок
            dataGridView1.Columns.Add("Title", "Название");
            dataGridView1.Columns.Add("NumberOfSeats", "Количество мест");
            dataGridView1.Columns.Add("SoldTickets", "Проданные билеты");
            dataGridView1.Columns.Add("Number", "Телефон");
            dataGridView1.Columns.Add("AverageAttendace", "Средняя посещаемость");
            dataGridView1.Columns.Add("DateOfOpening", "Дата открытия");
            dataGridView1.Columns.Add("Address", "Адрес");
        }

        // Метод для инициализации таблицы сравнения
        private void InitializeCompareDataGridView()
        {
            // Проверяем, что dataGridView2 существует
            if (dataGridView2 == null)
            {
                MessageBox.Show("dataGridView2 не инициализирован!", "Ошибка");
                return;
            }

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.Columns.Clear();

            // Добавление колонок
            dataGridView2.Columns.Add("TypeColumn", "Тип контейнера");
            dataGridView2.Columns["TypeColumn"].Width = 200;

            dataGridView2.Columns.Add("InsertTimeColumn", "Время внесения данных, мс");
            dataGridView2.Columns["InsertTimeColumn"].Width = 150;

            dataGridView2.Columns.Add("SequentialReadTimeColumn", "Время последовательного чтения, мс");
            dataGridView2.Columns["SequentialReadTimeColumn"].Width = 180;

            dataGridView2.Columns.Add("RandomReadTimeColumn", "Время случайного чтения, мс");
            dataGridView2.Columns["RandomReadTimeColumn"].Width = 150;
        }

        private void UpdateDataGridView()
        {
            // Проверяем, что dataGridView1 существует
            if (dataGridView1 == null || dataGridView1.IsDisposed)
                return;

            // Получаем все станции в правильном порядке
            var stations = _stationContainer.GetAllStations();
            // Преобразуем Stack в List для правильного отображения
            var stationList = stations.Reverse().ToList();

            dataGridView1.Rows.Clear();

            foreach (var station in stationList)
            {
                dataGridView1.Rows.Add(
                    station.Title,
                    station.NumberOfSeats,
                    station.SoldTickets,
                    station.Number,
                    station.AverageAttendace,
                    station.DateOfOpening.ToShortDateString(),
                    station.Address
                );
            }

            // Обновляем информацию о количестве
            if (InfoLabel != null && !InfoLabel.IsDisposed)
            {
                InfoLabel.Text = $"Всего станций: {_stationContainer.CountOfStation(null)}";
            }
        }

        private void PopulateComboBox()
        {
            if (FindParamsComboBox == null) return;

            FindParamsComboBox.Items.Clear();
            FindParamsComboBox.Items.AddRange(new string[]
            {
                "Название",
                "Количество мест",
                "Проданные билеты",
                "Телефон",
                "Средняя посещаемость",
                "Адрес"
            });

            if (FindParamsComboBox.Items.Count > 0)
                FindParamsComboBox.SelectedIndex = 0;
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            string param = FindParamsComboBox.SelectedItem?.ToString();
            string searchValue = ParamTextBox.Text.ToLower();

            if (string.IsNullOrEmpty(param) || string.IsNullOrEmpty(searchValue))
            {
                UpdateDataGridView();
                return;
            }

            var stations = _stationContainer.GetAllStations();
            var stationList = stations.Reverse().ToList();
            var filteredList = new List<Station>();

            foreach (var station in stationList)
            {
                bool matches = false;

                switch (param)
                {
                    case "Название":
                        matches = station.Title.ToLower().Contains(searchValue);
                        break;

                    case "Количество мест":
                        matches = CheckNumberCondition(station.NumberOfSeats, searchValue, ">=");
                        break;

                    case "Проданные билеты":
                        matches = CheckNumberCondition(station.SoldTickets, searchValue, ">=");
                        break;

                    case "Телефон":
                        matches = station.Number.ToLower().Contains(searchValue);
                        break;

                    case "Средняя посещаемость":
                        matches = CheckDoubleCondition(station.AverageAttendace, searchValue, ">=");
                        break;

                    case "Адрес":
                        matches = station.Address.ToLower().Contains(searchValue);
                        break;
                }

                if (matches)
                    filteredList.Add(station);
            }

            // Отображаем отфильтрованный список
            DisplayStations(filteredList);
        }

        // Вспомогательные методы для проверки условий
        private bool CheckNumberCondition(int? value, string conditionString, string defaultOperator = ">=")
        {
            if (!value.HasValue)
                return false;

            string trimmed = conditionString.Trim();

            if (string.IsNullOrEmpty(trimmed))
                return false;

            string op = defaultOperator;
            string numberStr = trimmed;

            if (trimmed.StartsWith(">=") || trimmed.StartsWith("<=") || trimmed.StartsWith("=="))
            {
                op = trimmed.Substring(0, 2);
                numberStr = trimmed.Substring(2);
            }
            else if (trimmed.StartsWith(">") || trimmed.StartsWith("<") || trimmed.StartsWith("="))
            {
                op = trimmed.Substring(0, 1);
                numberStr = trimmed.Substring(1);
            }

            if (int.TryParse(numberStr, out int targetValue))
            {
                switch (op)
                {
                    case ">": return value.Value > targetValue;
                    case ">=": return value.Value >= targetValue;
                    case "<": return value.Value < targetValue;
                    case "<=": return value.Value <= targetValue;
                    case "=":
                    case "==": return value.Value == targetValue;
                    default: return false;
                }
            }

            return false;
        }

        private bool CheckDoubleCondition(double? value, string conditionString, string defaultOperator = ">=")
        {
            if (!value.HasValue)
                return false;

            string trimmed = conditionString.Trim().Replace(',', '.');

            if (string.IsNullOrEmpty(trimmed))
                return false;

            string op = defaultOperator;
            string numberStr = trimmed;

            if (trimmed.StartsWith(">=") || trimmed.StartsWith("<=") || trimmed.StartsWith("=="))
            {
                op = trimmed.Substring(0, 2);
                numberStr = trimmed.Substring(2);
            }
            else if (trimmed.StartsWith(">") || trimmed.StartsWith("<") || trimmed.StartsWith("="))
            {
                op = trimmed.Substring(0, 1);
                numberStr = trimmed.Substring(1);
            }

            if (double.TryParse(numberStr, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double targetValue))
            {
                switch (op)
                {
                    case ">": return value.Value > targetValue;
                    case ">=": return value.Value >= targetValue;
                    case "<": return value.Value < targetValue;
                    case "<=": return value.Value <= targetValue;
                    case "=":
                    case "==": return Math.Abs(value.Value - targetValue) < 0.0001;
                    default: return false;
                }
            }

            return false;
        }

        private void DisplayStations(List<Station> stations)
        {
            if (dataGridView1 == null || dataGridView1.IsDisposed)
                return;

            dataGridView1.Rows.Clear();

            foreach (var station in stations)
            {
                dataGridView1.Rows.Add(
                    station.Title,
                    station.NumberOfSeats,
                    station.SoldTickets,
                    station.Number,
                    station.AverageAttendace,
                    station.DateOfOpening.ToShortDateString(),
                    station.Address
                );
            }

            if (InfoLabel != null && !InfoLabel.IsDisposed)
            {
                InfoLabel.Text = $"Найдено станций: {stations.Count}";
            }
        }

        private void OnStationAdded(object sender, StationContainer.StationEventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateDataGridView()));
            }
            else
            {
                UpdateDataGridView();
            }
        }

        private void OnStationRemoved(object sender, StationContainer.StationEventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateDataGridView()));
            }
            else
            {
                UpdateDataGridView();
            }
        }

        private void FindParamsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ParamTextBox != null)
                ParamTextBox.Clear();
            UpdateDataGridView();
        }

        private void ContainerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_stationContainer != null)
            {
                _stationContainer.StationAdded -= OnStationAdded;
                _stationContainer.StationRemoved -= OnStationRemoved;
            }
        }

        private void ContainerForm_Load(object sender, EventArgs e)
        {
            // Уже инициализировано в конструкторе
        }

        private void CompareButton_Click(object sender, EventArgs e)
        {
            // Проверяем, что dataGridView2 существует
            if (dataGridView2 == null)
            {
                MessageBox.Show("Таблица сравнения не инициализирована!", "Ошибка");
                return;
            }

            // Очищаем таблицу сравнения
            dataGridView2.Rows.Clear();

            // Создаем массив (List) и ваш контейнер
            List<Station> arrayList = new List<Station>();
            StationContainer stationContainer = new StationContainer();

            // Опционально: отключаем события в контейнере для повышения производительности
            stationContainer.EnableEvents = false;

            // Замер времени внесения данных (100000 элементов)
            var insertArrayTime = MeasureInsertTime(arrayList);
            var insertContainerTime = MeasureInsertTime(stationContainer);

            // Замер времени последовательного чтения
            var sequentialArrayTime = MeasureSequentialReadTime(arrayList);
            var sequentialContainerTime = MeasureSequentialReadTime(stationContainer);

            // Замер времени случайного чтения
            var randomArrayTime = MeasureRandomReadTime(arrayList);
            var randomContainerTime = MeasureRandomReadTime(stationContainer);

            // Выводим результаты в таблицу
            dataGridView2.Rows.Add(
                "Массив (List<Station>)",
                $"{insertArrayTime.TotalMilliseconds:F2} мс",
                $"{sequentialArrayTime.TotalMilliseconds:F2} мс",
                $"{randomArrayTime.TotalMilliseconds:F2} мс"
            );

            dataGridView2.Rows.Add(
                "Класс-контейнер (StationContainer)",
                $"{insertContainerTime.TotalMilliseconds:F2} мс",
                $"{sequentialContainerTime.TotalMilliseconds:F2} мс",
                $"{randomContainerTime.TotalMilliseconds:F2} мс"
            );

            // Обновляем отображение таблицы
            dataGridView2.Refresh();
        }

        // Метод для замера времени внесения данных в массив
        private TimeSpan MeasureInsertTime(List<Station> list)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 100000; i++)
            {
                list.Add(CreateRandomStation(i));
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        // Метод для замера времени внесения данных в контейнер
        private TimeSpan MeasureInsertTime(StationContainer container)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 100000; i++)
            {
                container.AddStation(CreateRandomStation(i));
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        // Метод для замера времени последовательного чтения из массива
        private TimeSpan MeasureSequentialReadTime(List<Station> list)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int totalSeats = 0;
            foreach (var station in list)
            {
                totalSeats += station.NumberOfSeats ?? 0;
            }

            stopwatch.Stop();

            Debug.WriteLine($"Total seats in array: {totalSeats}");

            return stopwatch.Elapsed;
        }

        // Метод для замера времени последовательного чтения из контейнера
        private TimeSpan MeasureSequentialReadTime(StationContainer container)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int totalSeats = 0;
            var allStations = container.GetAllStations();

            foreach (var station in allStations)
            {
                totalSeats += station.NumberOfSeats ?? 0;
            }

            stopwatch.Stop();

            Debug.WriteLine($"Total seats in container: {totalSeats}");

            return stopwatch.Elapsed;
        }

        // Метод для замера времени случайного чтения из массива
        private TimeSpan MeasureRandomReadTime(List<Station> list)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int totalSeats = 0;
            for (int i = 0; i < 1000; i++) // Уменьшил до 1000 для скорости
            {
                int randomIndex = _random.Next(0, list.Count);
                totalSeats += list[randomIndex].NumberOfSeats ?? 0;
            }

            stopwatch.Stop();

            Debug.WriteLine($"Random read total seats in array: {totalSeats}");

            return stopwatch.Elapsed;
        }

        // Метод для замера времени случайного чтения из контейнера
        private TimeSpan MeasureRandomReadTime(StationContainer container)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var stations = container.GetAllStations().Reverse().ToList();

            int totalSeats = 0;
            for (int i = 0; i < 1000; i++) // Уменьшил до 1000 для скорости
            {
                int randomIndex = _random.Next(0, stations.Count);
                totalSeats += stations[randomIndex].NumberOfSeats ?? 0;
            }

            stopwatch.Stop();

            Debug.WriteLine($"Random read total seats in container: {totalSeats}");

            return stopwatch.Elapsed;
        }

        // Метод для создания случайной станции
        private Station CreateRandomStation(int index)
        {
            string[] stations = { "Москва", "Санкт-Петербург", "Новосибирск", "Екатеринбург", "Казань",
                                  "Нижний Новгород", "Челябинск", "Самара", "Омск", "Ростов-на-Дону" };
            string[] addresses = { "ул. Ленина", "пр. Победы", "ул. Советская", "ул. Мира", "пр. Ленина" };

            return new Station(
                $"{stations[_random.Next(stations.Length)]}-{index}",
                _random.Next(50, 500),
                _random.Next(0, 100),
                $"+7{_random.Next(900, 999)}{_random.Next(1000000, 9999999):D7}",
                _random.NextDouble() * 100,
                DateTime.Now.AddDays(-_random.Next(0, 3650)),
                $"{addresses[_random.Next(addresses.Length)]} {_random.Next(1, 100)}"
            );
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Пустая реализация, если не нужна
        }
    }
}