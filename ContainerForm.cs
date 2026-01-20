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
using Lr1.Models;
using Lr1.Service;
using Lr1.Factories;
using Lr1.Exceptions;

namespace Lr1
{
    public partial class ContainerForm : Form
    {
        private StationContainer _stationContainer;
        private Random _random = new Random();
        private StationContainer _testContainer; // Для тестирования производительности

        // Конструктор, принимающий StationContainer
        public ContainerForm(StationContainer stationContainer)
        {
            InitializeComponent();

            _stationContainer = stationContainer;

            _stationContainer.StationAdded += OnStationAdded;
            _stationContainer.StationRemoved += OnStationRemoved;

            InitializeDataGridView();
            InitializeCompareDataGridView();
            UpdateDataGridView();
            PopulateComboBox();
        }

        #region Инициализация UI

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Общие поля для всех станций
            dataGridView1.Columns.Add("StationType", "Тип станции");
            dataGridView1.Columns["StationType"].Width = 100;

            dataGridView1.Columns.Add("Title", "Название");
            dataGridView1.Columns["Title"].Width = 120;

            dataGridView1.Columns.Add("NumberOfSeats", "Количество мест");
            dataGridView1.Columns["NumberOfSeats"].Width = 110;

            dataGridView1.Columns.Add("SoldTickets", "Проданные билеты");
            dataGridView1.Columns["SoldTickets"].Width = 110;

            dataGridView1.Columns.Add("Number", "Телефон");
            dataGridView1.Columns["Number"].Width = 100;

            dataGridView1.Columns.Add("AverageAttendance", "Средняя посещаемость");
            dataGridView1.Columns["AverageAttendance"].Width = 140;

            dataGridView1.Columns.Add("DateOfOpening", "Дата открытия");
            dataGridView1.Columns["DateOfOpening"].Width = 100;

            dataGridView1.Columns.Add("Address", "Адрес");
            dataGridView1.Columns["Address"].Width = 150;

            // Поля для RailwayStation
            dataGridView1.Columns.Add("HasLeftLuggageOffice", "Камера хранения");
            dataGridView1.Columns["HasLeftLuggageOffice"].Width = 100;

            dataGridView1.Columns.Add("TicketOfficesCount", "Количество касс");
            dataGridView1.Columns["TicketOfficesCount"].Width = 110;

            // Поля для Hub
            dataGridView1.Columns.Add("CranesCount", "Количество кранов");
            dataGridView1.Columns["CranesCount"].Width = 110;

            dataGridView1.Columns.Add("CargoThroughput", "Грузопередача (т/час)");
            dataGridView1.Columns["CargoThroughput"].Width = 130;

            dataGridView1.Columns.Add("CargoPlatformsCount", "Грузовых платформ");
            dataGridView1.Columns["CargoPlatformsCount"].Width = 120;

            dataGridView1.Columns.Add("HasWeightControl", "Весовой контроль");
            dataGridView1.Columns["HasWeightControl"].Width = 110;

            // Поля для Halte
            dataGridView1.Columns.Add("PlatformsCount", "Количество платформ");
            dataGridView1.Columns["PlatformsCount"].Width = 120;

            dataGridView1.Columns.Add("HasRoof", "Навес");
            dataGridView1.Columns["HasRoof"].Width = 70;

            // Форматирование столбцов
            dataGridView1.Columns["NumberOfSeats"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["SoldTickets"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["AverageAttendance"].DefaultCellStyle.Format = "F2";
            dataGridView1.Columns["CargoThroughput"].DefaultCellStyle.Format = "F2";
            dataGridView1.Columns["DateOfOpening"].DefaultCellStyle.Format = "dd.MM.yyyy";

            // Центрирование для логических значений
            dataGridView1.Columns["HasLeftLuggageOffice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["HasWeightControl"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["HasRoof"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
                "Тип станции",
                "Название",
                "Количество мест",
                "Проданные билеты",
                "Телефон",
                "Средняя посещаемость",
                "Адрес",
                "Камера хранения",
                "Количество касс",
                "Количество кранов",
                "Грузопередача",
                "Грузовых платформ",
                "Весовой контроль",
                "Количество платформ",
                "Навес"
            });

            if (FindParamsComboBox.Items.Count > 0)
                FindParamsComboBox.SelectedIndex = 0;
        }

