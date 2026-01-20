using System;
using Lr1.Models;

namespace Lr1.Factories
{
    /// <summary>
    /// Конкретная фабрика для создания Полустанков
    /// </summary>
    public class HalteFactory : StationFactory
    {
        public override StationBase CreateStation(string title)
        {
            Random rnd = new Random();
            return new Halte(title, rnd.Next(1, 3), rnd.Next(0, 2) == 1);
        }

        public override string GetFactoryType() => "Фабрика Полустанков";
    }
}