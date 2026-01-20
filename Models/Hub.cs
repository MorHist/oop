using System;

namespace Lr1.Models
{
    /// <summary>
    /// Узел - грузовая станция с возможностью перегрузки
    /// </summary>
    public class Hub : StationBase
    {
        /// <summary>
        /// Количество грузовых кранов
        /// </summary>
        public int CranesCount { get; set; }

        /// <summary>
        /// Одновременная грузопередача (тонн в час)
        /// </summary>
        public double CargoThroughput { get; set; }

        /// <summary>
        /// Количество грузовых платформ
        /// </summary>
        public int CargoPlatformsCount { get; set; }

        /// <summary>
        /// Есть ли весовой контроль
        /// </summary>
        public bool HasWeightControl { get; set; }

        public override string StationType => "Узел";

        public Hub() : base() { }

        public Hub(string title) : base(title) { }

        public Hub(string title, int cranesCount, double cargoThroughput,
            int cargoPlatformsCount, bool hasWeightControl) : base(title)
        {
            CranesCount = cranesCount;
            CargoThroughput = cargoThroughput;
            CargoPlatformsCount = cargoPlatformsCount;
            HasWeightControl = hasWeightControl;
        }

        /// <summary>
        /// Рассчитывает общую грузоподъемность
        /// </summary>
        public double CalculateTotalLiftingCapacity()
        {
            return CranesCount * 10.0; // Каждый кран имеет грузоподъемность 10 тонн
        }

        public override string GetSpecificInfo()
        {
            return $"Кранов: {CranesCount}\n" +
                   $"Грузопередача: {CargoThroughput:F2} т/час\n" +
                   $"Грузовых платформ: {CargoPlatformsCount}\n" +
                   $"Общая грузоподъемность: {CalculateTotalLiftingCapacity():F2} т\n" +
                   $"Весовой контроль: {(HasWeightControl ? "Есть" : "Нет")}\n" +
                   $"Сидячих мест: Нет";
        }

        /// <summary>
        /// Переопределение GetFieldValue для специфичных полей
        /// </summary>
        public new object GetFieldValue(string fieldName)
        {
            return fieldName.ToLower() switch
            {
                "cranescount" => CranesCount,
                "cargothroughput" => CargoThroughput,
                "cargoplatformscount" => CargoPlatformsCount,
                "hasweightcontrol" => HasWeightControl,
                _ => base.GetFieldValue(fieldName)
            };
        }
    }
}