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
        private StationContainer _testContainer; // Для тестирования производительности

        // Конструктор, принимающий StationContainer
        public ContainerForm(StationContainer stationContainer)
        {
            InitializeComponent();

            _stationContainer = stationContainer;

            // Подписка на события
            _stationContainer.StationAdded += OnStationAdded;
            _stationContainer.StationRemoved += OnStationRemoved;

            // Инициализация интерфейса
            InitializeDataGridView();
            InitializeCompareDataGridView();
            UpdateDataGridView();
            PopulateComboBox();
        }

        #region Инициализация UI

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

            // Добавление колонок для сравнения
            dataGridView2.Columns.Add("TypeColumn", "Тип контейнера");
            dataGridView2.Columns["TypeColumn"].Width = 180;

            dataGridView2.Columns.Add("InsertTimeColumn", "Время заполнения (100000 элементов), мс");
            dataGridView2.Columns["InsertTimeColumn"].Width = 220;

            dataGridView2.Columns.Add("SequentialReadTimeColumn", "Время последовательного чтения, мс");
            dataGridView2.Columns["SequentialReadTimeColumn"].Width = 200;

            dataGridView2.Columns.Add("MemoryUsageColumn", "Использование памяти, МБ");
            dataGridView2.Columns["MemoryUsageColumn"].Width = 160;

            dataGridView2.Columns.Add("AddElementTimeColumn", "Время добавления 1 элемента, мкс");
            dataGridView2.Columns["AddElementTimeColumn"].Width = 180;

            dataGridView2.Columns.Add("RemoveElementTimeColumn", "Время удаления 1 элемента, мкс");
            dataGridView2.Columns["RemoveElementTimeColumn"].Width = 180;

            dataGridView2.Columns.Add("PeekTimeColumn", "Время доступа к вершине, мкс");
            dataGridView2.Columns["PeekTimeColumn"].Width = 160;
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

        private void FindButton_Click(object sender, EventArgs e)
        {
            string param = FindParamsComboBox.SelectedItem?.ToString();
            string searchValue = ParamTextBox.Text.ToLower();

            if (string.IsNullOrEmpty(param) || string.IsNullOrEmpty(searchValue))
            {
                UpdateDataGridView();
                return;
            }

            // Создаем временный стек для поиска без преобразования в List
            var stationsStack = _stationContainer.GetAllStations();
            var tempStack = new Stack<Station>(stationsStack); // Копия для безопасного перебора
            var foundStations = new Stack<Station>(); // Стек для найденных станций

            // Перебираем элементы стека
            while (tempStack.Count > 0)
            {
                var station = tempStack.Pop();
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
                {
                    foundStations.Push(station); // Сохраняем в порядке стека
                }
            }

            // Отображаем найденные станции
            DisplayFoundStations(foundStations);
        }

        private void DisplayFoundStations(Stack<Station> stations)
        {
            if (dataGridView1 == null || dataGridView1.IsDisposed)
                return;

            dataGridView1.Rows.Clear();

            // Отображаем найденные станции в порядке стека
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
            // Проверяем, что dataGridView2 существует
            if (dataGridView2 == null)
            {
                MessageBox.Show("Таблица сравнения не инициализирована!", "Ошибка");
                return;
            }

            // Очищаем таблицу сравнения
            dataGridView2.Rows.Clear();

            // Освобождаем предыдущий тестовый контейнер
            _testContainer = null;

            // Принудительный сбор мусора для чистоты измерений
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // СОЗДАЕМ НОВЫЙ КОНТЕЙНЕР ДЛЯ ТЕСТА
            _testContainer = new StationContainer();
            _testContainer.EnableEvents = false; // Отключаем события для чистоты сравнения

            // СОЗДАЕМ МАССИВ (Array) для сравнения
            Station[] array = new Station[100000];

            // Выполняем все тесты
            PerformAllTests(array, _testContainer);

            // Освобождаем тестовый контейнер
            _testContainer = null;
            GC.Collect();
        }

        private void PerformAllTests(Station[] array, StationContainer container)
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

            // Тест 4: Замер времени добавления одного элемента
            var addElementArrayTime = MeasureAddElementTimeArray();
            var addElementContainerTime = MeasureAddElementTimeContainer();

            // Тест 5: Замер времени удаления одного элемента
            var removeElementArrayTime = MeasureRemoveElementTimeArray();
            var removeElementContainerTime = MeasureRemoveElementTimeContainer();

            // Тест 6: Замер времени доступа к вершине (только для контейнера)
            var peekContainerTime = MeasurePeekTimeContainer();

            // Выводим результаты в таблицу
            dataGridView2.Rows.Add(
                "Array",
                $"{insertArrayTime.TotalMilliseconds:F2} мс",
                $"{sequentialArrayTime.TotalMilliseconds:F2} мс",
                $"{memoryArray:F2} МБ",
                $"{(addElementArrayTime.TotalMilliseconds * 1000):F1} мкс",
                $"{(removeElementArrayTime.TotalMilliseconds * 1000):F1} мкс",
                "N/A" // Для массива нет операции Peek
            );

            dataGridView2.Rows.Add(
                "StationContainer",
                $"{insertContainerTime.TotalMilliseconds:F2} мс",
                $"{sequentialContainerTime.TotalMilliseconds:F2} мс",
                $"{memoryContainer:F2} МБ",
                $"{(addElementContainerTime.TotalMilliseconds * 1000):F1} мкс",
                $"{(removeElementContainerTime.TotalMilliseconds * 1000):F1} мкс",
                $"{(peekContainerTime.TotalMilliseconds * 1000):F1} мкс"
            );

            // Обновляем отображение таблицы
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

        private TimeSpan MeasureSequentialReadTime(Station[] array)
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
            // Замеряем память, занимаемую массивом из 100000 элементов
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

        private TimeSpan MeasureAddElementTimeArray()
        {
            // Для массива добавление элемента требует создания нового массива большего размера
            Stopwatch stopwatch = Stopwatch.StartNew();

            Station[] smallArray = new Station[1000];
            for (int i = 0; i < 1000; i++)
            {
                smallArray[i] = CreateRandomStation(i);
            }

            // Добавляем один элемент
            Station[] newArray = new Station[1001];
            Array.Copy(smallArray, newArray, 1000);
            newArray[1000] = CreateRandomStation(1000);

            stopwatch.Stop();
            return TimeSpan.FromTicks(stopwatch.Elapsed.Ticks / 1000); // Среднее время
        }

        private TimeSpan MeasureRemoveElementTimeArray()
        {
            // Для массива удаление элемента требует создания нового массива меньшего размера
            Stopwatch stopwatch = Stopwatch.StartNew();

            Station[] smallArray = new Station[1000];
            for (int i = 0; i < 1000; i++)
            {
                smallArray[i] = CreateRandomStation(i);
            }

            // Удаляем последний элемент
            Station[] newArray = new Station[999];
            Array.Copy(smallArray, newArray, 999);

            stopwatch.Stop();
            return TimeSpan.FromTicks(stopwatch.Elapsed.Ticks / 1000); // Среднее время
        }

        #endregion

        #region Методы тестирования для StationContainer

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

        private TimeSpan MeasureSequentialReadTime(StationContainer container)
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

        private double MeasureMemoryUsageContainer()
        {
            // Замеряем память, занимаемую контейнером с 100000 элементов
            long memoryBefore = GC.GetTotalMemory(true);

            StationContainer testContainer = new StationContainer();
            testContainer.EnableEvents = false;

            for (int i = 0; i < 100000; i++)
            {
                testContainer.AddStation(CreateRandomStation(i));
            }

            long memoryAfter = GC.GetTotalMemory(true);

            // Очищаем
            testContainer = null;
            GC.Collect();

            return (memoryAfter - memoryBefore) / (1024.0 * 1024.0); // В МБ
        }

        private TimeSpan MeasureAddElementTimeContainer()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            StationContainer testContainer = new StationContainer();
            testContainer.EnableEvents = false;

            // Добавляем 10000 элементов и замеряем общее время
            for (int i = 0; i < 10000; i++)
            {
                testContainer.AddStation(CreateRandomStation(i));
            }

            stopwatch.Stop();
            return TimeSpan.FromTicks(stopwatch.Elapsed.Ticks / 10000); // Среднее время на один элемент
        }

        private TimeSpan MeasureRemoveElementTimeContainer()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            StationContainer testContainer = new StationContainer();
            testContainer.EnableEvents = false;

            // Добавляем 10000 элементов
            for (int i = 0; i < 10000; i++)
            {
                testContainer.AddStation(CreateRandomStation(i));
            }

            // Удаляем все элементы по одному
            while (testContainer.AnyStations())
            {
                testContainer.RemoveStation(testContainer.Peek());
            }

            stopwatch.Stop();
            return TimeSpan.FromTicks(stopwatch.Elapsed.Ticks / 10000); // Среднее время на один элемент
        }

        private TimeSpan MeasurePeekTimeContainer()
        {
            StationContainer testContainer = new StationContainer();
            testContainer.EnableEvents = false;

            // Добавляем один элемент для теста
            testContainer.AddStation(CreateRandomStation(0));

            Stopwatch stopwatch = Stopwatch.StartNew();

            // Выполняем операцию Peek много раз
            for (int i = 0; i < 100000; i++)
            {
                var station = testContainer.Peek();
            }

            stopwatch.Stop();
            return TimeSpan.FromTicks(stopwatch.Elapsed.Ticks / 100000); // Среднее время
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

            string[] streetTypes = { "ул.", "пр.", "б-р", "ш.", "наб." };
            string[] streetNames = { "Ленина", "Победы", "Советская", "Мира", "Гагарина", "Кирова", "Лесная", "Центральная" };

            return new Station(
                $"{stationNames[_random.Next(stationNames.Length)]}-{index}",
                _random.Next(50, 500), // Количество мест
                _random.Next(0, 100), // Проданные билеты
                $"+7{_random.Next(900, 999)}{_random.Next(1000000, 9999999):D7}", // Телефон
                _random.NextDouble() * 100, // Средняя посещаемость
                DateTime.Now.AddDays(-_random.Next(0, 3650)), // Дата открытия
                $"{streetTypes[_random.Next(streetTypes.Length)]} {streetNames[_random.Next(streetNames.Length)]}, {_random.Next(1, 100)}" // Адрес
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
            // Отписываемся от событий
            if (_stationContainer != null)
            {
                _stationContainer.StationAdded -= OnStationAdded;
                _stationContainer.StationRemoved -= OnStationRemoved;
            }

            // Освобождаем тестовый контейнер
            _testContainer = null;
        }

        private void ContainerForm_Load(object sender, EventArgs e)
        {
            // Дополнительная инициализация, если нужна
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Пустая реализация, если не нужна
        }

        #endregion
    }
}
