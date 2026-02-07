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
        private StationFacade _stationFacade;
        private Random _random = new Random();

        // Конструктор, принимающий StationFacade
        public ContainerForm(StationFacade stationFacade)
        {
            InitializeComponent();
            _stationFacade = stationFacade;

            InitializeDataGridView();
            InitializeCompareDataGridView();
            UpdateDataGridView();
            PopulateComboBox();
        }

        /// <summary>
        /// Обработчик изменения коллекции станций
        /// </summary>
        private void OnStationCollectionChanged(object sender, StationFacade.StationCollectionChangedEventArgs e)
        {
            // Проверяем, создан ли дескриптор окна
            if (!this.IsHandleCreated || this.IsDisposed)
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

        #region Инициализация UI

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Title", "Название");
            dataGridView1.Columns.Add("NumberOfSeats", "Количество мест");
            dataGridView1.Columns.Add("SoldTickets", "Проданные билеты");
            dataGridView1.Columns.Add("Number", "Телефон");
            dataGridView1.Columns.Add("AverageAttendace", "Средняя посещаемость");
            dataGridView1.Columns.Add("DateOfOpening", "Дата открытия");
            dataGridView1.Columns.Add("Address", "Адрес");
        }

        private void InitializeCompareDataGridView()
        {
            if (dataGridView2 == null)
            {
                MessageBox.Show("dataGridView2 не инициализирован!", "Ошибка");
                return;
            }

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add("TypeColumn", "Тип контейнера");
            dataGridView2.Columns["TypeColumn"].Width = 180;

            dataGridView2.Columns.Add("InsertTimeColumn", "Время заполнения (100000 элементов), мс");
            dataGridView2.Columns["InsertTimeColumn"].Width = 220;

            dataGridView2.Columns.Add("SequentialReadTimeColumn", "Время последовательного чтения, мс");
            dataGridView2.Columns["SequentialReadTimeColumn"].Width = 200;

            dataGridView2.Columns.Add("MemoryUsageColumn", "Использование памяти, МБ");
            dataGridView2.Columns["MemoryUsageColumn"].Width = 160;
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

        #endregion

        #region Работа с фасадом

        private void UpdateDataGridView()
        {
            if (dataGridView1 == null || dataGridView1.IsDisposed)
                return;

            dataGridView1.Rows.Clear();

            // Используем метод фасада для получения станций
            foreach (var station in _stationFacade.GetAllStations())
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
                InfoLabel.Text = $"Всего станций: {_stationFacade.GetTotalCount()}";
            }
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

            // Используем возможности фасада для поиска
            IEnumerable<Station> foundStations = null;

            switch (param)
            {
                case "Название":
                    foundStations = _stationFacade.FindByName(searchValue);
                    break;

                case "Количество мест":
                    if (int.TryParse(searchValue, out int minSeats))
                        foundStations = _stationFacade.FindByMinCapacity(minSeats);
                    break;

                case "Проданные билеты":
                    foundStations = SearchBySoldTickets(searchValue);
                    break;

                case "Телефон":
                    foundStations = SearchByPhone(searchValue);
                    break;

                case "Средняя посещаемость":
                    foundStations = SearchByAttendance(searchValue);
                    break;

                case "Адрес":
                    foundStations = SearchByAddress(searchValue);
                    break;
            }

            DisplayFoundStations(foundStations);
        }

        private IEnumerable<Station> SearchBySoldTickets(string condition)
        {
            var allStations = _stationFacade.GetAllStations();

            if (string.IsNullOrEmpty(condition))
                return allStations;

            if (int.TryParse(condition, out int value))
                return allStations.Where(s => s.SoldTickets >= value);

            return CheckNumberCondition(allStations, condition, s => s.SoldTickets);
        }

        private IEnumerable<Station> SearchByPhone(string searchValue)
        {
            return _stationFacade.GetAllStations()
                .Where(s => s.Number?.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private IEnumerable<Station> SearchByAttendance(string condition)
        {
            var allStations = _stationFacade.GetAllStations();

            if (string.IsNullOrEmpty(condition))
                return allStations;

            if (double.TryParse(condition.Replace(',', '.'), out double value))
                return allStations.Where(s => s.AverageAttendace >= value);

            return CheckDoubleCondition(allStations, condition, s => s.AverageAttendace);
        }

        private IEnumerable<Station> SearchByAddress(string searchValue)
        {
            return _stationFacade.GetAllStations()
                .Where(s => s.Address?.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void DisplayFoundStations(IEnumerable<Station> stations)
        {
            if (dataGridView1 == null || dataGridView1.IsDisposed || stations == null)
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
                InfoLabel.Text = $"Найдено станций: {stations.Count()}";
            }
        }

        #endregion

        #region Вспомогательные методы для поиска

        private IEnumerable<Station> CheckNumberCondition(IEnumerable<Station> stations,
                                                         string conditionString,
                                                         Func<Station, int?> valueSelector,
                                                         string defaultOperator = ">=")
        {
            string trimmed = conditionString.Trim();

            if (string.IsNullOrEmpty(trimmed))
                return stations;

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
                return stations.Where(station =>
                {
                    var value = valueSelector(station);
                    if (!value.HasValue) return false;

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
                });
            }

            return Enumerable.Empty<Station>();
        }

        private IEnumerable<Station> CheckDoubleCondition(IEnumerable<Station> stations,
                                                         string conditionString,
                                                         Func<Station, double?> valueSelector,
                                                         string defaultOperator = ">=")
        {
            string trimmed = conditionString.Trim().Replace(',', '.');

            if (string.IsNullOrEmpty(trimmed))
                return stations;

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
                return stations.Where(station =>
                {
                    var value = valueSelector(station);
                    if (!value.HasValue) return false;

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
                });
            }

            return Enumerable.Empty<Station>();
        }

        #endregion

        #region Тестирование производительности

        private void CompareButton_Click(object sender, EventArgs e)
        {
            if (dataGridView2 == null)
            {
                MessageBox.Show("Таблица сравнения не инициализирована!", "Ошибка");
                return;
            }

            dataGridView2.Rows.Clear();

            // Для тестирования используем отдельные объекты
            var testContainer = new StationContainer();
            testContainer.EnableEvents = false;

            Station[] array = new Station[100000];

            PerformAllTests(array, testContainer);
            testContainer = null;
            GC.Collect();
        }

        private void PerformAllTests(Station[] array, StationContainer container)
        {
            // Тест 1: Замер времени заполнения
            var insertArrayTime = MeasureInsertTime(array);
            var insertContainerTime = MeasureInsertTimeForContainer(container);

            // Тест 2: Замер времени последовательного чтения
            var sequentialArrayTime = MeasureSequentialReadTimeForArray(array);
            var sequentialContainerTime = MeasureSequentialReadTimeForContainer(container);

            // Тест 3: Замер использования памяти
            var memoryArray = MeasureMemoryUsageForArray();
            var memoryContainer = MeasureMemoryUsageForContainer(container);

            dataGridView2.Rows.Add(
                "Array",
                $"{insertArrayTime.TotalMilliseconds:F2} мс",
                $"{sequentialArrayTime.TotalMilliseconds:F2} мс",
                $"{memoryArray:F2} МБ"
            );

            dataGridView2.Rows.Add(
                "StationContainer",
                $"{insertContainerTime.TotalMilliseconds:F2} мс",
                $"{sequentialContainerTime.TotalMilliseconds:F2} мс",
                $"{memoryContainer:F2} МБ"
            );

            dataGridView2.Refresh();
        }

        #region Методы тестирования для Array

        private TimeSpan MeasureInsertTime(Station[] array)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = CreateRandomStation(i);
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        private TimeSpan MeasureSequentialReadTimeForArray(Station[] array)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int totalSeats = 0;
            foreach (var station in array)
            {
                totalSeats += station.NumberOfSeats ?? 0;
            }

            stopwatch.Stop();
            Debug.WriteLine($"Array sequential read - total seats: {totalSeats}");
            return stopwatch.Elapsed;
        }

        private double MeasureMemoryUsageForArray()
        {
            long memoryBefore = GC.GetTotalMemory(true);

            Station[] testArray = new Station[100000];
            for (int i = 0; i < 100000; i++)
            {
                testArray[i] = CreateRandomStation(i);
            }

            long memoryAfter = GC.GetTotalMemory(true);

            // Очищаем
            testArray = null;
            GC.Collect();

            return (memoryAfter - memoryBefore) / (1024.0 * 1024.0); // В МБ
        }

        #endregion

        #region Методы тестирования для StationContainer

        private TimeSpan MeasureInsertTimeForContainer(StationContainer container)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 100000; i++)
            {
                container.AddStation(CreateRandomStation(i));
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        private TimeSpan MeasureSequentialReadTimeForContainer(StationContainer container)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int totalSeats = 0;
            foreach (var station in container.GetAllStations())
            {
                totalSeats += station.NumberOfSeats ?? 0;
            }

            stopwatch.Stop();
            Debug.WriteLine($"Container sequential read - total seats: {totalSeats}");
            return stopwatch.Elapsed;
        }

        private double MeasureMemoryUsageForContainer(StationContainer container)
        {
            long memoryBefore = GC.GetTotalMemory(true);

            StationContainer testContainer = container;

            if (testContainer.CountOfStation() == 0)
            {
                for (int i = 0; i < 100000; i++)
                {
                    testContainer.AddStation(CreateRandomStation(i));
                }
            }

            long memoryAfter = GC.GetTotalMemory(true);

            testContainer = null;
            GC.Collect();

            return (memoryAfter - memoryBefore) / (1024.0 * 1024.0);
        }

        #endregion

        #endregion

        #region Создание тестовых данных

        private Station CreateRandomStation(int index)
        {
            string[] stationNames = {
                "Москва", "Санкт-Петербург", "Новосибирск", "Екатеринбург", "Казань",
                "Нижний Новгород", "Челябинск", "Самара", "Омск", "Ростов-на-Дону"
            };

            string[] streetTypes = { "ул.", "пр." };
            string[] streetNames = { "Ленина", "Победы", "Советская", "Мира", "Гагарина", "Кирова", "Лесная", "Центральная" };

            return new Station(
                $"{stationNames[_random.Next(stationNames.Length)]}-{index}",
                _random.Next(50, 500),
                _random.Next(0, 100),
                $"+7{_random.Next(900, 999)}{_random.Next(1000000, 9999999):D7}",
                _random.NextDouble() * 100,
                DateTime.Now.AddDays(-_random.Next(0, 3650)),
                $"{streetTypes[_random.Next(streetTypes.Length)]} {streetNames[_random.Next(streetNames.Length)]}, {_random.Next(1, 100)}"
            );
        }

        #endregion

        #region Обработчики событий формы

        private void FindParamsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ParamTextBox != null)
                ParamTextBox.Clear();
            UpdateDataGridView();
        }

        private void ContainerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_stationFacade != null)
            {
                _stationFacade.CollectionChanged -= OnStationCollectionChanged;
            }
        }

        private void ContainerForm_Load(object sender, EventArgs e)
        {
            // Теперь подписываемся на события, когда форма уже загружена
            _stationFacade.CollectionChanged += OnStationCollectionChanged;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Пустая реализация
        }

        #endregion
    }
}