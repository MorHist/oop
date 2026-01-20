using System;
using Lr1.Models;

namespace Lr1.Factories
{
    /// <summary>
    /// Конкретная фабрика для создания Вокзалов
    /// </summary>
    public class RailwayStationFactory : StationFactory
    {
        public override StationBase CreateStation(string title)
        {
            Random rnd = new Random();
            return new RailwayStation(
                title: title,
                numberOfSeats: rnd.Next(50, 500),
                soldTickets: rnd.Next(100, 10000),
                averageAttendance: rnd.NextDouble() * 100,
                hasLeftLuggageOffice: rnd.Next(0, 2) == 1,
                ticketOfficesCount: rnd.Next(1, 5)
            );
        }

        public override string GetFactoryType() => "Фабрика Вокзалов";
    }
}