using System;
using Lr1.Models;

namespace Lr1.Factories
{
    /// <summary>
    /// Абстрактный создатель (Creator) - определяет фабричный метод
    /// </summary>
    public abstract class StationFactory
    {
        /// <summary>
        /// Фабричный метод - создает объект StationBase
        /// </summary>
        public abstract StationBase CreateStation(string title);

        /// <summary>
        /// Метод для получения информации о типе создаваемой станции
        /// </summary>
        public abstract string GetFactoryType();

        /// <summary>
        /// Общий метод для создания и отображения информации
        /// </summary>
        public string CreateAndDisplay(string title)
        {
            var station = CreateStation(title);
            return $"Создана станция типа: {station.StationType}\n" +
                   $"Название: {station.Title}";
        }
    }
}