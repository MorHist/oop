using static System.Collections.Specialized.BitVector32;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using static Lr1.Station;
using static Lr1.StationContainer;
using System.Drawing.Text;

namespace Lr1
{
    
    public partial class MainForm : Form
    {
        /// <summary>
        /// Список станций
        /// </summary>
        /// 


        public StationContainer _stations = new StationContainer();
        public MainForm()
        {
            InitializeComponent();
            _stations.AddStation(new Station("Пенза-1", 120, 3020, "+79875634543", 78.6, DateTime.Now, "Володарского 12"));
            _stations.AddStation(new Station("Пенза-2", 10, 3020, "+79888888883", 234.9, DateTime.Now, "Володарского 13"));
            _stations.AddStation(new Station("Пенза-3", 12370, 3020, "+71234567890", 13.2, DateTime.Now, "Володарского 14"));

        }

        /// <summary>
        /// Метод, вызываемый при загрузке формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Метод проверяет текстбоксы на пустоту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            Info.Text = $"Всего станций: {Station.TotalStations}\n{_stations.Peek()}";
        }

        /// <summary>
        /// Метод устанавливает в текстбоксы поля выбранного вокзала
        /// </summary>
        private void SetStationInfo()
        {

            Station station = _stations.Peek();
            if (!_stations.AnyStations())
            {
                // Если стек пуст, очищаем текстовые поля и деактивируем кнопки
                Title.Text = station.Title; ;
                NumberOfSeats.Text = station.NumberOfSeats.ToString();
                SoldTickets.Text = station.SoldTickets.ToString();
                Number.Text = station.Number;
                AverageAttendace.Text = station.AverageAttendace.ToString();
                DateOfOpening.Value = station.DateOfOpening; // Устанавливаем текущую дату по умолчанию
                Address.Text = station.Address;

                // Деактивируем кнопки, так как нет данных для отображения
                UpdateStationBtn.Enabled = false;
                AddNewStationBtn.Enabled = false;

                // Обновляем информацию о количестве станций
                Info.Text = "Всего станций: 0\nНет данных для отображения";
                return;
            }

            Title.Text = station.Title;
            NumberOfSeats.Text = station.NumberOfSeats.ToString();
            SoldTickets.Text = station.SoldTickets.ToString();
            Number.Text = station.Number;
            AverageAttendace.Text = station.AverageAttendace.ToString();
            DateOfOpening.Value = station.DateOfOpening;
            Address.Text = station.Address;
            SetInfo();
            Info.Text += "Количество мест: " + station.NumberOfSeats;
        }

        private void Stations_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStationInfo();
        }

        /// <summary>
        /// Метод вызызывается при нажатии на кнопку "Сохранить"
        /// Обновляет поля выбранного вокзала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateStationBtn_Click(object sender, EventArgs e)
        {

            Station station = _stations.Peek();
            try
            {
                station.Title = Title.Text;
                station.NumberOfSeats = Convert.ToInt32(NumberOfSeats.Text);
                station.SoldTickets = Convert.ToInt32(SoldTickets.Text);
                station.AverageAttendace = Convert.ToDouble(AverageAttendace.Text.Replace('.', ','));
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильные числовые данные", "Ошибка");
            }
            catch (NegativeValueException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            try
            {
                station.Number = Number.Text;
            }
            catch (WrongNumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            try
            {
                station.DateOfOpening = DateOfOpening.Value;
            }
            catch (InvalidDateOfOpeningException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            station.Address = Address.Text;
            TicketsInHex.Text = station.NumberOfSeatsToHex();
            _stations.AddStation(station);
            SetInfo();
            Info.Text += "Количество мест: " + station.NumberOfSeats;
        }

        /// <summary>
        /// Метод вызызывается при нажатии на кнопку "Добавить новый"
        /// Создаёт новый вокзал и добавляет его в список вокзалов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewStationBtn_Click(object sender, EventArgs e)
        {
            

            Station station = new Station("Новый вокзал");

            try
            {
                station.Title = Title.Text;
                station.NumberOfSeats = Convert.ToInt32(NumberOfSeats.Text);
                station.SoldTickets = Convert.ToInt32(SoldTickets.Text);
                station.AverageAttendace = Convert.ToDouble(AverageAttendace.Text.Replace('.', ','));
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильные числовые данные", "Ошибка");
                return;
            }
            catch (NegativeValueException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return;
            }
            try
            {
                station.Number = Number.Text;
            }
            catch (WrongNumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return;
            }
            try
            {
                station.DateOfOpening = DateOfOpening.Value;
            }
            catch (InvalidDateOfOpeningException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return;
            }
            station.Address = Address.Text;
            TicketsInHex.Text = station.NumberOfSeatsToHex();
            _stations.AddStation(station);
            SetInfo();
            Info.Text += "Количество мест: " + station.NumberOfSeats;
        }



        private void MainForm_Load_1(object sender, EventArgs e)
        {
            DateOfOpening.Format = DateTimePickerFormat.Custom;
            DateOfOpening.CustomFormat = "dd MMM yyyy";
            MessageBox.Show("Петряев и Маляев 23ВП1\nВариант 3", "Лабораторная работа №1");
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
            ContainerForm containerForm = new ContainerForm(_stations);
            containerForm.Show();
        }
    }
}
