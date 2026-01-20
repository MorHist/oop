using System;

namespace Lr1.Exceptions
{
    /// <summary>
    /// Бизнес-исключение для нарушений бизнес-правил станций
    /// </summary>
    public class StationBusinessException : Exception
    {
        public string StationType { get; }
        public DateTime ErrorTime { get; }

        public StationBusinessException() : base("Нарушение бизнес-правил станции")
        {
            ErrorTime = DateTime.Now;
        }

        public StationBusinessException(string message) : base(message)
        {
            ErrorTime = DateTime.Now;
        }

        public StationBusinessException(string message, string stationType) : base(message)
        {
            StationType = stationType;
            ErrorTime = DateTime.Now;
        }

        public StationBusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{ErrorTime:HH:mm:ss}] {StationType ?? "Неизвестный тип"}: {Message}";
        }
    }

    /// <summary>
    /// Исключение для ошибок конфигурации станций
    /// </summary>
    public class StationConfigurationException : Exception
    {
        public string ParameterName { get; }
        public object InvalidValue { get; }

        public StationConfigurationException(string parameterName, object invalidValue, string message)
            : base($"Ошибка конфигурации параметра '{parameterName}' = {invalidValue}: {message}")
        {
            ParameterName = parameterName;
            InvalidValue = invalidValue;
        }

        public StationConfigurationException(string parameterName, object invalidValue,
            string message, Exception innerException)
            : base($"Ошибка конфигурации параметра '{parameterName}' = {invalidValue}: {message}",
                  innerException)
        {
            ParameterName = parameterName;
            InvalidValue = invalidValue;
        }
    }
}