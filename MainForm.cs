using System.Text;
using System.Drawing;
using Lr1.Models;
using Lr1.Factories;
using Lr1.Exceptions;
using Lr1.Service;

namespace Lr1
{
    public partial class MainForm : Form
    {
        public StationContainer _stations = new StationContainer();
        private StationFactory _currentFactory;
        private Dictionary<string, StationFactory> _factories;
        private string _currentStationType = "Вокзал";

        public MainForm()
        {
            InitializeComponent();
            InitializeFactories();
            _currentFactory = _factories["Вокзал"];
            CreateInitialStations();

        }

        private void InitializeFactories()
        {
            _factories = new Dictionary<string, StationFactory>
            {
                ["Полустанок"] = new HalteFactory(),
                ["Вокзал"] = new RailwayStationFactory(),
                ["Узел"] = new HubFactory()
            };

            stationTypeComboBox.Items.Clear();
            stationTypeComboBox.Items.AddRange(_factories.Keys.ToArray());
            stationTypeComboBox.SelectedIndex = 1;
        }

        private void CreateInitialStations()
        {
            while (_stations.AnyStations())
            {
                _stations.RemoveStation();
            }

            var halteFactory = new HalteFactory();
            var stationFactory = new RailwayStationFactory();
            var hubFactory = new HubFactory();

            _stations.AddStation(halteFactory.CreateStation("Полустанок 'Лесная'"));
            _stations.AddStation(stationFactory.CreateStation("Вокзал 'Пенза-1'"));
            _stations.AddStation(hubFactory.CreateStation("Узел 'Промышленный'"));

            SetStationInfo();
        }



        private void SetStationInfo()
        {
            StationBase station = _stations.Peek();
            if (station == null || !_stations.AnyStations())
            {
                ClearAllFields();
                UpdateStationBtn.Enabled = false;
                SetInfo();
                return;
            }


            // Тип станции
            _currentStationType = station.StationType;
            stationTypeComboBox.SelectedItem = _currentStationType;

            // Формируем информацию для отображения
            SetInfo();
        }

        private void ClearAllFields()
        {
            Info.Text = "";
        }

        private void SetInfo()
        {
            var station = _stations.Peek();
            if (station != null)
            {
                StringBuilder info = new StringBuilder();

                // Основная информация
                info.AppendLine($"Всего станций: {StationBase.TotalStations}");
                info.AppendLine($"Текущая станция: {station.StationType} \"{station.Title}\"");
                info.AppendLine($"Номер: {station.Number}");
                info.AppendLine($"Дата открытия: {station.DateOfOpening:dd.MM.yyyy}");
                info.AppendLine($"Адрес: {station.Address}");
                info.AppendLine("");

                // Информация в зависимости от типа станции
                if (station is RailwayStation railwayStation)
                {
                    ShowRailwayStationInfo(info, railwayStation);
                }
                else if (station is Halte halte)
                {
                    ShowHalteInfo(info, halte);
                }
                else if (station is Hub hub)
                {
                    ShowHubInfo(info, hub);
                }

                Info.Text = info.ToString();
            }
            else
            {
                Info.Text = "Всего станций: 0\nНет данных для отображения";
            }
        }

        /// <summary>
        /// Формирует информацию о Вокзале
        /// </summary>
        private void ShowRailwayStationInfo(StringBuilder info, RailwayStation station)
        {
            info.AppendLine("--- Параметры вокзала ---");
            info.AppendLine($"Количество мест: {station.NumberOfSeats?.ToString() ?? "-"}");
            info.AppendLine($"Продано билетов: {station.SoldTickets?.ToString() ?? "-"}");
            info.AppendLine($"Средняя посещаемость: {station.AverageAttendance?.ToString("F2") ?? "-"}");
            info.AppendLine($"Камеры хранения: {(station.HasLeftLuggageOffice ? "Есть" : "Нет")}");
            info.AppendLine($"Количество касс: {station.TicketOfficesCount}");
            info.AppendLine($"Места в HEX: {station.NumberOfSeatsToHex()}");
        }

