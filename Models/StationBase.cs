using System;
using System.Text;
using System.Text.RegularExpressions;
using Lr1.Exceptions;

namespace Lr1.Models
{
    /// <summary>
    /// Абстрактный базовый класс для всех типов станций
    /// </summary>
    public abstract class StationBase
    {
        private string _number;
        private DateTime _dateOfOpening;
        private string _address;
        private string _title;

        /// <summary>
        /// Название станции
        /// </summary>
        public string Title
        {
            get => _title;
            set => _title = value;
        }

        /// <summary>
        /// Номер телефона станции
        /// </summary>
        public string Number
        {
            get => _number;
            set
            {
                if (!Regex.IsMatch(value, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{10}$"))
                    throw new WrongNumberFormatException();
                _number = value;
            }
        }

        /// <summary>
        /// Дата открытия
        /// </summary>
        public DateTime DateOfOpening
        {
            get => _dateOfOpening;
            set
            {
                DateTime currentDate = DateTime.Now;
                if (value.Year < 1830 || value > currentDate)
                    throw new InvalidDateOfOpeningException();
                _dateOfOpening = value;
            }
        }

        /// <summary>
        /// Адрес станции
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Тип станции (абстрактное свойство)
        /// </summary>
        public abstract string StationType { get; }

        /// <summary>
        /// Статический счетчик всех созданных станций
        /// </summary>
        public static int TotalStations { get; protected set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected StationBase()
        {
            TotalStations++;
            DateOfOpening = DateTime.Now;
        }
        /// <summary>
        /// Уменьшение счетчика в случае удаления элемента
        /// </summary>
        public static void DecreaseTotalStations()
        {
            TotalStations--;
        }

        /// <summary>
        /// Конструктор с названием
        /// </summary>
        protected StationBase(string title) : this()
        {
            Title = title;
        }

        /// <summary>
        /// Абстрактный метод для получения специфичной информации
        /// </summary>
        public abstract string GetSpecificInfo();

        /// <summary>
        /// Общая информация о станции
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Тип: {StationType}");
            if (Title != null) sb.AppendLine($"Название: {Title}");
            if (Number != null) sb.AppendLine($"Телефон: {Number}");
            if (DateOfOpening.Year != 1) sb.AppendLine($"Дата открытия: {DateOfOpening:dd.MM.yyyy}");
            if (Address != null) sb.AppendLine($"Адрес: {Address}");
            sb.AppendLine(GetSpecificInfo());
            return sb.ToString();
        }

        /// <summary>
        /// Метод возвращает значение поля по его названию (для совместимости)
        /// </summary>
        public object GetFieldValue(string fieldName)
        {
            return fieldName.ToLower() switch
            {
                "title" => Title,
                "number" => Number,
                "dateofopening" => DateOfOpening,
                "address" => Address,
                _ => null
            };
        }
    }
}