using System;
using Lr1.Models;

namespace Lr1.Factories
{
    /// <summary>
    /// Конкретная фабрика для создания Узлов
    /// </summary>
    public class HubFactory : StationFactory
    {
        public override StationBase CreateStation(string title)
        {
            Random rnd = new Random();
            return new Hub(
                title: title,
                cranesCount: rnd.Next(1, 10),
                cargoThroughput: rnd.Next(50, 500) + rnd.NextDouble(),
                cargoPlatformsCount: rnd.Next(2, 8),
                hasWeightControl: rnd.Next(0, 2) == 1
            );
        }

        public override string GetFactoryType() => "Фабрика Узлов";
    }
}