using System;
using System.Text.RegularExpressions;
using Lr1.Exceptions;

namespace Lr1.Models
{
    /// <summary>
    /// Вокзал - станция с сидячими местами и обслуживанием пассажиров
    /// (Замена оригинального класса Station)
    /// </summary>
    public class RailwayStation : StationBase
    {
        private int? _numberOfSeats;
        private int? _soldTickets;
        private double? _averageAttendance;

        /// <summary>
        /// Количество сидячих мест
        /// </summary>
        public int? NumberOfSeats
        {
            get => _numberOfSeats;
            set
            {
                if (value < 0)
                    throw new NegativeValueException("Число сидений");
                _numberOfSeats = value;
            }
        }

        /// <summary>
        /// Количество проданных билетов
        /// </summary>
        public int? SoldTickets
        {
            get => _soldTickets;
            set
            {
                if (value < 0)
                    throw new NegativeValueException("Количество проданных билетов");
                _soldTickets = value;
            }
        }

        /// <summary>
        /// Средняя посещаемость
        /// </summary>
        public double? AverageAttendance
        {
            get => _averageAttendance;
            set
            {
                if (value < 0)
                    throw new NegativeValueException("Средняя посещаемость");
                _averageAttendance = value;
            }
        }

        /// <summary>
        /// Есть ли камера хранения
        /// </summary>
        public bool HasLeftLuggageOffice { get; set; }

        /// <summary>
        /// Количество касс
        /// </summary>
        public int TicketOfficesCount { get; set; }

        public override string StationType => "Вокзал";

        public RailwayStation() : base() { }

        public RailwayStation(string title) : base(title) { }

        public RailwayStation(string title, int numberOfSeats) : base(title)
        {
            NumberOfSeats = numberOfSeats;
        }

        public RailwayStation(string title, int numberOfSeats, int soldTickets,
            double averageAttendance, bool hasLeftLuggageOffice, int ticketOfficesCount)
            : this(title, numberOfSeats)
        {
            SoldTickets = soldTickets;
            AverageAttendance = averageAttendance;
            HasLeftLuggageOffice = hasLeftLuggageOffice;
            TicketOfficesCount = ticketOfficesCount;
        }

        /// <summary>
        /// Конвертирует количество мест в HEX (из оригинального Station)
        /// </summary>
        public string NumberOfSeatsToHex()
        {
            if (NumberOfSeats == null)
                return string.Empty;
            return Convert.ToString((int)NumberOfSeats, 16);
        }

        public override string GetSpecificInfo()
        {
            return $"Сидячих мест: {NumberOfSeats ?? 0}\n" +
                   $"Продано билетов: {SoldTickets ?? 0}\n" +
                   $"Средняя посещаемость: {AverageAttendance ?? 0:F2}\n" +
                   $"Камеры хранения: {(HasLeftLuggageOffice ? "Есть" : "Нет")}\n" +
                   $"Количество касс: {TicketOfficesCount}";
        }

        /// <summary>
        /// Переопределение GetFieldValue для специфичных полей
        /// </summary>
        public new object GetFieldValue(string fieldName)
        {
            return fieldName.ToLower() switch
            {
                "numberofseats" => NumberOfSeats,
                "soldtickets" => SoldTickets,
                "averageattendance" => AverageAttendance,
                "hasleftluggageoffice" => HasLeftLuggageOffice,
                "ticketofficescount" => TicketOfficesCount,
                _ => base.GetFieldValue(fieldName)
            };
        }
    }
}