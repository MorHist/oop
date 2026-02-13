namespace Lr1.Interpreter
{
    public abstract class TerminalExpression : IExpression
    {
        protected readonly string _fieldName;
        protected readonly string _value;

        protected TerminalExpression(string fieldName, string value)
        {
            _fieldName = fieldName;
            _value = value;
        }

        public abstract bool Interpret(Station context);
    }
}