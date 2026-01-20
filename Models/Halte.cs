using System;

namespace Lr1.Models
{
    /// <summary>
    /// Полустанок - небольшая станция без сидячих мест
    /// </summary>
    public class Halte : StationBase
    {
        /// <summary>
        /// Количество платформ
        /// </summary>
        public int PlatformsCount { get; set; }

        /// <summary>
        /// Есть ли навес от дождя
        /// </summary>
        public bool HasRoof { get; set; }

        public override string StationType => "Полустанок";

        public Halte() : base() { }

        public Halte(string title) : base(title) { }

        public Halte(string title, int platformsCount, bool hasRoof) : base(title)
        {
            PlatformsCount = platformsCount;
            HasRoof = hasRoof;
        }

        public override string GetSpecificInfo()
        {
            return $"Платформ: {PlatformsCount}\n" +
                   $"Навес: {(HasRoof ? "Есть" : "Нет")}\n" +
                   $"Сидячих мест: Нет";
        }

        /// <summary>
        /// Переопределение GetFieldValue для специфичных полей
        /// </summary>
        public new object GetFieldValue(string fieldName)
        {
            return fieldName.ToLower() switch
            {
                "platformscount" => PlatformsCount,
                "hasroof" => HasRoof,
                _ => base.GetFieldValue(fieldName)
            };
        }
    }
}