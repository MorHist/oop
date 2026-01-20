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

        #region Работа с основным контейнером

        private void UpdateDataGridView()
        {
            if (dataGridView1 == null || dataGridView1.IsDisposed)
                return;

            dataGridView1.Rows.Clear();

            // Отображаем элементы стека в порядке LIFO (последний добавленный - первый)
            foreach (var station in _stationContainer.GetAllStations())
            {
                
            }

            if (InfoLabel != null && !InfoLabel.IsDisposed)
            {
                InfoLabel.Text = $"Всего станций: {_stationContainer.CountOfStation()}";
            }
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            
        }

        private void DisplayFoundStations(Stack<RailwayStation> stations)
        {
            
        }

        #endregion

        #region Вспомогательные методы для поиска

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

            

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        private TimeSpan MeasureSequentialReadTime(StationContainer container)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            int totalSeats = 0;
           
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

            

            long memoryAfter = GC.GetTotalMemory(true);

            // Очищаем
            testContainer = null;
            GC.Collect();

            return (memoryAfter - memoryBefore) / (1024.0 * 1024.0); // В МБ
        }

        #endregion

        #endregion

        #region Создание тестовых данных

        

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
 
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #endregion
    }
}