        /// <summary>
        /// Формирует информацию о Полустанке
        /// </summary>
        private void ShowHalteInfo(StringBuilder info, Halte station)
        {
            info.AppendLine("--- Параметры полустанка ---");
            info.AppendLine($"Количество платформ: {station.PlatformsCount}");
            info.AppendLine($"Навес от дождя: {(station.HasRoof ? "Есть" : "Нет")}");
            info.AppendLine($"Сидячих мест: Нет");
        }

        /// <summary>
        /// Формирует информацию о Узле
        /// </summary>
        private void ShowHubInfo(StringBuilder info, Hub station)
        {
            info.AppendLine("--- Параметры узла ---");
            info.AppendLine($"Количество кранов: {station.CranesCount}");
            info.AppendLine($"Грузопередача (т/час): {station.CargoThroughput:F2}");
            info.AppendLine($"Грузовые платформы: {station.CargoPlatformsCount}");
            info.AppendLine($"Весовой контроль: {(station.HasWeightControl ? "Есть" : "Нет")}");
            info.AppendLine($"Общая грузоподъемность: {station.CalculateTotalLiftingCapacity():F2} т");
        }

        private void AddNewStationBtn_Click(object sender, EventArgs e)
        {
            if (stationTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип станции", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string stationType = stationTypeComboBox.SelectedItem.ToString();

            using (var addForm = new AddStationForm(stationType))
            {
                if (addForm.ShowDialog() == DialogResult.OK && addForm.CreatedStation != null)
                {
                        try
                        {
                            _stations.AddStation(addForm.CreatedStation);
                            SetStationInfo();

                            MessageBox.Show($"Создана новая станция: {addForm.CreatedStation.Title} ({addForm.CreatedStation.StationType})",
                                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка добавления станции: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                }
            }
        }

        private void UpdateStationBtn_Click(object sender, EventArgs e)
        {
            StationBase station = _stations.Peek();
            if (station == null) return;

            using (var editForm = new AddStationForm(station.StationType))
            {
                editForm.SetStationData(station);

                if (editForm.ShowDialog() == DialogResult.OK && editForm.CreatedStation != null)
                {
                    try
                    {
                        _stations.RemoveStation();   
                        _stations.AddStation(editForm.CreatedStation);
                        SetStationInfo();

                        MessageBox.Show("Станция успешно обновлена", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка обновления станции: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                  
                }
            }
            station.Address = Address.Text;
            TicketsInHex.Text = station.NumberOfSeatsToHex();
            SetInfo();
            Info.Text += "Количество мест: " + station.NumberOfSeats;
        }
        /// <summary>
        /// Обновляет свойства существующего объекта StationBase на основе нового объекта
        /// </summary>
        // Добавляем новый метод
        private void MainForm_Load_1(object sender, EventArgs e)
        {
            MessageBox.Show("Петряев и Маляев 23ВП1\nВариант 3", "Лабораторная работа №3 - Порождающие паттерны");
            SetStationInfo();
        }

        private void ErrorButton_Click(object sender, EventArgs e)
        {
            // Используем только оригинальный MyException для деления на ноль
            MyExeption exp = new MyExeption();
            try
            {
                exp.createExeption();
            }
            catch (MyDivideByZeroException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка деления на ноль",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }   

        private void OpenEditFormButton_Click(object sender, EventArgs e)
        {
            ContainerForm containerForm = new ContainerForm(_stations);
            containerForm.Show();
        }

        private void stationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stationTypeComboBox.SelectedItem != null)
            {
                _currentStationType = stationTypeComboBox.SelectedItem.ToString();
                if (_factories.ContainsKey(_currentStationType))
                {
                    _currentFactory = _factories[_currentStationType];
                }
            }
        }
    }
}