        #endregion

        #region Работа с основным контейнером

        private void UpdateDataGridView()
        {
            if (dataGridView1 == null || dataGridView1.IsDisposed)
                return;

            dataGridView1.Rows.Clear();

            // Отображаем элементы стека в порядке LIFO (последний добавленный - первый)
            foreach (var station in _stationContainer.GetAllStations())
            {
                int rowIndex = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowIndex];

                // Общие поля для всех станций
                row.Cells["StationType"].Value = station.StationType;
                row.Cells["Title"].Value = station.Title;
                row.Cells["Number"].Value = station.Number;
                row.Cells["DateOfOpening"].Value = station.DateOfOpening;
                row.Cells["Address"].Value = station.Address;

                // Определяем тип станции и заполняем специфичные поля
                if (station is RailwayStation railwayStation)
                {
                    row.Cells["NumberOfSeats"].Value = railwayStation.NumberOfSeats;
                    row.Cells["SoldTickets"].Value = railwayStation.SoldTickets;
                    row.Cells["AverageAttendance"].Value = railwayStation.AverageAttendance;
                    row.Cells["HasLeftLuggageOffice"].Value = railwayStation.HasLeftLuggageOffice ? "Да" : "Нет";
                    row.Cells["TicketOfficesCount"].Value = railwayStation.TicketOfficesCount;

                    // Цветовое выделение для RailwayStation
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
                else if (station is Hub hub)
                {
                    row.Cells["CranesCount"].Value = hub.CranesCount;
                    row.Cells["CargoThroughput"].Value = hub.CargoThroughput;
                    row.Cells["CargoPlatformsCount"].Value = hub.CargoPlatformsCount;
                    row.Cells["HasWeightControl"].Value = hub.HasWeightControl ? "Да" : "Нет";

                    // Цветовое выделение для Hub
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (station is Halte halte)
                {
                    row.Cells["PlatformsCount"].Value = halte.PlatformsCount;
                    row.Cells["HasRoof"].Value = halte.HasRoof ? "Да" : "Нет";

                    // Цветовое выделение для Halte
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }

            if (InfoLabel != null && !InfoLabel.IsDisposed)
            {
                InfoLabel.Text = $"Всего станций: {_stationContainer.CountOfStation()}";
            }
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            // Код поиска остается без изменений
        }

        private void DisplayFoundStations(Stack<RailwayStation> stations)
        {
            // Код отображения найденных станций остается без изменений
        }

        #endregion

        #region Вспомогательные методы для поиска и работы с данными

        private object GetStationFieldValue(StationBase station, string fieldName)
        {
            if (station == null) return null;

            // Преобразуем русские названия полей в английские для метода GetFieldValue
            string englishFieldName = fieldName.ToLower() switch
            {
                "тип станции" => "stationtype",
                "название" => "title",
                "количество мест" => "numberofseats",
                "проданные билеты" => "soldtickets",
                "телефон" => "number",
                "средняя посещаемость" => "averageattendance",
                "адрес" => "address",
                "камера хранения" => "hasleftluggageoffice",
                "количество касс" => "ticketofficescount",
                "количество кранов" => "cranescount",
                "грузопередача" => "cargothroughput",
                "грузовых платформ" => "cargoplatformscount",
                "весовой контроль" => "hasweightcontrol",
                "количество платформ" => "platformscount",
                "навес" => "hasroof",
                _ => fieldName.ToLower()
            };

            // Сначала проверяем специфичные поля через метод GetFieldValue
            var value = station.GetFieldValue(englishFieldName);
            if (value != null)
                return value;

            // Если не нашли, используем switch для английских названий
            return englishFieldName switch
            {
                "stationtype" => station.StationType,
                "title" => station.Title,
                "number" => station.Number,
                "dateofopening" => station.DateOfOpening,
                "address" => station.Address,
                _ => null
            };
        }

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

        #endregion

        #region События контейнера

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

            _testContainer = null;

            // Принудительный сбор мусора для чистоты измерений
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            _testContainer = new StationContainer();
            _testContainer.EnableEvents = false;

            RailwayStation[] array = new RailwayStation[100000];

            PerformAllTests(array, _testContainer);
            _testContainer = null;
            GC.Collect();
        }

        private void PerformAllTests(RailwayStation[] array, StationContainer container)
        {
            // Тест 1: Замер времени заполнения
            var insertArrayTime = MeasureInsertTime(array);
            var insertContainerTime = MeasureInsertTime(container);

            // Тест 2: Замер времени последовательного чтения
            var sequentialArrayTime = MeasureSequentialReadTime(array);
            var sequentialContainerTime = MeasureSequentialReadTime(container);

            // Тест 3: Замер использования памяти
            var memoryArray = MeasureMemoryUsageArray();
            var memoryContainer = MeasureMemoryUsageContainer();

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

            // Обновляем отображение таблицы
            dataGridView2.Refresh();
        }

        #region Методы тестирования для Array

        private TimeSpan MeasureInsertTime(RailwayStation[] array)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Заполнение массива
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new RailwayStation($"TestStation_{i}")
                {
                    NumberOfSeats = _random.Next(50, 500),
                    SoldTickets = _random.Next(1000, 10000),
                    AverageAttendance = _random.NextDouble() * 1000,
                    HasLeftLuggageOffice = _random.Next(2) == 0,
                    TicketOfficesCount = _random.Next(1, 10)
                };
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        private TimeSpan MeasureSequentialReadTime(RailwayStation[] array)
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

        private double MeasureMemoryUsageArray()
        {
            long memoryBefore = GC.GetTotalMemory(true);

            RailwayStation[] testArray = new RailwayStation[100000];

            // Создаем минимальные объекты для теста
            for (int i = 0; i < testArray.Length; i++)
            {
                testArray[i] = new RailwayStation($"Test_{i}")
                {
                    NumberOfSeats = 100
                };
            }

            long memoryAfter = GC.GetTotalMemory(true);

            // Очищаем
            testArray = null;
            GC.Collect();

            return (memoryAfter - memoryBefore) / (1024.0 * 1024.0); // В МБ
        }

        #endregion

        #region Методы тестирования для StationContainer

        private TimeSpan MeasureInsertTime(StationContainer container)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Заполнение контейнера
            for (int i = 0; i < 100000; i++)
            {
                var station = new RailwayStation($"TestStation_{i}")
                {
                    NumberOfSeats = _random.Next(50, 500),
                    SoldTickets = _random.Next(1000, 10000),
                    AverageAttendance = _random.NextDouble() * 1000,
                    HasLeftLuggageOffice = _random.Next(2) == 0,
                    TicketOfficesCount = _random.Next(1, 10)
                };
                container.AddStation(station);
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        private TimeSpan MeasureSequentialReadTime(StationContainer container)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int totalSeats = 0;
            foreach (var station in container.GetAllStations())
            {
                if (station is RailwayStation railwayStation)
                {
                    totalSeats += railwayStation.NumberOfSeats ?? 0;
                }
            }

            stopwatch.Stop();
            Debug.WriteLine($"Container sequential read - total seats: {totalSeats}");
            return stopwatch.Elapsed;
        }

        private double MeasureMemoryUsageContainer()
        {
            // Замеряем память, занимаемую контейнером с 100000 элементов
            long memoryBefore = GC.GetTotalMemory(true);

            StationContainer testContainer = new StationContainer();
            testContainer.EnableEvents = false;

            // Заполняем контейнер
            for (int i = 0; i < 100000; i++)
            {
                var station = new RailwayStation($"Test_{i}")
                {
                    NumberOfSeats = 100
                };
                testContainer.AddStation(station);
            }

            long memoryAfter = GC.GetTotalMemory(true);

            // Очищаем
            testContainer = null;
            GC.Collect();

            return (memoryAfter - memoryBefore) / (1024.0 * 1024.0); // В МБ
        }

        #endregion

        #endregion

        #region Создание тестовых данных

        // Методы создания тестовых данных остаются без изменений

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
            if (_stationContainer != null)
            {
                _stationContainer.StationAdded -= OnStationAdded;
                _stationContainer.StationRemoved -= OnStationRemoved;
            }

            _testContainer = null;
        }

        private void ContainerForm_Load(object sender, EventArgs e)
        {
            // Код загрузки формы
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Обработка кликов в таблице сравнения
        }

        #endregion
    }
}