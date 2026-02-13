using System;

namespace Lr1.Interpreter
{
    public class GreaterThanExpression : TerminalExpression
    {
        public GreaterThanExpression(string fieldName, string value)
            : base(fieldName, value) { }

        public override bool Interpret(Station context)
        {
            if (_fieldName.ToLower() == "seats" && context.NumberOfSeats.HasValue)
                return context.NumberOfSeats.Value > int.Parse(_value);
            if (_fieldName.ToLower() == "year")
                return context.DateOfOpening.Year > int.Parse(_value);
            if (_fieldName.ToLower() == "sold" && context.SoldTickets.HasValue)
                return context.SoldTickets.Value > int.Parse(_value);
            if (_fieldName.ToLower() == "attendance" && context.AverageAttendace.HasValue)
                return context.AverageAttendace.Value > double.Parse(_value);
            return false;
        }
    }

    public class LessThanExpression : TerminalExpression
    {
        public LessThanExpression(string fieldName, string value)
            : base(fieldName, value) { }

        public override bool Interpret(Station context)
        {
            if (_fieldName.ToLower() == "seats" && context.NumberOfSeats.HasValue)
                return context.NumberOfSeats.Value < int.Parse(_value);
            if (_fieldName.ToLower() == "year")
                return context.DateOfOpening.Year < int.Parse(_value);
            if (_fieldName.ToLower() == "sold" && context.SoldTickets.HasValue)
                return context.SoldTickets.Value < int.Parse(_value);
            if (_fieldName.ToLower() == "attendance" && context.AverageAttendace.HasValue)
                return context.AverageAttendace.Value < double.Parse(_value);
            return false;
        }
    }

    public class EqualsExpression : TerminalExpression
    {
        public EqualsExpression(string fieldName, string value)
            : base(fieldName, value) { }

        public override bool Interpret(Station context)
        {
            if (_fieldName.ToLower() == "seats" && context.NumberOfSeats.HasValue)
                return context.NumberOfSeats.Value == int.Parse(_value);
            if (_fieldName.ToLower() == "year")
                return context.DateOfOpening.Year == int.Parse(_value);
            if (_fieldName.ToLower() == "sold" && context.SoldTickets.HasValue)
                return context.SoldTickets.Value == int.Parse(_value);
            if (_fieldName.ToLower() == "attendance" && context.AverageAttendace.HasValue)
                return Math.Abs(context.AverageAttendace.Value - double.Parse(_value)) < 0.001;
            return false;
        }
    }

    public class ContainsExpression : TerminalExpression
    {
        public ContainsExpression(string fieldName, string value)
            : base(fieldName, value) { }

        public override bool Interpret(Station context)
        {
            if (_fieldName.ToLower() == "title" && !string.IsNullOrEmpty(context.Title))
                return context.Title.Contains(_value, StringComparison.OrdinalIgnoreCase);
            if (_fieldName.ToLower() == "adress" && !string.IsNullOrEmpty(context.Address))
                return context.Address.Contains(_value, StringComparison.OrdinalIgnoreCase);
            if (_fieldName.ToLower() == "phonenum" && !string.IsNullOrEmpty(context.Number))
                return context.Number.Contains(_value);
            return false;
        }
    }
}