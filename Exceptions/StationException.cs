using System;

namespace Lr1.Exceptions
{
    /// <summary>
    /// Представляет исключение, возникающее при отрицательном значении числового поля 
    /// </summary>
    public class NegativeValueException : Exception
    {
        public NegativeValueException(string msg) : base($"{msg} не может быть отрицательным числом") { }
    }

    /// <summary>
    /// Представляет исключение, возникающее при неверном формате номера
    /// </summary>
    public class WrongNumberFormatException : Exception
    {
        public WrongNumberFormatException() : base("Неверный формат номера телефона") { }
    }

    /// <summary>
    /// Представляет исключение, возникающее при некорректной дате открытия
    /// </summary>
    public class InvalidDateOfOpeningException : Exception
    {
        public InvalidDateOfOpeningException() : base("Некорректная дата открытия (должна быть между 1830 и текущей датой)") { }
    }
}